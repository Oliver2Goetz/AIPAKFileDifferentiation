using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

using CATHODE.Scripting;
using CATHODE.Scripting.Internal;

namespace AIPAKDifferentiation {

    // Only fields supplied by the COMMANDS.PAK entity records belong here.
    internal static class EntityStoredData {

        internal static List<string> details(Entity entity) {
            return fields(entity).OrderBy(field => field.Key, StringComparer.Ordinal)
                .Select(field => field.Key + ": " + field.Value).ToList();
        }

        internal static List<string> changes(Entity initial, Entity updated) {
            Dictionary<string, string> before = fields(initial);
            Dictionary<string, string> after = fields(updated);
            List<string> result = new List<string>();

            foreach (string key in before.Keys.Union(after.Keys).OrderBy(key => key, StringComparer.Ordinal)) {
                before.TryGetValue(key, out string oldValue);
                after.TryGetValue(key, out string newValue);

                if (!String.Equals(oldValue, newValue, StringComparison.Ordinal)) {
                    result.Add(key + ": " + (oldValue ?? "-") + " -> " + (newValue ?? "-"));
                }
            }

            return result;
        }

        private static Dictionary<string, string> fields(Entity entity) {
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("Type", entity.variant.ToString());

            if (entity is VariableEntity variable) {
                values.Add("Variable name ID", id(variable.name));
                values.Add("Variable type", variable.type.ToString());
            }

            if (entity is FunctionEntity function) {
                values.Add("Function ID", id(function.function));
            }

            if (entity is ProxyEntity proxy) {
                values.Add("Proxy function ID", id(proxy.function));
                values.Add("Proxy path", path(proxy.proxy));
                addSequence(values, "Proxy sequence", proxy.sequence);
                addMethods(values, "Proxy methods", proxy.methods);
            }

            if (entity is AliasEntity alias) {
                values.Add("Alias path", path(alias.alias));
            }

            if (entity is CAGEAnimation animation) {
                values.Add("Animation connections count", animation.connections.Count.ToString());
                for (int i = 0; i < animation.connections.Count; i++) {
                    CAGEAnimation.Connection connection = animation.connections[i];
                    string prefix = "Animation connection [" + i + "] ";
                    values.Add(prefix + "binding ID", id(connection.binding_guid));
                    values.Add(prefix + "binding type", connection.binding_type.ToString());
                    values.Add(prefix + "target track", id(connection.target_track));
                    values.Add(prefix + "target parameter", id(connection.target_param));
                    values.Add(prefix + "target parameter type", connection.target_param_type.ToString());
                    values.Add(prefix + "target subparameter", id(connection.target_sub_param));
                    values.Add(prefix + "connected entity", path(connection.connectedEntity));
                }

                values.Add("Animation float tracks count", animation.floatTracks.Count.ToString());
                for (int i = 0; i < animation.floatTracks.Count; i++) {
                    CAGEAnimation.FloatTrack track = animation.floatTracks[i];
                    string prefix = "Animation float track [" + i + "] ";
                    values.Add(prefix + "ID", id(track.shortGUID));
                    values.Add(prefix + "keyframes count", track.keyframes.Count.ToString());
                    for (int j = 0; j < track.keyframes.Count; j++) {
                        CAGEAnimation.FloatTrack.Keyframe keyframe = track.keyframes[j];
                        string frame = prefix + "keyframe [" + j + "] ";
                        values.Add(frame + "mode", keyframe.mode.ToString());
                        values.Add(frame + "time", number(keyframe.time));
                        values.Add(frame + "value", vector(keyframe.value));
                        values.Add(frame + "tangent in", vector(keyframe.tan_in));
                        values.Add(frame + "tangent out", vector(keyframe.tan_out));
                    }
                }

                values.Add("Animation event tracks count", animation.eventTracks.Count.ToString());
                for (int i = 0; i < animation.eventTracks.Count; i++) {
                    CAGEAnimation.EventTrack track = animation.eventTracks[i];
                    string prefix = "Animation event track [" + i + "] ";
                    values.Add(prefix + "ID", id(track.shortGUID));
                    values.Add(prefix + "type", track.track_type.ToString());
                    values.Add(prefix + "keyframes count", track.keyframes.Count.ToString());
                    for (int j = 0; j < track.keyframes.Count; j++) {
                        CAGEAnimation.EventTrack.Keyframe keyframe = track.keyframes[j];
                        string frame = prefix + "keyframe [" + j + "] ";
                        values.Add(frame + "time", number(keyframe.time));
                        values.Add(frame + "forward", id(keyframe.forward));
                        values.Add(frame + "reverse", id(keyframe.reverse));
                        values.Add(frame + "type", keyframe.track_type.ToString());
                        values.Add(frame + "duration", number(keyframe.duration));
                    }
                }
            }

            if (entity is TriggerSequence sequence) {
                addSequence(values, "Trigger sequence", sequence.sequence);
                addMethods(values, "Trigger methods", sequence.methods);
            }

            return values;
        }

        private static void addSequence(Dictionary<string, string> values, string label, List<TriggerSequence.SequenceEntry> entries) {
            values.Add(label + " count", entries.Count.ToString());
            for (int i = 0; i < entries.Count; i++) {
                values.Add(label + " [" + i + "] timing", number(entries[i].timing));
                values.Add(label + " [" + i + "] connected entity", path(entries[i].connectedEntity));
            }
        }

        private static void addMethods(Dictionary<string, string> values, string label, List<TriggerSequence.MethodEntry> entries) {
            values.Add(label + " count", entries.Count.ToString());
            for (int i = 0; i < entries.Count; i++) {
                values.Add(label + " [" + i + "] method", id(entries[i].method));
                values.Add(label + " [" + i + "] relay", id(entries[i].relay));
                values.Add(label + " [" + i + "] finished", id(entries[i].finished));
            }
        }

        private static string path(EntityPath entityPath) {
            if (entityPath?.path == null || entityPath.path.Length == 0) {
                return "<empty>";
            }

            return String.Join(" -> ", entityPath.path.Select(id => id.ToByteString()));
        }

        private static string id(ShortGuid value) {
            return value.ToByteString();
        }

        private static string number(float value) {
            return ParameterValueFormatter.formatFloat(value);
        }

        private static string vector(Vector2 value) {
            return "X: " + number(value.X) + ", Y: " + number(value.Y);
        }
    }
}
