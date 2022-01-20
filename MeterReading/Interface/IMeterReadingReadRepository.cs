using MeterReading.SqlRepository;
using System.Threading.Tasks;

namespace MeterReading.Interface
{
  public interface IMeterReadingReadRepository
  {
    Task<Account> GetAccount(int accountId);
  }
}