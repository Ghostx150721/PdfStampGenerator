using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PdfStampGenerator.App.Converters
{
    public static class ColorHex
    {
        public static Color ToColor(string? hex)
        {
            if (string.IsNullOrWhiteSpace(hex))
                return Colors.Transparent;

            hex = hex.Trim();
            if (!hex.StartsWith("#", StringComparison.Ordinal))
                hex = "#" + hex;

            // Accept #RRGGBB by upgrading to #FFRRGGBB
            if (hex.Length == 7)
                hex = "#FF" + hex.Substring(1);

            return (Color)ColorConverter.ConvertFromString(hex)!;
        }

        public static string ToHex(Color c)
            => $"#{c.A:X2}{c.R:X2}{c.G:X2}{c.B:X2}";
    }
}
