using System.Collections.Generic;

namespace MeterReading.SqlRepository
{
  using MeterReading = MeterReading.Domain.MeterReading;
  public class Account
  {
    public int AccountId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<MeterReading> MeeterReadings { get; set; }
  }
}