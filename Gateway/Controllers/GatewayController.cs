using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
            Console.WriteLine($"gateway555");
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        // GET: api/Gateway
        [HttpGet]
        public async Task<IActionResult> GetTopics()
        {
            Console.WriteLine($"gateway0");
            var logger = new LoggerConfiguration()
                   .WriteTo.Sentry("https://8472251de833404e9ecd48cdfeb6ed00@o661932.ingest.sentry.io/5764923")
                   .Enrich.FromLogContext()
                   .CreateLogger();
            Console.WriteLine($"gateway1");
            try
            {

                logger.Error("Новый поиск тем");

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("sentry-header", "123");
                    //Реализация обращения к сервису
                    var url = _configuration.GetSection("TopicsURI").Value;
                    Console.WriteLine($"gateway");

                    var resultMessage = await client.GetAsync($"{url}topics");
                    Console.WriteLine($"{url}topics");

                    resultMessage.EnsureSuccessStatusCode();
                    var result = await resultMessage.Content.ReadAsStringAsync();
                    return Ok(result);
                }
            }
            catch (Exception e)
            {
                logger.Fatal(e, "Произошла фатальная ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}
