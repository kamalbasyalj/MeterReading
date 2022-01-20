namespace MeterReading.Interface
{
  public interface IImportResult
  {
    int Failed { get; set; }
    int Successful { get; set; }
  }
}