﻿using System.Collections.Generic;
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
            this.pak1 = new Commands(pakPath1);
            this.pak2 = new Commands(pakPath2);
        }

        /*
         * Gets all the differences
         */
        public List<CompositeDifference> loadDifferences() {
            if (pak1.Loaded && pak2.Loaded) {
                this.loadDifferencesComposites();

                return this.compositeDifferences;
            }

            return null;
        }

        /*
         * Loads differences for composites
         */
        private void loadDifferencesComposites() {
            foreach (Composite composite in pak1.Composites) {
                // first we check for composite deletion (check 1: check if shortGuid is in both paks. check 2: In not, check if name/path of the composite exists in both paks)
                bool pak2ContainsComposite = pak2.GetComposite(composite.shortGUID) != null;
                if (!pak2ContainsComposite) {
                    pak2ContainsComposite = pak2.GetCompositeNames().ToList().Contains(composite.name);
                }

                if (!pak2ContainsComposite) {
                    CompositeDifference compositeDifference = new CompositeDifference(composite, DIFFERENCE_TYPE.DELETED, null);
                    this.compositeDifferences.Add(compositeDifference);
                } else {
                    // this part handles modifications (responsible for checking entities) - we do not want to check for modified entities if a composite got deleted
                    this.loadDifferencesEntities(composite, pak2.GetComposite(composite.shortGUID));
                }
            }
            // afterwards we check if any composite has been created - this does not mean we want all the entities and parameter inside the newly created composite. We only want the composite in this case
            foreach (Composite pak2composite in pak2.Composites) {
                // same as for checking for DELETED, here again we check if the composite exists in both paks
                bool pak1ContainsComposite = pak1.GetComposite(pak2composite.shortGUID) != null;
                if (!pak1ContainsComposite) {
                    pak1ContainsComposite = pak1.GetCompositeNames().ToList().Contains(pak2composite.name);
                }

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
                string name = EntityUtils.GetName(composite.shortGUID, entity.shortGUID);

                // first we check for entity deletion
                Entity pak2Entity = pak2Composite.GetEntityByID(entity.shortGUID);
                if (pak2Entity == null) {
                    EntityDifference entityDifference = new EntityDifference(entity, DIFFERENCE_TYPE.DELETED, null, composite, pak2Composite);
                    compositeDifference.entityDifferences.Add(entityDifference);
                } else {
                    // now we handle modifications with parameters and links of this entity
                    EntityDifference entityDifference = this.loadDifferencesParameters(entity, pak2Entity, compositeDifference);
                    entityDifference = this.loadDifferencesLinks(entity, pak2Entity, compositeDifference, entityDifference);

                    //if (null != entityDifference) {
                    //    compositeDifference.entityDifferences.Add(entityDifference);
                    //}
                }
            }
            // afterwards we check if any entity has been created - we dont want any parameters here, just the newly created entities
            foreach (Entity pak2Entity in pak2Composite.GetEntities()) {
                if (null == composite.GetEntityByID(pak2Entity.shortGUID)) { // null means the entity with guid from composite2 wasn't found in composite1
                    EntityDifference entityDifference = new EntityDifference(null, DIFFERENCE_TYPE.CREATED, pak2Entity, composite, pak2Composite);
                    this.loadParameterDifferenceByNewEntity(pak2Entity, entityDifference);
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
            EntityDifference entityDifference = new EntityDifference(entity, DIFFERENCE_TYPE.MODIFIED, pak2Entity, compositeDifference.composite, compositeDifference.compositePak2);

            foreach (Parameter parameter in entity.parameters) {
                // do not handle resources currently - we don't want to see the differences as of now
                if (parameter.content.dataType == DataType.RESOURCE) {
                    continue;
                }

                // first we check for parameter deletion
                Parameter pak2Parameter = pak2Entity.parameters.Find(x => x.name == parameter.name);
                if (null == pak2Parameter) {
                    ParameterDifference parameterDifference = new ParameterDifference(parameter, DIFFERENCE_TYPE.DELETED, null);
                    entityDifference.parameterDiffereces.Add(parameterDifference);
                } else {
                    ParameterDifference parameterDifference = loadParameterValues(parameter, pak2Parameter);

                    if (null != parameterDifference) {
                        entityDifference.parameterDiffereces.Add(parameterDifference);
                    }
                }
            }
            // afterwards we check if any parameter has been created - we dont want any parameter values here, just newly created parameters
            foreach (Parameter pak2Parameter in pak2Entity.parameters) {
                // same as above - do not handle resources currently - we don't want to see the differences as of now
                if (pak2Parameter.content.dataType == DataType.RESOURCE) {
                    continue;
                }
                Parameter foundPak1Parameter = entity.parameters.Find(x => x.name == pak2Parameter.name);
                if (null == foundPak1Parameter) {
                    ParameterDifference parameterDifference = new ParameterDifference(null, DIFFERENCE_TYPE.CREATED, pak2Parameter);
                    parameterDifference.valueAfter = this.getParameterValue(pak2Parameter);
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
                if (pak2Parameter.content.dataType == DataType.RESOURCE) {
                    continue;
                }

                ParameterDifference parameterDifference = new ParameterDifference(null, DIFFERENCE_TYPE.CREATED, pak2Parameter);
                parameterDifference.valueAfter = this.getParameterValue(pak2Parameter);
                entityDifference.parameterDiffereces.Add(parameterDifference);
            }
        }

        /*
         * Checks if the parameter in both entities from each PAK are the same
         * The performance of this part is really bad currently
         */
        private ParameterDifference loadParameterValues(Parameter parameter, Parameter pak2Parameter) {
            ParameterDifference parameterDifference = new ParameterDifference(parameter, DIFFERENCE_TYPE.MODIFIED, pak2Parameter);

            ParameterData data = parameter.content;
            if (data.dataType == pak2Parameter.content.dataType) {
                // this part is trash - maybe change that if everything is clear in the end
                switch (data.dataType) {
                    case DataType.STRING:
                        cString cString = (cString)parameter.content;
                        cString cString2 = (cString)pak2Parameter.content;
                        parameterDifference.valueBefore = data.dataType.ToString() + ": " + cString.value;
                        parameterDifference.valueAfter = pak2Parameter.content.dataType.ToString() + ": " + cString2.value;
                        break;
                    case DataType.FLOAT:
                        cFloat cFloat = (cFloat)parameter.content;
                        cFloat cFloat2 = (cFloat)pak2Parameter.content;
                        parameterDifference.valueBefore = data.dataType.ToString() + ": " + cFloat.value.ToString();
                        parameterDifference.valueAfter = pak2Parameter.content.dataType.ToString() + ": " + cFloat2.value.ToString();
                        break;
                    case DataType.INTEGER:
                        cInteger cInteger = (cInteger)parameter.content;
                        cInteger cInteger2 = (cInteger)pak2Parameter.content;
                        parameterDifference.valueBefore = data.dataType.ToString() + ": " + cInteger.value.ToString();
                        parameterDifference.valueAfter = pak2Parameter.content.dataType.ToString() + ": " + cInteger2.value.ToString();
                        break;
                    case DataType.BOOL:
                        cBool cBool = (cBool)parameter.content;
                        cBool cBool2 = (cBool)pak2Parameter.content;
                        parameterDifference.valueBefore = data.dataType.ToString() + ": " + cBool.value.ToString();
                        parameterDifference.valueAfter = pak2Parameter.content.dataType.ToString() + ": " + cBool2.value.ToString();
                        break;
                    case DataType.VECTOR:
                        cVector3 cVector3 = (cVector3)parameter.content;
                        cVector3 cVector32 = (cVector3)pak2Parameter.content;
                        parameterDifference.valueBefore = data.dataType.ToString() + ": " + cVector3.value.ToString();
                        parameterDifference.valueAfter = pak2Parameter.content.dataType.ToString() + ": " + cVector32.value.ToString();
                        break;
                    case DataType.TRANSFORM:
                        cTransform cTransform = (cTransform)parameter.content;
                        cTransform cTransform2 = (cTransform)pak2Parameter.content;
                        parameterDifference.valueBefore = data.dataType.ToString() + ": " + cTransform.position.ToString() + ", " + cTransform.rotation.ToString();
                        parameterDifference.valueAfter = pak2Parameter.content.dataType.ToString() + ": " + cTransform2.position.ToString() + ", " + cTransform.rotation.ToString();
                        break;
                    case DataType.ENUM:
                        cEnum cEnum = (cEnum)parameter.content;
                        EnumUtils.EnumDescriptor descriptor = EnumUtils.GetEnum(cEnum.enumID);
                        string element = descriptor.Entries.ElementAtOrDefault(cEnum.enumIndex);
                        if (element != null) {
                            parameterDifference.valueBefore = data.dataType.ToString() + ": [" + cEnum.enumID.ToString() + ": " + descriptor.Entries.ElementAtOrDefault(cEnum.enumIndex) + " (enumIndex: " + cEnum.enumIndex + ")]";
                        } else {
                            parameterDifference.valueBefore = data.dataType.ToString() + ": [" + cEnum.enumID.ToString() + ": " + cEnum.enumIndex + "]";
                        }

                        cEnum cEnum2 = (cEnum)pak2Parameter.content;
                        EnumUtils.EnumDescriptor descriptor2 = EnumUtils.GetEnum(cEnum2.enumID);
                        string element2 = descriptor2.Entries.ElementAtOrDefault(cEnum2.enumIndex);
                        if (element2 != null) {
                            parameterDifference.valueAfter = pak2Parameter.content.dataType.ToString() + ": [" + cEnum2.enumID.ToString() + ": " + descriptor2.Entries.ElementAtOrDefault(cEnum2.enumIndex) + " (enumIndex: " + cEnum2.enumIndex + ")]";
                        } else {
                            parameterDifference.valueAfter = pak2Parameter.content.dataType.ToString() + ": [" + cEnum2.enumID.ToString() + ": " + cEnum2.enumIndex + "]";
                        }

                        break;
                    case DataType.SPLINE:
                        // TODO make splines better
                        cSpline cSpline = (cSpline)parameter.content;
                        cSpline cSpline2 = (cSpline)pak2Parameter.content;
                        string str1 = cSpline.dataType.ToString() + ":";
                        foreach (cTransform c in cSpline.splinePoints) {
                            str1 += " [position: " + c.position.ToString() + ", rotation: " + c.rotation.ToString() + "]";
                        }
                        string str2 = cSpline2.dataType.ToString() + ":";
                        foreach (cTransform c2 in cSpline2.splinePoints) {
                            str2 += " [position: " + c2.position.ToString() + ", rotation: " + c2.rotation.ToString() + "]";
                        }
                        parameterDifference.valueBefore = str1;
                        parameterDifference.valueAfter = str2;
                        break;
                    case DataType.RESOURCE:
                        // TODO
                        break;
                    case DataType.FILEPATH:
                        // TODO
                        break;
                    case DataType.OBJECT:
                        // TODO
                        break;
                    case DataType.ZONE_LINK_PTR:
                        // TODO
                        break;
                    case DataType.ZONE_PTR:
                        // TODO
                        break;
                    case DataType.MARKER:
                        // TODO
                        break;
                    case DataType.CHARACTER:
                        // TODO
                        break;
                    case DataType.CAMERA:
                        // TODO
                        break;

                    case DataType.NONE:
                    default:
                        // Do nothing - default values are already set
                        break;
                }
            } else { // dataType was changed of this parameter (this case probably shouldn't exist since it's not possible to date to edit the datatype itself)
                parameterDifference.valueBefore = "Initial type: " + data.dataType.ToString();
                parameterDifference.valueAfter = "New type" + pak2Parameter.content.dataType.ToString();
            }

            // check if they are the same - if yes there is no difference
            if (parameterDifference.valueBefore == parameterDifference.valueAfter) {
                return null;
            }

            return parameterDifference;
        }

        /*
         * Gets the readable value for the given parameter, e.g: STRING: example
         * Maybe merge this function and loadParameterValues() together later
         */
        private string getParameterValue(Parameter parameter) {
            string value = "";

            ParameterData data = parameter.content;

            // this part is trash - maybe change that if everything is clear in the end
            switch (data.dataType) {
                case DataType.STRING:
                    cString cString = (cString)parameter.content;
                    value = data.dataType.ToString() + ": " + cString.value;
                    break;
                case DataType.FLOAT:
                    cFloat cFloat = (cFloat)parameter.content;
                    value = data.dataType.ToString() + ": " + cFloat.value.ToString();
                    break;
                case DataType.INTEGER:
                    cInteger cInteger = (cInteger)parameter.content;
                    value = data.dataType.ToString() + ": " + cInteger.value.ToString();
                    break;
                case DataType.BOOL:
                    cBool cBool = (cBool)parameter.content;
                    value = data.dataType.ToString() + ": " + cBool.value.ToString();
                    break;
                case DataType.VECTOR:
                    cVector3 cVector3 = (cVector3)parameter.content;
                    value = data.dataType.ToString() + ": " + cVector3.value.ToString();
                    break;
                case DataType.TRANSFORM:
                    cTransform cTransform = (cTransform)parameter.content;
                    value = data.dataType.ToString() + ": " + cTransform.position.ToString() + ", " + cTransform.rotation.ToString();
                    break;
                case DataType.ENUM:
                    // TODO make enums better
                    cEnum cEnum = (cEnum)parameter.content;
                    EnumUtils.EnumDescriptor descriptor = EnumUtils.GetEnum(cEnum.enumID);
                    string element = descriptor.Entries.ElementAtOrDefault(cEnum.enumIndex);
                    if (element != null) {
                        value = data.dataType.ToString() + ": [" + cEnum.enumID.ToString() + ": " + descriptor.Entries.ElementAtOrDefault(cEnum.enumIndex) + " (enumIndex: " + cEnum.enumIndex + ")]";
                    } else {
                        value = data.dataType.ToString() + ": [" + cEnum.enumID.ToString() + ": " + cEnum.enumIndex + "]";
                    }
                    break;
                case DataType.SPLINE:
                    // TODO make splines better
                    cSpline cSpline = (cSpline)parameter.content;
                    string str1 = cSpline.dataType.ToString() + ":";
                    foreach (cTransform c in cSpline.splinePoints) {
                        str1 += " [position: " + c.position.ToString() + ", rotation: " + c.rotation.ToString() + "]";
                    }
                    value = str1;
                    break;
                case DataType.RESOURCE:
                    // TODO
                    break;
                case DataType.FILEPATH:
                    // TODO
                    break;
                case DataType.OBJECT:
                    // TODO
                    break;
                case DataType.ZONE_LINK_PTR:
                    // TODO
                    break;
                case DataType.ZONE_PTR:
                    // TODO
                    break;
                case DataType.MARKER:
                    // TODO
                    break;
                case DataType.CHARACTER:
                    // TODO
                    break;
                case DataType.CAMERA:
                    // TODO
                    break;

                case DataType.NONE:
                default:
                    value = "<<Unkown type: Please create an bug report if this occurs>>";
                    break;
            }

            return value;
        }

        /*
         * Loads differences for links
         */
        private EntityDifference loadDifferencesLinks(Entity entity, Entity pak2Entity, CompositeDifference compositeDifference, EntityDifference entityDifference) {
            List<LinkDifference> linkDifferences = new List<LinkDifference>();

            // first we check for deletion (modification of a link is not possible - in this case it would be a new link
            foreach (EntityLink link  in entity.childLinks) {
                EntityLink pak2Link = pak2Entity.childLinks.Find(x => x.connectionID == link.connectionID);
                if (null == pak2Link.connectionID.val) {
                    LinkDifference linkDifference = new LinkDifference(link, DIFFERENCE_TYPE.DELETED, link, compositeDifference.composite, compositeDifference.compositePak2, entity, pak2Entity);
                    linkDifferences.Add(linkDifference);
                } else {
                    // TODO implement link moficiations -> modification of links seems to be possible in the current staging branch of OpenCAGE
                }
            }
            // afterwards we check if any link has been created
            foreach (EntityLink pak2Link in pak2Entity.childLinks) {
                EntityLink foundPak1Link = entity.childLinks.Find(x => x.connectionID == pak2Link.connectionID);
                if (null == foundPak1Link.connectionID.val) {
                    LinkDifference linkDifference = new LinkDifference(pak2Link, DIFFERENCE_TYPE.CREATED, pak2Link, compositeDifference.composite, compositeDifference.composite, entity, pak2Entity);
                    linkDifferences.Add(linkDifference);
                }
            }

            if (null == entityDifference && 0 < linkDifferences.Count) {
                entityDifference = new EntityDifference(entity, DIFFERENCE_TYPE.MODIFIED, pak2Entity, compositeDifference.composite, compositeDifference.compositePak2);
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
