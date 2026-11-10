using System;
using System.Collections.Generic;
using System.Linq;

using CATHODE;
using CATHODE.Scripting;
using CATHODE.Scripting.Internal;

namespace AIPAKDifferentiation {

    class PAKFileDifferentiation {

        private string pakPath1 = "";
        private string pakPath2 = "";

        public Commands pak1 = null;
        public Commands pak2 = null;

        List<CompositeDifference> compositeDifferences = new List<CompositeDifference>();

        /*
         * This class compares two PAK files with each other and shows the differences in composites, entities, parameters and links
         * pakPath1 is handeled like the original PAK file, while pakPath2 is considered modified
         */
        public PAKFileDifferentiation(string pakPath1, string pakPath2) {
            this.pakPath1 = pakPath1;
            this.pakPath2 = pakPath2;

            // Load PAKs
            this.pak1 = LoadCommands(pakPath1);
            this.pak2 = LoadCommands(pakPath2);
        }

        private static Commands LoadCommands(string path) {
            // Resource differences are excluded. Empty companion tables let CathodeLib read
            // references without requiring other level files next to a standalone COMMANDS.PAK.
            return new Commands(path,
                new EnvironmentAnimations("", null),
                new CollisionMaps("", null, null),
                new RenderableElements("", null, null));
        }

        /*
         * Gets all the differences
         */
        public List<CompositeDifference> loadDifferences() {
            if (pak1.Loaded && pak2.Loaded) {
                this.compositeDifferences.Clear();
                this.loadDifferencesComposites();
                this.sortDifferences();

                return this.compositeDifferences;
            }

            return null;
        }

        /*
         * Sorts the differences alphabetically.
         */
        private void sortDifferences() {
            // Sort once so both views and text exports use the same sibling order.
            // Created entries use PAK 2. Modified/Deleted entries use their displayed PAK 1 name.
            var comparer = StringComparer.OrdinalIgnoreCase;
            this.compositeDifferences = this.compositeDifferences
                .OrderBy(d => (d.composite ?? d.compositePak2).name, comparer)
                .ThenBy(d => (d.composite ?? d.compositePak2).shortGUID.AsUInt32)
                .ToList()
            ;

            foreach (CompositeDifference compositeDifference in this.compositeDifferences) {
                compositeDifference.entityDifferences = compositeDifference.entityDifferences
                    .OrderBy(d => d.differenceType == DIFFERENCE_TYPE.CREATED
                        ? d.utilsPak2.GetEntityName(d.compositePak2, d.entityPak2)
                        : d.utils.GetEntityName(d.composite, d.entity), comparer)
                    .ThenBy(d => (d.entity ?? d.entityPak2).shortGUID.AsUInt32)
                    .ToList()
                ;

                foreach (EntityDifference entityDifference in compositeDifference.entityDifferences) {
                    entityDifference.parameterDiffereces = entityDifference.parameterDiffereces
                        .OrderBy(d => (d.parameter ?? d.parameterPak2).name.ToString(), comparer)
                        .ThenBy(d => (d.parameter ?? d.parameterPak2).name.AsUInt32)
                        .ToList()
                    ;

                    entityDifference.linkDifferences = entityDifference.linkDifferences
                        .OrderBy(d => (d.differenceType == DIFFERENCE_TYPE.CREATED ? d.linkPak2 : d.link).linkedEntityID.ToString(), comparer)
                        .ThenBy(d => (d.differenceType == DIFFERENCE_TYPE.CREATED ? d.linkPak2 : d.link).thisParamID.ToString(), comparer)
                        .ThenBy(d => (d.differenceType == DIFFERENCE_TYPE.CREATED ? d.linkPak2 : d.link).linkedParamID.ToString(), comparer)
                        .ThenBy(d => (d.differenceType == DIFFERENCE_TYPE.CREATED ? d.linkPak2 : d.link).ID.AsUInt32)
                        .ToList()
                    ;
                }
            }
        }

        /*
         * Loads differences for composites
         */
        private void loadDifferencesComposites() {
            foreach (Composite composite in pak1.Entries) {
                // first we check for composite deletion (check 1: check if shortGuid is in both paks. check 2: In not, check if name/path of the composite exists in both paks)
                Composite pak2Composite = pak2.GetComposite(composite.shortGUID) ?? pak2.GetComposite(composite.name);

                if (pak2Composite == null) {
                    CompositeDifference compositeDifference = new CompositeDifference(composite, DIFFERENCE_TYPE.DELETED, null);
                    this.compositeDifferences.Add(compositeDifference);
                } else {
                    // this part handles modifications (responsible for checking entities) - we do not want to check for modified entities if a composite got deleted
                    this.loadDifferencesEntities(composite, pak2Composite);
                }
            }
            // afterwards we check if any composite has been created - this does not mean we want all the entities and parameter inside the newly created composite. We only want the composite in this case
            foreach (Composite pak2composite in pak2.Entries) {
                // same as for checking for DELETED, here again we check if the composite exists in both paks
                bool pak1ContainsComposite = (pak1.GetComposite(pak2composite.shortGUID) ?? pak1.GetComposite(pak2composite.name)) != null;

                if (!pak1ContainsComposite) {
                    CompositeDifference compositeDifference = new CompositeDifference(null, DIFFERENCE_TYPE.CREATED, pak2composite);
                    this.compositeDifferences.Add(compositeDifference);
                }
            }
        }

