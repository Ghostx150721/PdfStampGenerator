namespace PdfStampGenerator.Services.Time;

public interface ITimeProvider
{
    DateTime Now { get; }
}
