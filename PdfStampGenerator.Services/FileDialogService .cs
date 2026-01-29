using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfStampGenerator.Services
{
    public class FileDialogService : IFileDialogService
    {
        public string? ShowSaveFileDialog(string filter, string defaultExt)
        {
            var dailog = new SaveFileDialog
            {
                Filter = filter,
                DefaultExt = defaultExt,
                AddExtension = true
            };

            return dailog.ShowDialog() == true
                ? dailog.FileName
                : null;
        }
    }
}
