using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfStampGenerator.Services
{
    public interface IFileDialogService
    {
        string? ShowSaveFileDialog(string filter, string defaultExt);
    }
}
