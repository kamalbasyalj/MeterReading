using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MeterReading
{
  public class ConnectionFactory : IConnectionFactory
  {
    private readonly IConfiguration configuration;

    public ConnectionFactory(IConfiguration configuration)
    {
      this.configuration = configuration;
    }

    public SqlConnection CreateConnection()
    {
      return new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }


  }
}
