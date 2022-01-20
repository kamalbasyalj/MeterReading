using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeterReading.Model
{
  public class CsvMeterReading
  {
    public int AccountId { get; set; }
    public DateTime MeterReadingDateTime { get; set; }
    public string MeterReadValue { get; set; }
  }
}
