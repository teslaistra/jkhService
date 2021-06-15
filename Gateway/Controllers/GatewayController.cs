using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sentry;
using Serilog;
using Serilog.Core;
using System.Net.Http.Json;


namespace Gateway.Controllers
{
   
    [ApiController]
    public class GatewayController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<GatewayController> _logger;

        public GatewayController(IConfiguration configuration, ILogger<GatewayController> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/Gateway
        [Route("api/v1/TopicsGet")]
        [HttpGet]
        public async Task<IActionResult> GetTopics()
        {
            var logger = new LoggerConfiguration()
                     .WriteTo.Sentry(_configuration.GetSection("SENTRY").Value)
                     .WriteTo.Console()
                     .Enrich.FromLogContext()
                     .CreateLogger();
            try
            {
                logger.Error("Запрос на поиск тем");
                using HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("sentry-header", "123");
                var url = _configuration.GetSection("TopicsURI").Value;
                var resultMessage = await client.GetAsync($"{url}gettopics");
                resultMessage.EnsureSuccessStatusCode();
                var result = await resultMessage.Content.ReadAsStringAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                logger.Fatal(e, "Произошла фатальная ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [Route("api/v1/EditTopics")]
        [HttpPost]
        public async Task<IActionResult> EditTopics([FromBody] object model)
        {
            var logger = new LoggerConfiguration()
                     .WriteTo.Sentry(_configuration.GetConnectionString("SENTRY"))
                     .WriteTo.Console()
                     .Enrich.FromLogContext()
                     .CreateLogger();
            try
            {
                logger.Error("Запрос на редактирование темы");
                using HttpClient client = new HttpClient();
                var uri = _configuration.GetSection("TopicsURI").Value;
                var resultMessage = await client.PostAsJsonAsync($"{uri}edittopics", model);
                var result = await resultMessage.Content.ReadAsStringAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                logger.Fatal(e, "Произошла фатальная ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [Route("api/v1/DeleteTopics/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteTopics([FromRoute] string id)
        {
            var logger = new LoggerConfiguration()
                     .WriteTo.Sentry(_configuration.GetConnectionString("SENTRY"))
                     .WriteTo.Console()
                     .Enrich.FromLogContext()
                     .CreateLogger();
            try
            {   
                logger.Error("Запрос на удаление темы");
                using (HttpClient client = new HttpClient())
                {
                    var uri = _configuration.GetSection("TopicsURI").Value;
                    var resultMessage = await client.DeleteAsync($"{uri}deletetopics/{id}");
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

        [Route("api/v1/AddTopics")]
        [HttpPost]
        public async Task<IActionResult> AddTopics([FromBody] object model)
        {
            var logger = new LoggerConfiguration()
                     .WriteTo.Sentry(_configuration.GetConnectionString("SENTRY"))
                     .WriteTo.Console()
                     .Enrich.FromLogContext()
                     .CreateLogger();
            try
            {
                logger.Error("Запрос на добавление темы");
                using HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("sentry-header", "123");
                var url = _configuration.GetSection("TopicsURI").Value;
                var resultMessage = await client.PostAsJsonAsync($"{url}AddTopics", model);
                resultMessage.EnsureSuccessStatusCode();
                var result = await resultMessage.Content.ReadAsStringAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                logger.Fatal(e, "Произошла фатальная ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}
