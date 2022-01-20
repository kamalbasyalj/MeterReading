using MeterReading.Interface;

namespace MeterReading.Model
{
  public class ImportResult : IImportResult
  {
    public int Successful { get; set; }
    public int Failed { get; set; }
  }
}
