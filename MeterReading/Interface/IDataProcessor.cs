using System.IO;
using System.Threading.Tasks;

namespace MeterReading.Interface
{
  public interface IDataProcessor
  {
    Task<IImportResult> Process(Stream dataStream);
  }
}