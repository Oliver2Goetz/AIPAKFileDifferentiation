using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CATHODE.Scripting;
using CATHODE.Scripting.Internal;

namespace AIPAKDifferentiation {
    class ExportFilterOptions {
        public bool HideComposites;
        public bool HideEntities;
        public bool HideParameters;
        public bool HideLinks;
        public bool HideAliasEntities;
        public bool HideCreated;
        public bool HideModified;
        public bool HideDeleted;

        public bool allows(DIFFERENCE_TYPE type) {
            switch (type) {
                case DIFFERENCE_TYPE.CREATED:
                    return !this.HideCreated;
                case DIFFERENCE_TYPE.MODIFIED:
                    return !this.HideModified;
                case DIFFERENCE_TYPE.DELETED:
                    return !this.HideDeleted;
                default:
                    return true;
            }
        }
    }

    class ExportDifferences {
        private string fileContent = "";
        private string fileName = "";
        private ExportFilterOptions filters = null;

        public List<string> export(List<CompositeDifference> compositeDifferences, string pak1Name, ExportFilterOptions filters = null) {
            this.filters = filters;
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
            if (this.filters != null) {
                this.constructFilteredFileContent(compositeDifferences);
                return;
            }

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

        private void constructFilteredFileContent(List<CompositeDifference> compositeDifferences) {
            StringBuilder output = new StringBuilder();
            foreach (CompositeDifference compositeDifference in compositeDifferences) {
                List<string> entitySections = new List<string>();
                List<EntityDifference> shownEntityChanges = new List<EntityDifference>();

                foreach (EntityDifference entityDifference in compositeDifference.entityDifferences) {
                    Entity entity = entityDifference.entity ?? entityDifference.entityPak2;
                    if (this.filters.HideAliasEntities && entity.variant == EntityVariant.ALIAS) {
                        continue;
                    }

                    bool showEntityStatus = this.filters.allows(entityDifference.differenceType);
                    List<ParameterDifference> parameters = entityDifference.parameterDiffereces
                        .Where(difference => this.filters.allows(difference.differenceType)).ToList();
                    List<LinkDifference> links = entityDifference.linkDifferences
                        .Where(difference => this.filters.allows(difference.differenceType)).ToList();

                    if (!showEntityStatus && parameters.Count == 0 && links.Count == 0) {
                        continue;
                    }
                    if (showEntityStatus) {
                        shownEntityChanges.Add(entityDifference);
                    }

                    StringBuilder entityOutput = new StringBuilder();
                    if (this.filters.HideEntities || !showEntityStatus) {
                        entityOutput.AppendLine("  Entity - " + entityDifference.differenceType);
                        entityOutput.AppendLine("  ID: " + entity.shortGUID.ToByteString());
                    } else {
                        entityOutput.AppendLine("  " + DifferenceDetails.entity(entityDifference, parameters.Count, links.Count).Replace("\n", "\n  ").TrimEnd());
                    }
                    entityOutput.AppendLine();

                    if (this.filters.HideParameters) {
                        if (parameters.Count > 0) {
                            entityOutput.AppendLine("    " + countSummary("Parameters", parameters));
                            entityOutput.AppendLine();
                        }
                    } else {
                        foreach (ParameterDifference parameterDifference in parameters) {
                            entityOutput.AppendLine("    " + DifferenceDetails.parameter(parameterDifference).Replace("\n", "\n    ").TrimEnd());
                            entityOutput.AppendLine();
                        }
                    }

                    if (this.filters.HideLinks) {
                        if (links.Count > 0) {
                            entityOutput.AppendLine("    " + countSummary("Links", links));
                            entityOutput.AppendLine();
                        }
                    } else {
                        foreach (LinkDifference linkDifference in links) {
                            entityOutput.AppendLine("    " + DifferenceDetails.link(linkDifference).Replace("\n", "\n    ").TrimEnd());
                            entityOutput.AppendLine();
                        }
                    }

                    entitySections.Add(entityOutput.ToString());
                }

                bool showCompositeStatus = this.filters.allows(compositeDifference.differenceType);
                if (!showCompositeStatus && entitySections.Count == 0) {
                    continue;
                }

                if (this.filters.HideComposites || !showCompositeStatus) {
                    Composite composite = compositeDifference.composite ?? compositeDifference.compositePak2;
                    output.AppendLine("Composite - " + compositeDifference.differenceType);
                    output.AppendLine("ID: " + composite.shortGUID.ToByteString());
                } else {
                    output.AppendLine(DifferenceDetails.composite(compositeDifference, shownEntityChanges));
                }

                output.AppendLine();
                foreach (string entitySection in entitySections) {
                    output.Append(entitySection);
                }
                output.AppendLine(new string('=', 72));
                output.AppendLine();
            }

            if (output.Length == 0) {
                output.AppendLine("No differences match the selected filters.");
            }
            this.fileContent = output.ToString();
        }

        private static string countSummary<T>(string title, List<T> differences) where T : AbstractDifference {
            return title + " - CREATED: " + differences.Count(difference => difference.differenceType == DIFFERENCE_TYPE.CREATED) +
                ", MODIFIED: " + differences.Count(difference => difference.differenceType == DIFFERENCE_TYPE.MODIFIED) +
                ", DELETED: " + differences.Count(difference => difference.differenceType == DIFFERENCE_TYPE.DELETED);
        }

        // Kept as a single-entry translation point for callers that need an export value.
        private string translateParameter(Parameter parameter, DIFFERENCE_TYPE differenceType, CommandsUtils utils) {
            return "{" + parameter.name + " " + ParameterValueFormatter.format(parameter, utils, detailed: true) + " " + differenceType + "}";
        }
    }
}
