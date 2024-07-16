using JOIEnergy.Domain;
using JOIEnergy.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JOIEnergy.Controllers
{
    [Route("readings")]
    public class MeterReadingController(IMeterReadingService meterReadingService) : Controller
    {

        // POST api/values
        [HttpPost("store")]
        public ObjectResult Post([FromBody] MeterReadings meterReadings)
        {
            if (!IsMeterReadingsValid(meterReadings))
                return new BadRequestObjectResult("Bad Request");

            meterReadingService.StoreReadings(meterReadings);
            return new OkObjectResult("{}");
        }

        private bool IsMeterReadingsValid(MeterReadings meterReadings)
        {
            var isValid = !string.IsNullOrWhiteSpace(meterReadings?.SmartMeterId) && (meterReadings.ElectricityReadings?.Count ?? 0) != 0;
            return isValid;
        }

        [HttpGet("read/{smartMeterId}")]
        public ObjectResult GetReading(string smartMeterId)
            => new OkObjectResult(meterReadingService.GetReadings(smartMeterId));

    }
}