        /*
         * Loads differences for entities
         */
        private void loadDifferencesEntities(Composite composite, Composite pak2Composite) {
            CompositeDifference compositeDifference = new CompositeDifference(composite, DIFFERENCE_TYPE.MODIFIED, pak2Composite);
            
            foreach (Entity entity in composite.GetEntities()) {
                // first we check for entity deletion
                Entity pak2Entity = pak2Composite.GetEntityByID(entity.shortGUID);
                if (pak2Entity == null) {
                    EntityDifference entityDifference = new EntityDifference(entity, DIFFERENCE_TYPE.DELETED, null, composite, pak2Composite, pak1.Utils, pak2.Utils, this.pak1, this.pak2);
                    compositeDifference.entityDifferences.Add(entityDifference);
                } else {
                    // now we handle modifications with parameters and links of this entity
                    EntityDifference entityDifference = this.loadDifferencesParameters(entity, pak2Entity, compositeDifference);
                    entityDifference = this.loadDifferencesLinks(entity, pak2Entity, compositeDifference, entityDifference);
                    List<string> storedDataChanges = EntityStoredData.changes(entity, pak2Entity);
                    if (storedDataChanges.Count > 0) {
                        if (entityDifference == null) {
                            entityDifference = new EntityDifference(entity, DIFFERENCE_TYPE.MODIFIED, pak2Entity, composite, pak2Composite, pak1.Utils, pak2.Utils, this.pak1, this.pak2);
                            compositeDifference.entityDifferences.Add(entityDifference);
                        }

                        entityDifference.storedDataChanges.AddRange(storedDataChanges);
                    }
                }
            }
            // afterwards we check if any entities have been created and include their stores parameters and links
            foreach (Entity pak2Entity in pak2Composite.GetEntities()) {
                if (null == composite.GetEntityByID(pak2Entity.shortGUID)) { // null means the entity with guid from composite2 wasn't found in composite1
                    EntityDifference entityDifference = new EntityDifference(null, DIFFERENCE_TYPE.CREATED, pak2Entity, composite, pak2Composite, pak1.Utils, pak2.Utils, this.pak1, this.pak2);
                    this.loadParameterDifferenceByNewEntity(pak2Entity, entityDifference);
                    foreach (EntityConnector link in pak2Entity.childLinks) {
                        entityDifference.linkDifferences.Add(new LinkDifference(link, DIFFERENCE_TYPE.CREATED, link,
                            composite, pak2Composite, null, pak2Entity, pak1.Utils, pak2.Utils));
                    }
                    compositeDifference.entityDifferences.Add(entityDifference);
                }
            }

            // if we found any changed entities -> add the composite where the entity lays to the compositeDifferences list
            if (0 < compositeDifference.entityDifferences.Count) {
                this.compositeDifferences.Add(compositeDifference);
            }
        }

        /*
         * Loads differences for parameters
         */
        private EntityDifference loadDifferencesParameters(Entity entity, Entity pak2Entity, CompositeDifference compositeDifference) {
            EntityDifference entityDifference = new EntityDifference(entity, DIFFERENCE_TYPE.MODIFIED, pak2Entity, compositeDifference.composite, compositeDifference.compositePak2, pak1.Utils, pak2.Utils, this.pak1, this.pak2);

            foreach (Parameter parameter in entity.parameters) {
                // first we check for parameter deletion
                Parameter pak2Parameter = pak2Entity.parameters.Find(x => x.name == parameter.name);
                if (isEmptyResource(parameter) && (pak2Parameter == null || isEmptyResource(pak2Parameter))) {
                    continue;
                }

                if (null == pak2Parameter) {
                    ParameterDifference parameterDifference = new ParameterDifference(parameter, DIFFERENCE_TYPE.DELETED, null);
                    parameterDifference.valueBefore = ParameterValueFormatter.format(parameter, this.pak1.Utils, entity, compositeDifference.composite);
                    parameterDifference.parent = entityDifference;
                    entityDifference.parameterDiffereces.Add(parameterDifference);
                } else {
                    ParameterDifference parameterDifference = this.loadParameterValues(parameter, pak2Parameter, entity, pak2Entity, compositeDifference.composite, compositeDifference.compositePak2);

                    if (null != parameterDifference) {
                        parameterDifference.parent = entityDifference;
                        entityDifference.parameterDiffereces.Add(parameterDifference);
                    }
                }
            }
            // afterwards we check if any parameter has been created - we dont want any parameter values here, just newly created parameters
            foreach (Parameter pak2Parameter in pak2Entity.parameters) {
                if (isEmptyResource(pak2Parameter)) {
                    continue;
                }

                Parameter foundPak1Parameter = entity.parameters.Find(x => x.name == pak2Parameter.name);
                if (null == foundPak1Parameter) {
                    ParameterDifference parameterDifference = new ParameterDifference(null, DIFFERENCE_TYPE.CREATED, pak2Parameter);
                    parameterDifference.valueAfter = ParameterValueFormatter.format(pak2Parameter, this.pak2.Utils, pak2Entity, compositeDifference.compositePak2);
                    parameterDifference.parent = entityDifference;
                    entityDifference.parameterDiffereces.Add(parameterDifference);
                }
            }

            if (0 < entityDifference.parameterDiffereces.Count) {
                compositeDifference.entityDifferences.Add(entityDifference);
                return entityDifference;
            } else {
                return null;
            }
        }

