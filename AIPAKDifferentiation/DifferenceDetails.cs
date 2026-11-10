using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CATHODE;
using CATHODE.Scripting;
using CATHODE.Scripting.Internal;

namespace AIPAKDifferentiation {

    // Shared by tree selection and text export. Values always refer to stored PAK data.
    internal static class DifferenceDetails {

        internal static string composite(CompositeDifference difference, List<EntityDifference> filteredEntities = null) {
            Composite current = difference.composite ?? difference.compositePak2;
            List<EntityDifference> shownEntities = filteredEntities ?? difference.entityDifferences;

            StringBuilder text = new StringBuilder();
            text.AppendLine("Composite - " + difference.differenceType);
            text.AppendLine("Name: " + current.name);
            text.AppendLine("ID: " + current.shortGUID.ToByteString());
            text.AppendLine();
            appendCompositeSide(text, "Initial", difference.composite);
            text.AppendLine();
            appendCompositeSide(text, "New", difference.compositePak2);
            text.AppendLine();
            text.AppendLine("Changed entities shown: " + shownEntities.Count);

            foreach (DIFFERENCE_TYPE type in new[] { DIFFERENCE_TYPE.CREATED, DIFFERENCE_TYPE.MODIFIED, DIFFERENCE_TYPE.DELETED }) {
                text.AppendLine("  " + type + ": " + shownEntities.Count(x => x.differenceType == type));
            }

            return text.ToString().TrimEnd();
        }

        private static void appendCompositeSide(StringBuilder text, string title, Composite value) {
            if (value == null) {
                text.AppendLine(title + ": -");
                return;
            }

            text.AppendLine(title + " name: " + value.name);
            text.AppendLine(title + " ID: " + value.shortGUID.ToByteString());
            text.AppendLine(title + ": " + value.GetEntities().Count + " entities (" +
                value.variables.Count + " variables, " + value.functions.Count + " functions, " +
                value.aliases.Count + " aliases, " + value.proxies.Count + " proxies)");
        }

        internal static string entity(EntityDifference difference, int? filteredParameterCount = null, int? filteredLinkCount = null) {
            Entity current = difference.entity ?? difference.entityPak2;
            Composite composite = difference.entity == null ? difference.compositePak2 : difference.composite;
            CommandsUtils utils = difference.entity == null ? difference.utilsPak2 : difference.utils;

            StringBuilder text = new StringBuilder();
            text.AppendLine("Entity - " + difference.differenceType);
            text.AppendLine("Name: " + (utils?.GetEntityName(composite, current) ?? "unresolved"));
            text.AppendLine("ID: " + current.shortGUID.ToByteString());
            text.AppendLine("Composite: " + (composite?.name ?? "unresolved"));
            text.AppendLine();
            appendEntitySide(text, "Initial", difference.entity, difference.commands);
            text.AppendLine();
            appendEntitySide(text, "New", difference.entityPak2, difference.commandsPak2);

            if (difference.storedDataChanges.Count > 0) {
                text.AppendLine();
                text.AppendLine("Changed stored entity data:");
                foreach (string change in difference.storedDataChanges) {
                    text.AppendLine("  " + change);
                }
            } else if (
                difference.differenceType != DIFFERENCE_TYPE.MODIFIED &&
                (current is ProxyEntity || current is AliasEntity || current is CAGEAnimation || current is TriggerSequence)
            ) {
                text.AppendLine();
                text.AppendLine("Stored entity data:");
                foreach (string field in EntityStoredData.details(current)) {
                    text.AppendLine("  " + field);
                }
            }

            text.AppendLine();
            text.AppendLine("Changed parameters shown: " + (filteredParameterCount ?? difference.parameterDiffereces.Count));
            text.AppendLine("Changed links shown: " + (filteredLinkCount ?? difference.linkDifferences.Count));

            return text.ToString().TrimEnd();
        }

