# Custom PDF Stamp Generator (WPF · MVVM)

A **WPF MVVM–based desktop application** for designing and exporting **custom PDF stamps** with live preview. This project follows **industry-standard MVVM practices**, clean architecture, and is intended as a **public, portfolio-quality GitHub project**.

---

## Features

### Stamp Design

- Multiple **stamp shapes** (Rectangle, Rounded Rectangle, Circle, Custom)
- **Color palette** with custom color picker
- Border thickness & fill options

### Stamp Content

- Support for **at least 3 content fields**:
  - Stamp Title / Name
  - Author / User
  - Date & Time (auto-generated or custom)
- Customizable **font family**, **font size**, **font weight**, and **alignment**

### Live Preview

- Real-time **WYSIWYG preview** of the stamp while editing
- Preview updates instantly when properties change

### Export Options

- Save stamp as:
  - `.png`
  - `.jpeg`
  - (extensible for SVG / PDF stamp objects)
- Configurable DPI and transparency

### Architecture & Quality

- Strict **MVVM pattern** (no code-behind logic)
- Command-based actions (`ICommand`)
- Observable state updates (`INotifyPropertyChanged`)
- Clean separation of **UI / Domain / Infrastructure**

---

## Project Structure

```
/src
 ├── PdfStampGenerator.App          # WPF Views (XAML)
 ├── PdfStampGenerator.Core         # Domain
 └── PdfStampGenerator.Services     # Rendering, export, preview logic
     └── ImageExport, FileSystem    # Stamp models, enums, rules
```

---

## Technology Stack

- .NET 8
- **UI**: WPF (XAML)
- **Pattern**: MVVM
- **Graphics**: `DrawingVisual`, `RenderTargetBitmap`
- **PDF Ready**: Designed to integrate with PDF libraries (iText7 / PDFSharp / Syncfusion)

---

## Design Goals

- Production-quality MVVM architecture
- Testable business logic (ViewModel & Services)
- Easily extendable stamp types and exporters

---

## Preview (Planned)

> Screenshots and GIF demos will be added after first stable release.

---

## Getting Started

### Prerequisites

- Visual Studio 2022+
- .NET Desktop Development workload

### Run Locally

```bash
git clone https://github.com/Ghostx150721/PdfStampGenerator.git
```

Open the solution in **Visual Studio** and run the `PdfStampGenerator.UI` project.

---

## Testing Strategy (Planned)

- Unit tests for:
  - Stamp models
  - Validation rules
  - Export services
- Fake rendering services for ViewModel tests]

---

## Contributing

Contributions are welcome.

1. Fork the repository
2. Create a feature branch
3. Commit with clear messages
4. Open a Pull Request

---

## Author

**Hirusha Yohan**\
WPF / .NET Developer

---

## ⭐ Why This Project?

This project demonstrates:

- Real-world WPF MVVM architecture
- Graphics rendering in desktop apps
- Clean, maintainable C# code
- Open-source collaboration readiness

If you find this useful, consider giving it a ⭐ on GitHub.

