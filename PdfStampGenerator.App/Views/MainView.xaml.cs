using PdfStampGenerator.App.ViewModels;
using PdfStampGenerator.Services;
using System.Windows;

namespace PdfStampGenerator.App.Views
{
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();

            // Create services
            IStampExportService exportService = new StampExportService();
            IFileDialogService fileDialogService = new FileDialogService();

            // Inject services
            DataContext = new StampPreviewViewModel(
                exportService,
                fileDialogService);
        }
    }
}
