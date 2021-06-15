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
    public class ComplaintController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ComplaintController> _logger;

        public ComplaintController(IConfiguration configuration, ILogger<ComplaintController> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Route("api/v1/FormAdd")]
        [HttpPost]
        public async Task<IActionResult> AddForm([FromBody] object model)
        {
            var logger = new LoggerConfiguration()
                     .WriteTo.Sentry(_configuration.GetConnectionString("SENTRY"))
                     .WriteTo.Console()
                     .Enrich.FromLogContext()
                     .CreateLogger();
            try
            {
                logger.Error("Добавление формы");
                using HttpClient client = new HttpClient();
                var uri = _configuration.GetSection("ComplaintsURI").Value;
                var resultMessage = await client.PostAsJsonAsync($"{uri}addforms", model);
                var result = await resultMessage.Content.ReadAsStringAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                logger.Fatal(e, "Произошла фатальная ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [Route("api/v1/FormEdit")]
        [HttpPost]
        public async Task<IActionResult> FormEdit([FromBody] object model)
        {
            var logger = new LoggerConfiguration()
                        .WriteTo.Sentry(_configuration.GetConnectionString("SENTRY"))
                        .WriteTo.Console()
                        .Enrich.FromLogContext()
                        .CreateLogger();
            try
            {
                logger.Error("Изменение формы");
                using HttpClient client = new HttpClient();
                var uri = _configuration.GetSection("ComplaintsURI").Value;
                var resultMessage = await client.PostAsJsonAsync($"{uri}editforms", model);
                var result = await resultMessage.Content.ReadAsStringAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                logger.Fatal(e, "Произошла фатальная ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [Route("api/v1/FormsGet/useruid={u_uid}/mundepuid={m_uid}")]
        [HttpGet]
        public async Task<IActionResult> FormsGet([FromRoute] string u_uid, string m_uid)
        {
            var logger = new LoggerConfiguration()
                        .WriteTo.Sentry(_configuration.GetConnectionString("SENTRY"))
                        .WriteTo.Console()
                        .Enrich.FromLogContext()
                        .CreateLogger();
            try
            { 
                logger.Error("Получение форм");
                using HttpClient client = new HttpClient();
                var uri = _configuration.GetSection("ComplaintsURI").Value;
                var resultMessage = await client.GetAsync($"{uri}getforms/useruid={u_uid}/mundepuid={m_uid}");
                var result = await resultMessage.Content.ReadAsStringAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                logger.Fatal(e, "Произошла фатальная ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [Route("api/v1/FormsGet/useruid={u_uid}")]
        [HttpGet]
        public async Task<IActionResult> FormsGet_user([FromRoute] string u_uid)
        {
            var logger = new LoggerConfiguration()
                        .WriteTo.Sentry(_configuration.GetConnectionString("SENTRY"))
                        .WriteTo.Console()
                        .Enrich.FromLogContext()
                        .CreateLogger();
            try
            {
                logger.Error("Получение форм по uid пользователя");
                using HttpClient client = new HttpClient();
                var uri = _configuration.GetSection("ComplaintsURI").Value;
                var resultMessage = await client.GetAsync($"{uri}getforms/useruid={u_uid}/mundepuid=0");
                var result = await resultMessage.Content.ReadAsStringAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                logger.Fatal(e, "Произошла фатальная ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [Route("api/v1/FormsGet/mundepuid={m_uid}")]
        [HttpGet]
        public async Task<IActionResult> FormsGet_mundep([FromRoute] string m_uid)
        {
            var logger = new LoggerConfiguration()
                        .WriteTo.Sentry(_configuration.GetConnectionString("SENTRY"))
                        .WriteTo.Console()
                        .Enrich.FromLogContext()
                        .CreateLogger();
            try
            {
                logger.Error("Получение форм по uid мундепа");
                using HttpClient client = new HttpClient();
                var uri = _configuration.GetSection("ComplaintsURI").Value;
                var resultMessage = await client.GetAsync($"{uri}getforms/useruid=0/mundepuid={m_uid}");
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