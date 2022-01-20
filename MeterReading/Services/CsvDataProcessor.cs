using CsvHelper;
using MeterReading.Interface;
using MeterReading.Model;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace MeterReading.Services
{
  public class CsvDataProcessor : IDataProcessor
  {
    private readonly IFactory factory;
    private readonly IMeterReadingValidator meterReadingValidator;
    private readonly IMeterReadingWriteRepository meterReadingWriteRepository;

    public CsvDataProcessor(IFactory factory, IMeterReadingValidator meterReadingValidator, IMeterReadingWriteRepository meterReadingWriteRepository)
    {
      this.factory = factory;
      this.meterReadingValidator = meterReadingValidator;
      this.meterReadingWriteRepository = meterReadingWriteRepository;
    }

    public async Task<IImportResult> Process(Stream dataStream)
    {
      int successful, failed;
      successful = failed = 0; 
      using (var streamReader = new StreamReader(dataStream))
      {
        var csv = factory.CreateReader(streamReader, new CultureInfo("en-GB"));
        var records = csv.GetRecordsAsync<CsvMeterReading>();
        var validReading = new List<CsvMeterReading>();

        await foreach (var meterReading in records)
        {
          if (meterReadingValidator.IsValid(meterReading, validReading))
          {
            successful++; 
            validReading.Add(meterReading);
            this.meterReadingWriteRepository.PersistMeterReading(meterReading.AccountId, meterReading.MeterReadingDateTime, int.Parse(meterReading.MeterReadValue));
          }
          else
          {
            failed++;
          }
        }
      }

      return new ImportResult { Successful = successful, Failed = failed }; 
    }
  }
}
