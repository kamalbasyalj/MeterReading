using MeterReading.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MeterReading.Controllers
{
  [ApiController]
  public class MeterReadingUploadsController : ControllerBase
  {
    private readonly IDataProcessor csvDataProcessor;

    public MeterReadingUploadsController(IDataProcessor csvDataProcessor)
    {     
      this.csvDataProcessor = csvDataProcessor;
    }

    // POST v1/meter-reading-uploads
    [HttpPost]
    [Route("v1/meter-reading-uploads")]
    [Consumes("text/csv")]   
    public async Task<IImportResult> Post()
    {      
        return await csvDataProcessor.Process(Request.Body);        
    }
  }
}
