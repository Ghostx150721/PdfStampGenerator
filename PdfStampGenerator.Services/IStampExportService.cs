using PdfStampGenerator.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PdfStampGenerator.Services
{
    public interface IStampExportService
    {
        void Export(
            FrameworkElement previewElement,
            string filePath,
            ExportFormat format);
    }
}
