using System;
using System.Windows.Media;
using PdfStampGenerator.Core.Enums;

namespace PdfStampGenerator.Core.Models
{
    public class StampModel
    {
        public StampShape Shape { get; set; } = StampShape.Rectangle;

        public Color FillColor { get; set; } = Colors.Transparent;
        public Color BorderColor { get; set; } = Colors.Black;
        public float BorderThickness { get; set; } = 2;

        public string Title { get; set; } = "APPROVED";
        public string User { get; set; } = Environment.UserName;
        public DateTime Timestamp { get; set; } = DateTime.Now;

        public FontFamily FontFamily { get; set; } = new("Segoe UI");
        public double FontSize { get; set; } = 16;
        public Color FontColor { get; set; } = Colors.Black;
    }
}
