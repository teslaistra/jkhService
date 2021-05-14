using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Sentry;
using Serilog;

namespace Gateway.Controllers
{
    [Route("api/v1/TopicsList")]
    [ApiController]
    public class GatewayController : ControllerBase
    {
        private IConfiguration _configuration;

        public GatewayController(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        // GET: api/Gateway
        [HttpGet]
        public async Task<IActionResult> GetFitnessRecords()
        {
            Console.WriteLine("tut");
            var logger = new LoggerConfiguration()
                   .WriteTo.Sentry("https://8472251de833404e9ecd48cdfeb6ed00@o661932.ingest.sentry.io/5764923")
                   .Enrich.FromLogContext()
                   .CreateLogger();

            var result = "OK";
            return Ok(result);
        }
    }
}
