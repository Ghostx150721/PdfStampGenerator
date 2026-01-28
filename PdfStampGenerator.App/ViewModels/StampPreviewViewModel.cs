using PdfStampGenerator.App.Commands;
using PdfStampGenerator.Core.Enums;
using PdfStampGenerator.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace PdfStampGenerator.App.ViewModels;

public class StampPreviewViewModel : INotifyPropertyChanged
{
    private readonly StampModel _stamp = new();

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    public ObservableCollection<StampShape> Shapes { get; } =
    new ObservableCollection<StampShape>
    {
        StampShape.Rectangle,
        StampShape.RoundedRectangle,
        StampShape.Circle
    };

    public ObservableCollection<SolidColorBrush> PresetColors { get; } =
        new()
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




    // ===== Shape =====
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

    // ===== Text =====
    public string Title
    {
        get => _stamp.Title;
        set { _stamp.Title = value; OnPropertyChanged(); }
    }

    public string Author
    {
        get => _stamp.User;
        set { _stamp.User = value; OnPropertyChanged(); }
    }

    public string TimestampText => _stamp.Timestamp.ToString("yyyy-MM-dd HH:mm");

    public Color FillColor
    {
        get => _stamp.FillColor;
        set
        {
            _stamp.FillColor = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(FillBrush));
        }
    }

    public Color BorderColor
    {
        get => _stamp.BorderColor;
        set
        {
            _stamp.BorderColor = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(BorderBrush));
        }
    }

    public Color FontColor
    {
        get => _stamp.FontColor;
        set
        {
            _stamp.FontColor = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(FontBrush));
        }
    }


    // ===== Appearance =====
    public Brush FillBrush => new SolidColorBrush(_stamp.FillColor);
    public Brush BorderBrush => new SolidColorBrush(_stamp.BorderColor);
    public Brush FontBrush => new SolidColorBrush(_stamp.FontColor);

    public double BorderThickness => _stamp.BorderThickness;
    public FontFamily FontFamily => _stamp.FontFamily;
    public double FontSize => _stamp.FontSize;

    public CornerRadius CornerRadius =>
        Shape == StampShape.RoundedRectangle
            ? new CornerRadius(14)
            : new CornerRadius(0);

    public ICommand SetFillColorCommand { get; }
    public ICommand SetBorderColorCommand { get; }
    public ICommand SetFontColorCommand { get; }



    // ===== Test data =====
    public StampPreviewViewModel()
    {
        Shape = StampShape.RoundedRectangle;

        SetFillColorCommand = new RelayCommand<SolidColorBrush>(b => FillColor = b.Color);
        SetBorderColorCommand = new RelayCommand<SolidColorBrush>(b => BorderColor = b.Color);
        SetFontColorCommand = new RelayCommand<SolidColorBrush>(b => FontColor = b.Color);

    }
}
