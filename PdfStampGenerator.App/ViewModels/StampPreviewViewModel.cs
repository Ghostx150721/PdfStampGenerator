using PdfStampGenerator.App.Commands;
using PdfStampGenerator.App.Converters;
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

        public StampContentKind Content1Kind
        {
            get => _stamp.Content1.Kind;
            set
            {
                // rule: Content1 cannot be None
                var normalized = (value == StampContentKind.None) ? StampContentKind.Text : value;
                if (_stamp.Content1.Kind == normalized) return;

                _stamp.Content1.Kind = normalized;

                OnPropertyChanged(nameof(Content1Kind));
                OnPropertyChanged(nameof(Content1Resolved));
                OnPropertyChanged(nameof(IsContent1TextEnabled));
                OnPropertyChanged(nameof(PreviewLines));
            }
        }

        public string Content1Text
        {
            get => _stamp.Content1.Text;
            set
            {
                var v = value ?? string.Empty;
                if (_stamp.Content1.Text == v) return;

                _stamp.Content1.Text = v;

                // If user types, force Kind=Text (nice UX)
                if (_stamp.Content1.Kind != StampContentKind.Text)
                {
                    _stamp.Content1.Kind = StampContentKind.Text;
                    OnPropertyChanged(nameof(Content1Kind));
                    OnPropertyChanged(nameof(IsContent1TextEnabled));
                }

                OnPropertyChanged(nameof(Content1Text));
                OnPropertyChanged(nameof(Content1Resolved));
                OnPropertyChanged(nameof(PreviewLines));
            }
        }

        public string Content1Resolved => _stamp.Content1.Resolve("yyyy-MM-dd HH:mm");
        public bool IsContent1TextEnabled => Content1Kind == StampContentKind.Text;

        public StampContentKind Content2Kind
        {
            get => _stamp.Content2.Kind;
            set
            {
                if (_stamp.Content2.Kind == value) return;
                _stamp.Content2.Kind = value;

                // Optional: clear text if not Text
                if (value != StampContentKind.Text)
                    _stamp.Content2.Text = string.Empty;

                OnPropertyChanged(nameof(Content2Kind));
                OnPropertyChanged(nameof(Content2Text));
                OnPropertyChanged(nameof(Content2Resolved));
                OnPropertyChanged(nameof(IsContent2TextEnabled));
                OnPropertyChanged(nameof(PreviewLines));
            }
        }

        public string Content2Text
        {
            get => _stamp.Content2.Text;
            set
            {
                var v = value ?? string.Empty;
                if (_stamp.Content2.Text == v) return;

                _stamp.Content2.Text = v;

                // If user types, force Kind=Text (optional)
                if (!string.IsNullOrWhiteSpace(v) && _stamp.Content2.Kind != StampContentKind.Text)
                {
                    _stamp.Content2.Kind = StampContentKind.Text;
                    OnPropertyChanged(nameof(Content2Kind));
                    OnPropertyChanged(nameof(IsContent2TextEnabled));
                }

                OnPropertyChanged(nameof(Content2Text));
                OnPropertyChanged(nameof(Content2Resolved));
                OnPropertyChanged(nameof(PreviewLines));
            }
        }

        public string Content2Resolved => _stamp.Content2.Resolve("yyyy-MM-dd HH:mm");

        public bool IsContent2TextEnabled => Content2Kind == StampContentKind.Text;


        public StampContentKind Content3Kind
        {
            get => _stamp.Content3.Kind;
            set
            {
                if (_stamp.Content3.Kind == value) return;
                _stamp.Content3.Kind = value;

                // Optional: clear text if not Text
                if (value != StampContentKind.Text)
                    _stamp.Content3.Text = string.Empty;

                OnPropertyChanged(nameof(Content3Kind));
                OnPropertyChanged(nameof(Content3Text));
                OnPropertyChanged(nameof(Content3Resolved));
                OnPropertyChanged(nameof(IsContent3TextEnabled));
                OnPropertyChanged(nameof(PreviewLines));
            }
        }

        public string Content3Text
        {
            get => _stamp.Content3.Text;
            set
            {
                var v = value ?? string.Empty;
                if (_stamp.Content3.Text == v) return;

                _stamp.Content3.Text = v;

                // If user types, force Kind=Text (optional)
                if (!string.IsNullOrWhiteSpace(v) && _stamp.Content3.Kind != StampContentKind.Text)
                {
                    _stamp.Content3.Kind = StampContentKind.Text;
                    OnPropertyChanged(nameof(Content3Kind));
                    OnPropertyChanged(nameof(IsContent3TextEnabled));
                }

                OnPropertyChanged(nameof(Content3Text));
                OnPropertyChanged(nameof(Content3Resolved));
                OnPropertyChanged(nameof(PreviewLines));
            }
        }

        public string Content3Resolved => _stamp.Content3.Resolve("yyyy-MM-dd HH:mm");

        public bool IsContent3TextEnabled => Content3Kind == StampContentKind.Text;

        public ObservableCollection<string> PreviewLines =>
            new(new[]
            {
                Content1Resolved,
                Content2Resolved,
                Content3Resolved
            }.Where(s => !string.IsNullOrWhiteSpace(s)));

        public Color FillColor
        {
            get => ColorHex.ToColor(_stamp.FillColorHex);
            set
            {
                var hex = ColorHex.ToHex(value);
                if (_stamp.FillColorHex == hex) return;

                _stamp.FillColorHex = hex;
                OnPropertyChanged(nameof(FillColor));
                OnPropertyChanged(nameof(FillBrush));
            }
        }

        public Color BorderColor
        {
            get => ColorHex.ToColor(_stamp.BorderColorHex);
            set
            {
                var hex = ColorHex.ToHex(value);
                if (_stamp.BorderColorHex == hex) return;

                _stamp.BorderColorHex = hex;
                OnPropertyChanged(nameof(BorderColor));
                OnPropertyChanged(nameof(BorderBrush));
            }
        }

        public Color FontColor
        {
            get => ColorHex.ToColor(_stamp.FontColorHex);
            set
            {
                var hex = ColorHex.ToHex(value);
                if (_stamp.FontColorHex == hex) return;

                _stamp.FontColorHex = hex;
                OnPropertyChanged(nameof(FontColor));
                OnPropertyChanged(nameof(FontBrush));
            }
        }

        public float BorderThickness { get => _stamp.BorderThickness; set { _stamp.BorderThickness = value; OnPropertyChanged(); } }
        public double FontSize { get => _stamp.FontSize; set { _stamp.FontSize = value; OnPropertyChanged(); } }

        public Brush FillBrush
        {
            get
            {
                var brush = new SolidColorBrush(ColorHex.ToColor(_stamp.FillColorHex));
                brush.Opacity = FillOpacity;
                return brush;
            }
        }

        public Brush BorderBrush => new SolidColorBrush(ColorHex.ToColor(_stamp.BorderColorHex));
        public Brush FontBrush => new SolidColorBrush(ColorHex.ToColor(_stamp.FontColorHex));

        //public FontFamily FontFamily => _stamp.FontFamily;
        public CornerRadius CornerRadius => Shape == StampShape.RoundedRectangle ? new CornerRadius(14) : new CornerRadius(0);

        // ===== Commands =====
        public ICommand SetFillColorCommand { get; }
        public ICommand SetBorderColorCommand { get; }
        public ICommand SetFontColorCommand { get; }
        public ICommand ExportPngCommand { get; }
        public ICommand ExportJpegCommand { get; }
        public ICommand ToggleFillColorPanelCommand { get; }

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
