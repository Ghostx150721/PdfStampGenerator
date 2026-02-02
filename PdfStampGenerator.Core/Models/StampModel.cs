using System;
using System.Windows.Media;
using PdfStampGenerator.Core.Enums;

namespace PdfStampGenerator.Core.Models
{
    public class StampModel
    {
        private float _borderThikness = 5;
        private double _fontSize = 20;
        private string _title = "Sample Text";

        public StampShape Shape { get; set; } = StampShape.Rectangle;

        public Color FillColor { get; set; } = Colors.Transparent;
        public Color BorderColor { get; set; } = Colors.Green;

        public float BorderThickness
        {
            get => _borderThikness;
            set => _borderThikness = value <= 0 ? 1 : value;
        }

        public string Title
        {
            get => _title;
            set => _title = string.IsNullOrWhiteSpace(value) ? "Sample Text" : value;
        }

        public string User { get; set; } = Environment.UserName;
        public DateTime Timestamp { get; set; } = DateTime.Now;

        public FontFamily FontFamily { get; set; } = new("Segoe UI");

        public double FontSize
        {
            get => _fontSize;
            set => _fontSize = value <= 0 ? 10 : value;
        }

        public Color FontColor { get; set; } = Colors.Green;
    }
}
