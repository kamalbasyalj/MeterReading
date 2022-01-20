using Dapper;
using MeterReading.Interface;
using System.Data;
using System.Linq;
using System.Threading.Tasks;


namespace MeterReading.SqlRepository
{
  using MeterReading =  MeterReading.Domain.MeterReading;
  public class MeterReadingReadRepository : IMeterReadingReadRepository
  {
    private readonly IConnectionFactory connectionFactory;

    public MeterReadingReadRepository(IConnectionFactory connectionFactory)
    {
      this.connectionFactory = connectionFactory;
    }

    public async Task<Account> GetAccount(int accountId)
    {
      using (var connection = connectionFactory.CreateConnection())
      {
        await connection.OpenAsync();
        Account account = null;
        DynamicParameters param = new DynamicParameters();
        param.Add("@AccountId", accountId);
        using (var multi = connection.QueryMultiple("usp_Get_AccountWithMeterReading", param, commandType: CommandType.StoredProcedure))
        {
          account = multi.Read<Account>().FirstOrDefault();
          if (account != null) account.MeeterReadings = multi.Read<MeterReading>().ToList();
        }
        return account;
      }
  }

  }
}
