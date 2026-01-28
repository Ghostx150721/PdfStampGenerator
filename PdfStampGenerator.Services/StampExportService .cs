using PdfStampGenerator.Core.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PdfStampGenerator.Services
{
    public class StampExportService : IStampExportService
    {
        public void Export(
            System.Windows.FrameworkElement previewElement,
            string filePath,
            Core.Enums.ExportFormat format)
        {
            if (previewElement == null)
                return;

            previewElement.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            previewElement.Arrange(new Rect(previewElement.DesiredSize));

            int width = (int)previewElement.ActualWidth;
            int height = (int)previewElement.ActualHeight;

            if (width == 0 || height == 0)
                return;

            var rtb = new RenderTargetBitmap(
                width,
                height,
                96,
                96,
                PixelFormats.Pbgra32);

            rtb.Render(previewElement);

            BitmapEncoder encoder = format switch
            {
                ExportFormat.Png => new PngBitmapEncoder(),
                ExportFormat.Jpeg => new JpegBitmapEncoder { QualityLevel = 95 },
                _ => throw new NotSupportedException()
            };

            encoder.Frames.Add(BitmapFrame.Create(rtb));

            using var fs = new FileStream(filePath, FileMode.Create);
            encoder.Save(fs);
        }
    }
}
