using System;
using System.Globalization;
using System.Numerics;
using System.Linq;
using System.Text;

using CATHODE;
using CATHODE.Scripting;
using CATHODE.Scripting.Internal;

namespace AIPAKDifferentiation {

    internal static class ParameterValueFormatter {

        internal static string formatFloat(float value) {
            // Expand the round-trip representation instead of rounding tiny values to zero.
            string text = value.ToString("R", CultureInfo.InvariantCulture);
            int exponentIndex = text.IndexOf('E');
            if (exponentIndex < 0) {
                return text;
            }

            int exponent = int.Parse(text.Substring(exponentIndex + 1), CultureInfo.InvariantCulture);
            string mantissa = text.Substring(0, exponentIndex);
            string sign = mantissa.StartsWith("-", StringComparison.Ordinal) ? "-" : "";
            mantissa = mantissa.TrimStart('-');
            int point = mantissa.IndexOf('.');
            int decimalPosition = (point < 0 ? mantissa.Length : point) + exponent;
            string digits = mantissa.Replace(".", "");

            if (decimalPosition <= 0) {
                return sign + "0." + new string('0', -decimalPosition) + digits;
            }
            if (decimalPosition >= digits.Length) {
                return sign + digits + new string('0', decimalPosition - digits.Length);
            }

            return sign + digits.Insert(decimalPosition, ".");
        }

        internal static string formatVector(Vector3 value) {
            return "<" + formatFloat(value.X) + ", " + formatFloat(value.Y) + ", " + formatFloat(value.Z) + ">";
        }

        internal static string formatAxes(Vector3 value) {
            return "X: " + formatFloat(value.X) + ", Y: " + formatFloat(value.Y) + ", Z: " + formatFloat(value.Z);
        }

        internal static string formatTransform(cTransform value) {
            return "Position (" + formatAxes(value.position) + "); Rotation (" + formatAxes(value.rotation) + ")";
        }

        internal static string quote(string value) {
            if (value == null) {
                return "<null>";
            }

            StringBuilder result = new StringBuilder("\"");
            foreach (char c in value) {
                switch (c) {
                    case '\\':
                        result.Append("\\\\");
                        break;
                    case '"':
                        result.Append("\\\"");
                        break;
                    case '\n':
                        result.Append("\\n");
                        break;
                    case '\r':
                        result.Append("\\r");
                        break;
                    case '\t':
                        result.Append("\\t");
                        break;
                    default:
                        if (char.IsControl(c)) {
                            result.Append("\\u" + ((int)c).ToString("X4", CultureInfo.InvariantCulture));
                        } else {
                            result.Append(c);
                        }
                        break;
                }
            }

            return result.Append('"').ToString();
        }

        internal static string format(Parameter parameter, CommandsUtils utils = null, Entity entity = null, Composite composite = null, bool detailed = false) {
            if (parameter?.content == null) return "<no payload>";
            ParameterData data = parameter.content;
            switch (data.dataType) {
                case DataType.BOOL:
                    return "BOOL: " + ((cBool)data).value;
                
                case DataType.INTEGER:
                    return "INTEGER: " + ((cInteger)data).value.ToString(CultureInfo.InvariantCulture);
                
                case DataType.FLOAT:
                    return "FLOAT: " + formatFloat(((cFloat)data).value);
                case DataType.STRING:
                case DataType.ENUM_STRING:
                    cString text = (cString)data;
                    cEnumString enumString = data as cEnumString;
                    // Serialized enum strings often arrive as ordinary strings. Metadata can identify the editor category.
                    if (enumString == null && utils != null && entity != null && composite != null) {
                        enumString = utils.CreateDefaultParameterData(entity, composite, parameter.name) as cEnumString;
                    }

                    if (enumString == null) {
                        return "STRING: " + quote(text.value);
                    }

                    string category = Enum.GetValues(typeof(EnumStringType)).Cast<EnumStringType>()
                        .FirstOrDefault(x => ShortGuidUtils.Generate(x.ToString()) == enumString.enumID).ToString();
                    if (ShortGuidUtils.Generate(category) != enumString.enumID) {
                        category = "unresolved";
                    }

                    return "ENUM_STRING (" + category + ", ID: " + enumString.enumID.ToByteString() + "): " + quote(text.value);
                
                case DataType.VECTOR:
                    return "VECTOR: " + formatAxes(((cVector3)data).value);
                
                case DataType.TRANSFORM:
                    return "TRANSFORM: " + formatTransform((cTransform)data);
                
                case DataType.ENUM:
                    cEnum choice = (cEnum)data;
                
                    var descriptor = utils?.GetEnum(choice.enumID);
                    string name = descriptor?.Name ?? "unresolved";
                    string label = descriptor?.Entries?.Find(e => e.Index == choice.enumIndex)?.Name;
                    
                    return "ENUM (" + name + ", ID: " + choice.enumID.ToByteString() + "): key " +
                        choice.enumIndex.ToString(CultureInfo.InvariantCulture) + ", value " + 
                        (label == null ? "unresolved" : quote(label))
                    ;

                case DataType.SPLINE:
                    var points = ((cSpline)data).splinePoints;
                    if (points == null) {
                        return "SPLINE: <no point data>";
                    }
                    if (!detailed) {
                        return "SPLINE: " + points.Count + " point(s)";
                    }

                    return "SPLINE: " + points.Count + " point(s)" +
                        string.Concat(points.Select((point, index) => "\r\n  [" + index + "] " + formatTransform(point)));

                case DataType.NONE:
                    return "NONE: no value";
                
                case DataType.RESOURCE:
                    return "RESOURCE: excluded from comparison (companion files required)";
                
                default:
                    return data.dataType + ": payload unavailable in CathodeLib 1.0";
            }
        }

        internal static bool sameStoredValue(Parameter a, Parameter b) {
            if (a?.content == null || b?.content == null) {
                return a?.content == b?.content && a?.variant == b?.variant;
            }

            if (a.variant != b.variant || a.content.dataType != b.content.dataType) {
                return false;
            }

            ParameterData x = a.content, y = b.content;
            if (x is cSpline sx && y is cSpline sy) {
                if (sx.splinePoints == null || sy.splinePoints == null) return sx.splinePoints == sy.splinePoints;

                return sx.splinePoints.Count == sy.splinePoints.Count && sx.splinePoints.Zip(sy.splinePoints,
                    (p, q) => p.position == q.position && p.rotation == q.rotation).All(equal => equal);
            }

            return x == y;
        }

    }
}
