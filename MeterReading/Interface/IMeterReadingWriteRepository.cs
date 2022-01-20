using System;

namespace MeterReading.Interface
{
  public interface IMeterReadingWriteRepository
  {
    void PersistMeterReading(int accountId, DateTime meterReadingDateTime, int meterReadValue);
  }
}