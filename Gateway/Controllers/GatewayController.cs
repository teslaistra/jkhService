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

        private readonly Logger logger = new LoggerConfiguration()
                   .WriteTo.Sentry("https://8472251de833404e9ecd48cdfeb6ed00@o661932.ingest.sentry.io/5764923")
                   .Enrich.FromLogContext()
                   .CreateLogger();
        public GatewayController(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        // GET: api/Gateway
        [Route("api/v1/TopicsGet")]
        [HttpGet]
        public async Task<IActionResult> GetTopics()
        {
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
