using CsvHelper;
using MeterReading.Interface;
using MeterReading.Services;
using MeterReading.SqlRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeterReading
{
  public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {      
      services.AddTransient<IDataProcessor, CsvDataProcessor>();
      services.AddTransient<IMeterReadingReadRepository, MeterReadingReadRepository>();
      services.AddTransient<IMeterReadingWriteRepository, MeterReadingWriteRepository>();
      services.AddTransient<IMeterReadingValidator, MeterReadingValidator>(); 
      services.AddTransient<IConnectionFactory, ConnectionFactory>(); 
      services.AddTransient<IFactory, Factory>(); 
      services.AddControllers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseRouting();
      // global cors policy
      app.UseCors(x => x
          //.AllowAnyMethod()
          .AllowAnyHeader()
          .SetIsOriginAllowed(origin => true) // allow any origin
          //.AllowCredentials()
          ); // allow credentials
      //app.UseCors("MeterReadingClient");
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