        private static void appendEntitySide(StringBuilder text, string title, Entity value, Commands commands) {
            if (value == null) {
                text.AppendLine(title + ": -");
                return;
            }

            Composite referencedComposite = value is FunctionEntity functionEntity ? commands?.GetComposite(functionEntity.function) : null;
            string entityType = value.variant.ToString();
            if (referencedComposite != null) {
                entityType += " (composite instance)";
            }

            text.AppendLine(title + " type: " + entityType);
            text.AppendLine(title + " parameters: " + value.parameters.Count);
            text.AppendLine(title + " outgoing links: " + value.childLinks.Count);
            if (value is VariableEntity variable) {
                text.AppendLine(title + " variable: " + variable.name + " [" + variable.name.ToByteString() + "]");
                text.AppendLine(title + " declared type: " + variable.type);

            } else if (value is ProxyEntity proxy) {
                text.AppendLine(title + " proxy function ID: " + proxy.function.ToByteString());
                text.AppendLine(title + " proxy path: " + path(proxy.proxy));
                if (proxy.sequence.Count > 0 || proxy.methods.Count > 0) {
                    text.AppendLine(title + " trigger sequence data: " + proxy.sequence.Count + " sequence entries, " + proxy.methods.Count + " methods");
                }

            } else if (value is AliasEntity alias) {
                text.AppendLine(title + " alias path: " + path(alias.alias));
            } else if (value is FunctionEntity function) {
                text.AppendLine(title + " function ID: " + function.function.ToByteString());
                if (referencedComposite != null) {
                    text.AppendLine(title + " referenced composite: " + referencedComposite.name);
                } else if (function.function.IsFunctionType) {
                    text.AppendLine(title + " function name: " + function.function.AsFunctionType);
                }

                if (function.resources.Count > 0) {
                    text.AppendLine(title + " resource references: " + function.resources.Count + " (companion-file details not compared)");
                }

                if (function is CAGEAnimation animation) {
                    text.AppendLine(title + " animation: " + animation.connections.Count + " connections, " + animation.floatTracks.Count + " float tracks, " + animation.eventTracks.Count + " event tracks");
                }

                if (function is TriggerSequence sequence) {
                    text.AppendLine(title + " trigger sequence: " + sequence.sequence.Count + " entries, " + sequence.methods.Count + " methods");
                }
            }
        }

        private static string path(EntityPath path) {
            if (path?.path == null || path.path.Length == 0) {
                return "<empty>";
            }

            return string.Join(" -> ", path.path.Select(id => id.ToByteString()));
        }

        internal static string parameter(ParameterDifference difference) {
            Parameter current = difference.parameter ?? difference.parameterPak2;
            EntityDifference parent = difference.parent;

            StringBuilder text = new StringBuilder();
            text.AppendLine("Parameter - " + difference.differenceType);
            text.AppendLine("Name: " + current.name);
            text.AppendLine("ID: " + current.name.ToByteString());
            text.AppendLine();
            appendParameterSide(text, "Initial", difference.parameter, parent?.utils, parent?.entity, parent?.composite);
            text.AppendLine();
            appendParameterSide(text, "New", difference.parameterPak2, parent?.utilsPak2, parent?.entityPak2, parent?.compositePak2);

            return text.ToString().TrimEnd();
        }

        private static void appendParameterSide(StringBuilder text, string title, Parameter value, CommandsUtils utils, Entity entity, Composite composite) {
            if (value == null) {
                text.AppendLine(title + ": -");
                return; 
            }

            text.AppendLine(title + " type: " + (value.content?.dataType.ToString() ?? "no payload"));
            text.AppendLine(title + " flags: " + value.variant);
            text.AppendLine(title + " value:");
            text.AppendLine(ParameterValueFormatter.format(value, utils, entity, composite, true));
        }

        internal static string link(LinkDifference difference) {
            EntityConnector current = difference.differenceType == DIFFERENCE_TYPE.CREATED ? difference.linkPak2 : difference.link;

            StringBuilder text = new StringBuilder();
            text.AppendLine("Link - " + difference.differenceType);
            text.AppendLine("Connection ID: " + current.ID.ToByteString());
            text.AppendLine();
            appendLinkSide(text, "Initial", difference.differenceType == DIFFERENCE_TYPE.CREATED ? null : difference.link,
                difference.entity, difference.composite, difference.utils);
            text.AppendLine();
            appendLinkSide(text, "New", difference.differenceType == DIFFERENCE_TYPE.DELETED ? null : difference.linkPak2,
                difference.entityPak2, difference.compositePak2, difference.utilsPak2);

            return text.ToString().TrimEnd();
        }

        private static void appendLinkSide(StringBuilder text, string title, EntityConnector? link, Entity source, Composite composite, CommandsUtils utils) {
            if (link == null) {
                text.AppendLine(title + ": -");
                return;
            }

            EntityConnector value = link.Value;
            string sourceName = source == null || composite == null ? "unresolved" : utils?.GetEntityName(composite, source) ?? "unresolved";
            string targetName = composite == null ? "unresolved" : utils?.GetEntityName(composite.shortGUID, value.linkedEntityID) ?? "unresolved";
            text.AppendLine(title + " connection ID: " + value.ID.ToByteString());
            text.AppendLine(title + " source: " + sourceName + " [" + (source?.shortGUID.ToByteString() ?? "unresolved") + "]");
            text.AppendLine(title + " source parameter: " + value.thisParamID + " [" + value.thisParamID.ToByteString() + "]");
            text.AppendLine(title + " target: " + targetName + " [" + value.linkedEntityID.ToByteString() + "]");
            text.AppendLine(title + " target parameter: " + value.linkedParamID + " [" + value.linkedParamID.ToByteString() + "]");
        }
    }
}
