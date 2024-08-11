using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

using CATHODE.Scripting;
using CATHODE.Scripting.Internal;

namespace AIPAKDifferentiation {

    class ExportDifferences {

        private string fileContent = "";
        private string fileName = "";
        private const string COMPOSITE_SEPARATOR = "=";

        /*
         * Entrypoint for exporting the differences to a file
         */
        public List<string> export(List<CompositeDifference> compositeDifferences, string pak1Name) {
            this.constructFileContent(compositeDifferences);
            this.constructFileName(compositeDifferences, pak1Name);
            List<string> exportResult = this.createFileWithContent();

            return exportResult;
        }

        /*
         * Constructs the file content out of the CompositeDifferences
         */
        private void constructFileContent(List<CompositeDifference> compositeDifferences) {
            foreach (CompositeDifference compositeDifference in compositeDifferences) {
                string compositeLine = this.getCompositeLine(compositeDifference);
                this.appendTemporaryContent(compositeLine);

                foreach (EntityDifference entityDifference in compositeDifference.entityDifferences) {

                    string entityName = this.getEntityLineName(entityDifference);
                    string parametersString = this.handleParameterDifferences(compositeDifference, entityDifference);

                    if ("" != parametersString) {
                        string entityAndParameterLine = "[" + entityDifference.differenceType + "] " + entityName + " " + parametersString;
                        this.appendTemporaryContent(entityAndParameterLine);
                    }

                    string linkString = this.handleLinkDifference(compositeDifference, entityDifference);
                    if ("" != linkString) {
                        string entityAndLinkLine = "[" + entityDifference.differenceType + "] " + entityName + " " + linkString;
                        this.appendTemporaryContent(entityAndLinkLine);
                    }
                }

                this.appendTemporaryContent(COMPOSITE_SEPARATOR);
            }
        }

        /*
         * Constructs the file name
         */
        private void constructFileName(List<CompositeDifference> compositeDifferences, string pak1Name) {
            DateTime date = DateTime.Now;

            string dateString = date.ToString("yyyy-MM-dd_H-mm-ss");

            this.fileName = "AIPakDiff-Export " + pak1Name + " " + dateString + ".txt";
        }

        /*
         * Creates the export file with the content
         */
        private List<string> createFileWithContent() {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            path += "\\" +this.fileName;

            List<string> returnList = new List<string>();
            try {
                using (FileStream fs = File.Create(path)) {
                    byte[] info = new UTF8Encoding(true).GetBytes(this.fileContent);

                    fs.Write(info, 0, info.Length);
                }

            } catch (Exception ex) {

                returnList.Add("exception");
                returnList.Add("Error while trying to export file.\n" + ex.ToString());

                return returnList;
            }

            returnList.Add("path");
            returnList.Add("File exported with path: " + path);
            returnList.Add(path);

            return returnList;
        }

        /*
         * Returns the line for the composite
         * example: AYZ\DOORS\SUICIDE_BARRIERS
         */
        private string getCompositeLine(CompositeDifference compositeDifference) {
            string compositeName = "";
            
            if (compositeDifference.differenceType == DIFFERENCE_TYPE.CREATED) {
                compositeName = compositeDifference.compositePak2.name;
            } else {
                compositeName = compositeDifference.composite.name;
            }

            return compositeName + " (" + compositeDifference.differenceType.ToString() + ")";
        }

        /*
         * Returns the first part of the entity line
         * First part contains the name of this entity (parameters will be added separately afterwards)
         * example: LogicDelay_4
         */
        private string getEntityLineName(EntityDifference entityDifference) {
            string entityName = "";

            if (entityDifference.differenceType == DIFFERENCE_TYPE.CREATED) {
                entityName = EntityUtils.GetName(entityDifference.compositePak2.shortGUID, entityDifference.entityPak2.shortGUID);
            } else {
                entityName = EntityUtils.GetName(entityDifference.composite.shortGUID, entityDifference.entity.shortGUID);
            }

            if (entityName.StartsWith("CATHODE.Scripting.")) {
                entityName = entityName.Replace("CATHODE.Scripting.", "");
            }

            return entityName;
        }

        /*
         * Handles the parameters and returns them as a string
         * example: {delay=0} | {initial_value=5}
         */
        private string handleParameterDifferences(CompositeDifference compositeDifference, EntityDifference entityDifference) {
            List<string> parameters = new List<string>();

            foreach (ParameterDifference parameterDifference in entityDifference.parameterDiffereces) {
                Parameter parameter = null;
                if (parameterDifference.differenceType == DIFFERENCE_TYPE.CREATED) {
                    parameter = parameterDifference.parameterPak2;
                } else {
                    parameter = parameterDifference.parameter;
                }

                string stringParam = this.translateParameter(parameter, parameterDifference.differenceType);
                parameters.Add(stringParam);
            }

            if (0 == parameters.Count) {
                return "";
            }

            return String.Join(" | ", parameters);
        }

