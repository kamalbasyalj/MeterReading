using MeterReading.Model;
using System.Collections.Generic;

namespace MeterReading.Interface
{
  public interface IMeterReadingValidator
  {
    bool IsValid(CsvMeterReading csvMeterReading, List<CsvMeterReading> validMeterReading);
  }
}