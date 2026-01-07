using System;
using System.Windows.Media;
using PdfStampGenerator.Core.Enums;

namespace PdfStampGenerator.Core.Models
{
    public class StampModel
    {
        private float _borderThickness = 5;
        private double _fontSize = 20;

        // Unique identity for this stamp preset (never changes once created)
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        public string BaseName { get; set; } = "";

        // Display name shown in UI
        public string StampName { get; set; } = "New Stamp";

        public string Category { get; set; } = "";

        public StampType Type { get; set; } = StampType.Dynamic;

        public StampShape Shape { get; set; } = StampShape.Rectangle;

        public string FillColorHex { get; set; } = "#008000";
        public string BorderColorHex { get; set; } = "#008000";

        public float BorderThickness
        {
            get => _borderThickness;
            set => _borderThickness = value <= 0 ? 1 : value;
        }

        public StampContentSlot Content1 { get; set; } = new()
        {
            Kind = StampContentKind.Text,
            Text = "Sample Text"
        };

        public StampContentSlot Content2 { get; set; } = new()
        {
            Kind = StampContentKind.Text,
            Text = Environment.UserName
        };

        public StampContentSlot Content3 { get; set; } = new()
        {
            Kind = StampContentKind.DateTime
        };

        public string FontFamilyName { get; set; } = "Segoe UI";

        public double FontSize
        {
            get => _fontSize;
            set => _fontSize = value <= 0 ? 10 : value;
        }

        public string FontColorHex { get; set; } = "#008000";

        public void Normalize()
        {
            // Name required
            StampName = string.IsNullOrWhiteSpace(StampName)
                ? "New Stamp"
                : StampName.Trim();

            // Id required
            if (string.IsNullOrWhiteSpace(Id))
                Id = Guid.NewGuid().ToString("N");

            Category = string.IsNullOrWhiteSpace(Category)
                ? ""
                : Category.Trim();

            // If Image stamp → no dynamic validation required
            if (Type == StampType.Image)
                return;

            // ----- Dynamic stamp validation -----

            // Content1 cannot be None
            if (Content1.Kind == StampContentKind.None)
            {
                Content1.Kind = StampContentKind.Text;

                if (string.IsNullOrWhiteSpace(Content1.Text))
                    Content1.Text = "Sample Text";
            }
        }
    }

    public enum StampContentKind { Text = 0, DateTime = 1, None = 2 }

    public sealed class StampContentSlot
    {
        public StampContentKind Kind { get; set; } = StampContentKind.Text;
        public string Text { get; set; } = "";

        public string Resolve(string? dateTimeFormat = null) =>
            Kind switch
            {
                StampContentKind.Text => Text,
                StampContentKind.DateTime => DateTime.Now.ToString(dateTimeFormat ?? "yyyy-MM-dd HH:mm"),
                StampContentKind.None => "",
                _ => ""
            };
    }
}
