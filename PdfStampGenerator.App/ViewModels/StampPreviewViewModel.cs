using PdfStampGenerator.App.Commands;
using PdfStampGenerator.Core.Enums;
using PdfStampGenerator.Core.Models;
using PdfStampGenerator.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace PdfStampGenerator.App.ViewModels
{
    public class StampPreviewViewModel : INotifyPropertyChanged
    {
        private readonly StampModel _stamp = new();
        private readonly IStampExportService _exportService;
        private readonly IFileDialogService _fileDialogService;

        private const double FillOpacity = 0.1;

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        // ===== Constructor =====
        public StampPreviewViewModel(
            IStampExportService exportService,
            IFileDialogService fileDialogService)
        {
            _exportService = exportService;
            _fileDialogService = fileDialogService;

            Shape = StampShape.RoundedRectangle;

            SetFillColorCommand = new RelayCommand<SolidColorBrush>(b => FillColor = b.Color);
            SetBorderColorCommand = new RelayCommand<SolidColorBrush>(b => BorderColor = b.Color);
            SetFontColorCommand = new RelayCommand<SolidColorBrush>(b => FontColor = b.Color);

            ExportPngCommand = new RelayCommand<FrameworkElement>(ExportPng);
            ExportJpegCommand = new RelayCommand<FrameworkElement>(ExportJpeg);

        }

        // ===== Data =====
        public ObservableCollection<StampShape> Shapes { get; } = new()
        {
            StampShape.Rectangle,
            StampShape.RoundedRectangle,
            StampShape.Circle
        };

        public ObservableCollection<SolidColorBrush> PresetColors { get; } = new()
        {
            Brushes.Black,
            Brushes.DarkRed,
            Brushes.Red,
            Brushes.Orange,
            Brushes.DarkOrange,
            Brushes.Green,
            Brushes.DarkGreen,
            Brushes.Blue,
            Brushes.DarkBlue,
            Brushes.Purple,
            Brushes.Transparent
        };

        // ===== Properties =====
        public StampShape Shape
        {
            get => _stamp.Shape;
            set
            {
                _stamp.Shape = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CornerRadius));
            }
        }

        public string Title { get => _stamp.Title; set { _stamp.Title = value; OnPropertyChanged(); } }
        public string Author { get => _stamp.User; set { _stamp.User = value; OnPropertyChanged(); } }
        public string TimestampText => _stamp.Timestamp.ToString("yyyy-MM-dd HH:mm");

        public Color FillColor { get => _stamp.FillColor; set { _stamp.FillColor = value; OnPropertyChanged(); OnPropertyChanged(nameof(FillBrush)); } }
        public Color BorderColor { get => _stamp.BorderColor; set { _stamp.BorderColor = value; OnPropertyChanged(); OnPropertyChanged(nameof(BorderBrush)); } }
        public Color FontColor { get => _stamp.FontColor; set { _stamp.FontColor = value; OnPropertyChanged(); OnPropertyChanged(nameof(FontBrush)); } }

        public float BorderThickness { get => _stamp.BorderThickness; set { _stamp.BorderThickness = value; OnPropertyChanged(); } }
        public double FontSize { get => _stamp.FontSize; set { _stamp.FontSize = value; OnPropertyChanged(); } }

        public Brush FillBrush
        {
            get
            {
                var brush = new SolidColorBrush(_stamp.FillColor);
                brush.Opacity = FillOpacity;
                return brush;
            }
        }

        public Brush BorderBrush => new SolidColorBrush(_stamp.BorderColor);
        public Brush FontBrush => new SolidColorBrush(_stamp.FontColor);
        public FontFamily FontFamily => _stamp.FontFamily;
        public CornerRadius CornerRadius => Shape == StampShape.RoundedRectangle ? new CornerRadius(14) : new CornerRadius(0);

        // ===== FillColor Panel Toggle =====
        //private bool _isFillColorPanelOpen;
        //public bool IsFillColorPanelOpen
        //{
        //    get => _isFillColorPanelOpen;
        //    set
        //    {
        //        if (SetProperty(ref _isFillColorPanelOpen, value))
        //        {
        //            FillColorToggleIcon = value ? "/Resources/up.png" : "/Resources/down.png";
        //        }
        //    }
        //}

        //private string _fillColorToggleIcon = "/Resources/down.png";
        //public string FillColorToggleIcon
        //{
        //    get => _fillColorToggleIcon;
        //    set => SetProperty(ref _fillColorToggleIcon, value);
        //}

        // ===== Commands =====
        public ICommand SetFillColorCommand { get; }
        public ICommand SetBorderColorCommand { get; }
        public ICommand SetFontColorCommand { get; }
        public ICommand ExportPngCommand { get; }
        public ICommand ExportJpegCommand { get; }
        public ICommand ToggleFillColorPanelCommand { get; }

        //private void ToggleFillColorPanel()
        //{
        //    IsFillColorPanelOpen = !IsFillColorPanelOpen;
        //}

        // ===== Export Logic =====
        private void ExportPng(FrameworkElement element)
        {
            var path = _fileDialogService.ShowSaveFileDialog("PNG Image|*.png", "stamp.png");
            if (path == null) return;
            _exportService.Export(element, path, ExportFormat.Png);
        }

        private void ExportJpeg(FrameworkElement element)
        {
            var path = _fileDialogService.ShowSaveFileDialog("JPEG Image|*.jpg", "stamp.jpg");
            if (path == null) return;
            _exportService.Export(element, path, ExportFormat.Jpeg);
        }
    }
}
