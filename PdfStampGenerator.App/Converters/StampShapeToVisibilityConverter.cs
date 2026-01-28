using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using PdfStampGenerator.Core.Enums;

namespace PdfStampGenerator.App.Converters
{
    public class StampShapeToVisibilityConverter : IValueConverter
    {
        public bool IsCircle { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is StampShape shape)
            {
                if (IsCircle)
                    return shape == StampShape.Circle ? Visibility.Visible : Visibility.Collapsed;

                // Border should be visible for Rectangle & RoundedRectangle
                return shape != StampShape.Circle ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
