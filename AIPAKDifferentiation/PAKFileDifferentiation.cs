using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

using CATHODE;
using CATHODE.Scripting;
using CATHODE.Scripting.Internal;

namespace AIPAKDifferentiation {

    class PAKFileDifferentiation {

        private string pakPath1 = "";
        private string pakPath2 = "";

        Commands pak1 = null;
        Commands pak2 = null;
        public EntityUtils entityUtilsPak1 = null;
        public EntityUtils entityUtilsPak2 = null;

        List<CompositeDifference> compositeDifferences = new List<CompositeDifference>();

        /**
         * This class compares two PAK files with each other and shows the differences in composites, entities and parameters
         * pakPath1 is handeled like the original PAK file, while pakPath2 is considered modified
         */
        public PAKFileDifferentiation(string pakPath1, string pakPath2) {
            this.pakPath1 = pakPath1;
            this.pakPath2 = pakPath2;

            // Load PAKs
            this.pak1 = new Commands(pakPath1);
            this.pak2 = new Commands(pakPath2);
            this.entityUtilsPak1 = new EntityUtils(this.pak1);
            this.entityUtilsPak2 = new EntityUtils(this.pak2);
        }

        public List<CompositeDifference> loadDifferences() {
            if (pak1.Loaded && pak2.Loaded) {
                this.loadDifferencesComposites();

                return this.compositeDifferences;
            }

            return null;
        }

        private void loadDifferencesComposites() {
            foreach (Composite composite in pak1.Composites) {
                // first we check for composite deletion
                if (!pak2.GetCompositeNames().ToList().Contains(composite.name)) {
                    CompositeDifference compositeDifference = new CompositeDifference(composite, COMPOSITE_DIFFERENCE_TYPE.DELETED);
                    this.compositeDifferences.Add(compositeDifference);
                } else {
                    // this part handles modifications (responsible for checking entities) - we do not want to check for modified entities if a composite got deleted
                    this.loadDifferencesEntities(composite, pak2.GetComposite(composite.shortGUID));
                }
            }
            // afterwards we check if any composite has been created - this does not mean we want all the entities and parameter inside the newly created composite. We only want the composite in this case
            foreach (Composite pak2composite in pak2.Composites) {
                if (!pak1.GetCompositeNames().ToList().Contains(pak2composite.name)) {
                    CompositeDifference compositeDifference = new CompositeDifference(pak2composite, COMPOSITE_DIFFERENCE_TYPE.CREATED);
                    this.compositeDifferences.Add(compositeDifference);
                }
            }
        }

        private void loadDifferencesEntities(Composite composite, Composite pak2Composite) {
            CompositeDifference compositeDifference = new CompositeDifference(composite, COMPOSITE_DIFFERENCE_TYPE.MODIFIED);
            
            foreach (Entity entity in composite.GetEntities()) {
                // first we check for entity deletion
                Entity pak2Entity = pak2Composite.GetEntityByID(entity.shortGUID);
                if (pak2Entity == null) {
                    EntityDifference entityDifference = new EntityDifference(entity, ENTITIY_DIFFERENCE_TYPE.DELETED);
                    compositeDifference.entityDifferences.Add(entityDifference);
                } else {
                    // now we handle modifications with parameters and links of this entity
                    EntityDifference entityDifference = this.loadDifferencesParameters(entity, pak2Entity, compositeDifference);
                    entityDifference = this.loadDifferencesLinks(entity, pak2Entity, compositeDifference, entityDifference);

                    if (null != entityDifference) {
                        compositeDifference.entityDifferences.Add(entityDifference);
                    }
                }
            }
            // afterwards we check if any entity has been created - we dont want any parameters here, just the newly created entities
            foreach (Entity pak2Entity in pak2Composite.GetEntities()) {
                if (null == composite.GetEntityByID(pak2Entity.shortGUID)) { // null means the entity with guid from composite2 wasn't found in composite1
                    EntityDifference entityDifference = new EntityDifference(pak2Entity, ENTITIY_DIFFERENCE_TYPE.CREATED);
                    compositeDifference.entityDifferences.Add(entityDifference);
                }
            }

            // if we found any changed entities -> add the composite where the entity lays to the compositeDifferences list
            if (0 < compositeDifference.entityDifferences.Count) {
                this.compositeDifferences.Add(compositeDifference);
            }
        }

        private EntityDifference loadDifferencesParameters(Entity entity, Entity pak2Entity, CompositeDifference compositeDifference) {
            EntityDifference entityDifference = new EntityDifference(entity, ENTITIY_DIFFERENCE_TYPE.MODIFIED);

            foreach (Parameter parameter in entity.parameters) {
                // first we check for parameter deletion
                Parameter pak2Parameter = pak2Entity.parameters.Find(x => x.shortGUID == parameter.shortGUID);
                if(null == pak2Parameter) {
                    ParameterDifference parameterDifference = new ParameterDifference(parameter, PARAMETER_DIFFERENCE_TYPE.DELETED);
                    entityDifference.parameterDiffereces.Add(parameterDifference);
                } else {
                    // now we handle modification of parameter values of this parameter
                    this.loadParameterValues(parameter, pak2Parameter, entityDifference); // TODO has to be implemented
                }
            }
            // afterwards we check if any parameter has been created - we dont want any parameter values here, just newly created parameters
            foreach (Parameter pak2Parameter in pak2Entity.parameters) {
                Parameter foundPak1Parameter = entity.parameters.Find(x => x.shortGUID == pak2Parameter.shortGUID);
                if (null == foundPak1Parameter) {
                    ParameterDifference parameterDifference = new ParameterDifference(pak2Parameter, PARAMETER_DIFFERENCE_TYPE.CREATED);
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

        private EntityDifference loadDifferencesLinks(Entity entity, Entity pak2Entity, CompositeDifference compositeDifference, EntityDifference entityDifference) {
            List<LinkDifference> linkDifferences = new List<LinkDifference>();

            // first we check for deletion (modification of a link is not possible - in this case it would be a new link
            foreach (EntityLink link  in entity.childLinks) {
                EntityLink pak2Link = pak2Entity.childLinks.Find(x => x.connectionID == link.connectionID);

                if (null == pak2Link.connectionID) {
                    LinkDifference linkDifference = new LinkDifference(link, LINK_DIFFERENCE_TYPE.DELETED);
                    linkDifferences.Add(linkDifference);
                }
            }
            // afterwards we check if any link has been created
            foreach (EntityLink pak2Link in pak2Entity.childLinks) {
                EntityLink foundPak1Link = entity.childLinks.Find(x => x.connectionID == pak2Link.connectionID);
                if (null == foundPak1Link.connectionID) {
                    LinkDifference linkDifference = new LinkDifference(pak2Link, LINK_DIFFERENCE_TYPE.CREATED);
                    linkDifferences.Add(linkDifference);
                }
            }

            if (null == entityDifference && 0 < linkDifferences.Count) {
                entityDifference = new EntityDifference(entity, ENTITIY_DIFFERENCE_TYPE.MODIFIED);
                compositeDifference.entityDifferences.Add(entityDifference);
            }
            if (0 < linkDifferences.Count) {
                entityDifference.linkDifferences.AddRange(linkDifferences);

                return entityDifference;
            }

            return null;
        }

        private void loadParameterValues(Parameter parameter, Parameter pak2Parameter, EntityDifference entityDifference) {
            
        }
    }
}