        /*
         * Loads parameter values as a list of ParameterDifference's - Used for newly created entities
         * This is a feature requested in this issue @see https://github.com/Oliver2Goetz/AIPAKFileDifferentiation/issues/2
         */
        private void loadParameterDifferenceByNewEntity(Entity pak2Entity, EntityDifference entityDifference) { 
            foreach (Parameter pak2Parameter in pak2Entity.parameters) {
                if (isEmptyResource(pak2Parameter)) {
                    continue;
                }

                ParameterDifference parameterDifference = new ParameterDifference(null, DIFFERENCE_TYPE.CREATED, pak2Parameter);
                parameterDifference.valueAfter = ParameterValueFormatter.format(pak2Parameter, this.pak2.Utils, pak2Entity, entityDifference.compositePak2);
                parameterDifference.parent = entityDifference;
                entityDifference.parameterDiffereces.Add(parameterDifference);
            }
        }

        private ParameterDifference loadParameterValues(Parameter parameter, Parameter pak2Parameter, Entity entity, Entity pak2Entity, Composite composite, Composite pak2Composite) {
            if (ParameterValueFormatter.sameStoredValue(parameter, pak2Parameter)) {
                return null;
            }

            return new ParameterDifference(parameter, DIFFERENCE_TYPE.MODIFIED, pak2Parameter) {
                valueBefore = ParameterValueFormatter.format(parameter, this.pak1.Utils, entity, composite),
                valueAfter = ParameterValueFormatter.format(pak2Parameter, this.pak2.Utils, pak2Entity, pak2Composite)
            };
        }

        private static bool isEmptyResource(Parameter parameter) {
            return parameter?.content is cResource resource && (resource.value == null || resource.value.Count == 0);
        }

        /*
         * Loads differences for links
         */
        private EntityDifference loadDifferencesLinks(Entity entity, Entity pak2Entity, CompositeDifference compositeDifference, EntityDifference entityDifference) {
            List<LinkDifference> linkDifferences = new List<LinkDifference>();
            List<EntityConnector> unmatchedPak2Links = new List<EntityConnector>(pak2Entity.childLinks);

            // first we check for deletion (modification of a link is not possible - in this case it would be a new link
            foreach (EntityConnector link  in entity.childLinks) {
                if (!unmatchedPak2Links.Remove(link)) {
                    LinkDifference linkDifference = new LinkDifference(link, DIFFERENCE_TYPE.DELETED, link, compositeDifference.composite, compositeDifference.compositePak2, entity, pak2Entity, pak1.Utils, pak2.Utils);
                    linkDifferences.Add(linkDifference);
                }
            }

            // afterwards we check if any link has been created
            foreach (EntityConnector pak2Link in unmatchedPak2Links) {
                LinkDifference linkDifference = new LinkDifference(pak2Link, DIFFERENCE_TYPE.CREATED, pak2Link, compositeDifference.composite, compositeDifference.compositePak2, entity, pak2Entity, pak1.Utils, pak2.Utils);
                linkDifferences.Add(linkDifference);
            }

            if (null == entityDifference && 0 < linkDifferences.Count) {
                entityDifference = new EntityDifference(entity, DIFFERENCE_TYPE.MODIFIED, pak2Entity, compositeDifference.composite, compositeDifference.compositePak2, pak1.Utils, pak2.Utils, this.pak1, this.pak2);
                compositeDifference.entityDifferences.Add(entityDifference);
            }

            if (0 < linkDifferences.Count) {
                entityDifference.linkDifferences.AddRange(linkDifferences);

                return entityDifference;
            }

            return entityDifference;
        }
    }
}
