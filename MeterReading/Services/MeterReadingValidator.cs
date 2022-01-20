using MeterReading.SqlRepository;
using System.Linq;
using MeterReading.Domain;
using System.Text.RegularExpressions;
using MeterReading.Model;
using System.Collections.Generic;
using MeterReading.Interface;

namespace MeterReading.Services
{
  public class MeterReadingValidator : IMeterReadingValidator
  {
    private CsvMeterReading csvMeterReading;
    private Account account;
    private readonly IMeterReadingReadRepository meterReadingReadRepository;

    public MeterReadingValidator(IMeterReadingReadRepository meterReadingReadRepository)
    {
      this.meterReadingReadRepository = meterReadingReadRepository;
    }

    public bool IsValid(CsvMeterReading csvMeterReading, List<CsvMeterReading> validMeterReading)
    {
      this.csvMeterReading = csvMeterReading;
      var isValid = IsValidReadingValue();
      if (isValid)
      {
        this.account = meterReadingReadRepository.GetAccount(csvMeterReading.AccountId).Result;
        isValid = IsValidAccount() && IsNotDuplicateEntry(validMeterReading) && IsValidReadingValue() && IsNewerReading();
      }
      return isValid;
    }
    /// <summary>
    /// Reading should be in NNNNN
    /// Assumption - posative number less then 5 digit long have preceded 0's which are trimmed by csv
    /// Hence 0 - 99999 are matched as valid entry
    /// </summary>
    /// <returns></returns>
    private bool IsValidReadingValue()
    {
      return Regex.IsMatch(this.csvMeterReading.MeterReadValue, @"^[0-9]{1,5}$");
    }

    private bool IsValidAccount()
    {
      return account != null;
    }
    private bool IsNotDuplicateEntry(List<CsvMeterReading> validMeterReading)
    {
      return
        !validMeterReading.Any(x => x.MeterReadingDateTime == csvMeterReading.MeterReadingDateTime && x.MeterReadValue == csvMeterReading.MeterReadValue) && 
        !account.MeeterReadings.Any(x => x.MeterReadingDateTime == csvMeterReading.MeterReadingDateTime && x.MeterReadValue == int.Parse(csvMeterReading.MeterReadValue));
    }

    private bool IsNewerReading()
    {
      return !account.MeeterReadings.Any(x => x.MeterReadingDateTime > csvMeterReading.MeterReadingDateTime);
    }
  }
}
