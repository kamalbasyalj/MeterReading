using System.Data.SqlClient;

namespace MeterReading
{
  public interface IConnectionFactory
  {
    SqlConnection CreateConnection();
  }
}