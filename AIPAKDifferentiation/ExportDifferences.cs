using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CATHODE.Scripting;
using CATHODE.Scripting.Internal;

namespace AIPAKDifferentiation {
    class ExportDifferences {
        private string fileContent = "";
        private string fileName = "";

        public List<string> export(List<CompositeDifference> compositeDifferences, string pak1Name) {
            this.constructFileContent(compositeDifferences);
            this.fileName = "AIPakDiff-Export " + pak1Name + " " + DateTime.Now.ToString("yyyy-MM-dd_H-mm-ss") + ".txt";
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), this.fileName);

            try {
                File.WriteAllText(path, this.fileContent, new UTF8Encoding(true));
            } catch (Exception ex) {
                return new List<string> { "exception", "Error while trying to export file.\n" + ex };
            }

            return new List<string> { "path", "File exported with path: " + path, path };
        }

        private void constructFileContent(List<CompositeDifference> compositeDifferences) {
            StringBuilder output = new StringBuilder();
            foreach (CompositeDifference compositeDifference in compositeDifferences) {
                output.AppendLine(DifferenceDetails.composite(compositeDifference));
                output.AppendLine();
                foreach (EntityDifference entityDifference in compositeDifference.entityDifferences) {
                    output.AppendLine("  " + DifferenceDetails.entity(entityDifference).Replace("\n", "\n  ").TrimEnd());
                    output.AppendLine();
                    foreach (ParameterDifference parameterDifference in entityDifference.parameterDiffereces) {
                        output.AppendLine("    " + DifferenceDetails.parameter(parameterDifference).Replace("\n", "\n    ").TrimEnd());
                        output.AppendLine();
                    }
                    foreach (LinkDifference linkDifference in entityDifference.linkDifferences) {
                        output.AppendLine("    " + DifferenceDetails.link(linkDifference).Replace("\n", "\n    ").TrimEnd());
                        output.AppendLine();
                    }
                }
                output.AppendLine(new string('=', 72));
                output.AppendLine();
            }
            this.fileContent = output.ToString();
        }

        // Kept as a single-entry translation point for callers that need an export value.
        private string translateParameter(Parameter parameter, DIFFERENCE_TYPE differenceType, CommandsUtils utils) {
            return "{" + parameter.name + " " + ParameterValueFormatter.format(parameter, utils, detailed: true) + " " + differenceType + "}";
        }
    }
}
