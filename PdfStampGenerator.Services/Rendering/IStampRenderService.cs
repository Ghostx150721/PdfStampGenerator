using System.Windows;
using PdfStampGenerator.Core.Enums;

namespace PdfStampGenerator.Services.Rendering;

public interface IStampRenderService
{
    void Export(
        FrameworkElement previewElement,
        string filePath,
        ExportFormat format);
}
