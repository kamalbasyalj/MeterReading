using Dapper;
using MeterReading.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MeterReading.SqlRepository
{
  public class MeterReadingWriteRepository : IMeterReadingWriteRepository
  {
    private readonly IConnectionFactory connectionFactory;
    public MeterReadingWriteRepository(IConnectionFactory connectionFactory)
    {
      this.connectionFactory = connectionFactory;
    }

    public async void PersistMeterReading(int accountId, DateTime meterReadingDateTime, int meterReadValue)
    {
      using (var connection = connectionFactory.CreateConnection())
      {
        await connection.OpenAsync();
        DynamicParameters param = new DynamicParameters();
        param.Add("@AccountId", accountId);
        param.Add("@MeterReadingDateTime", meterReadingDateTime);
        param.Add("@MeterReadValue", meterReadValue);
        await connection.ExecuteAsync("usp_Add_MeterReading", param, commandType: CommandType.StoredProcedure);
      }
    }
  }
}
