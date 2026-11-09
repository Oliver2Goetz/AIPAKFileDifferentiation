using System;
using System.Globalization;
using System.Numerics;

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

    }
}