        /*
         * Handles the links and returns them as a string
         * example: Links: {RipleyFinished => [BF-0F-1E-C9: first_alien_encounter]}
         */
        private string handleLinkDifference(CompositeDifference compositeDifference, EntityDifference entityDifference) {
            List<string> links = new List<string>();

            foreach (LinkDifference linkDifference in entityDifference.linkDifferences) {
                EntityLink link = linkDifference.link;
                if (linkDifference.differenceType == DIFFERENCE_TYPE.CREATED) {
                    link = linkDifference.linkPak2;
                } else {
                    link = linkDifference.link;
                }

                string stringLink = "{" + link.parentParamID + " => [" + link.childID + ": " + link.childParamID + "] " + linkDifference.differenceType + "}";
                links.Add(stringLink);
            }

            if (0 == links.Count) {
                return "";
            }

            return "Links: " + String.Join(" | ", links);
        }

        /*
         * Writes the content including a break to the string which will be later written to the export file
         */
        private void appendTemporaryContent(string line) {
            this.fileContent += line + "\n";
        }


        private string translateParameter(Parameter parameter, DIFFERENCE_TYPE differenceType) {
            string newValue = "";

            ParameterData data = parameter.content;
            // copied from another code part. We shouldn't do this but I don't care xD
            switch (data.dataType) {
                case DataType.STRING:
                    cString cString = (cString)parameter.content;
                    newValue = data.dataType.ToString() + COMPOSITE_SEPARATOR + cString.value;
                    break;

                case DataType.FLOAT:
                    cFloat cFloat = (cFloat)parameter.content;
                    newValue = parameter.content.dataType.ToString() + COMPOSITE_SEPARATOR + cFloat.value.ToString();
                    break;

                case DataType.INTEGER:
                    cInteger cInteger = (cInteger)parameter.content;
                    newValue = parameter.content.dataType.ToString() + COMPOSITE_SEPARATOR + cInteger.value.ToString();
                    break;

                case DataType.BOOL:
                    cBool cBool = (cBool)parameter.content;
                    newValue = parameter.content.dataType.ToString() + COMPOSITE_SEPARATOR + cBool.value.ToString();
                    break;

                case DataType.VECTOR:
                    cVector3 cVector3 = (cVector3)parameter.content;
                    newValue = parameter.content.dataType.ToString() + COMPOSITE_SEPARATOR + cVector3.value.ToString();
                    break;

                case DataType.TRANSFORM:
                    cTransform cTransform = (cTransform)parameter.content;
                    newValue = parameter.content.dataType.ToString() + COMPOSITE_SEPARATOR + cTransform.position.ToString() + ", " + cTransform.rotation.ToString();
                    break;

                case DataType.ENUM:
                    cEnum cEnum = (cEnum)parameter.content;
                    EnumUtils.EnumDescriptor descriptor = EnumUtils.GetEnum(cEnum.enumID);
                    string element = descriptor.Entries.ElementAtOrDefault(cEnum.enumIndex);
                    if (element != null) {
                        newValue = data.dataType.ToString() + COMPOSITE_SEPARATOR + "[" + cEnum.enumID.ToString() + ": " + descriptor.Entries.ElementAtOrDefault(cEnum.enumIndex) + " (enumIndex: " + cEnum.enumIndex + ")]";
                    } else {
                        newValue = data.dataType.ToString() + COMPOSITE_SEPARATOR + "[" + cEnum.enumID.ToString() + ": " + cEnum.enumIndex + "]";
                    }
                    break;

                case DataType.SPLINE:
                    // TODO make splines better
                    cSpline cSpline = (cSpline)parameter.content;
                    string str = cSpline.dataType.ToString() + COMPOSITE_SEPARATOR;
                    foreach (cTransform c in cSpline.splinePoints) {
                        str += " [position: " + c.position.ToString() + ", rotation: " + c.rotation.ToString() + "]";
                    }
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
                    newValue = data.dataType.ToString() + COMPOSITE_SEPARATOR + parameter.content.ToString();
                    break;
            }

            return "{" + parameter.name + " " + newValue + " " + differenceType + "}";
        }
    }
}
