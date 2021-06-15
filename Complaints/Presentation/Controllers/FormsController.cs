using Forms.Domain.Entities;
using Forms.Domain.Interfaces;
using Forms.Presentation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Forms.Presentation.Controllers
{
    [ApiController]
    public class FormsController : ControllerBase
    {
        private readonly IFormService _formsService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<FormsController> _logger;

        public FormsController(IFormService exerciseService, ILogger<FormsController> logger, IConfiguration configuration)
        {
            _formsService = exerciseService ?? throw new ArgumentNullException(nameof(Domain.Services.FormsService));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Route("addforms")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FormModel model)
        {
            var logger = new LoggerConfiguration()
                  .WriteTo.Sentry(_configuration.GetConnectionString("SENTRY"))
                  .WriteTo.Console()
                  .Enrich.FromLogContext()
                  .CreateLogger();
            try
            {
                logger.Information("Запрос на добавление формы");
                await _formsService.AddForm(model.ToEntity());
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception e)
            {
                logger.Error(e, "Произошла ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка, обратитесь в службу поддержки!");
            }
        }

        [Route("editforms")]
        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] FormModel model)
        {
            var logger = new LoggerConfiguration()
                     .WriteTo.Sentry(_configuration.GetConnectionString("SENTRY"))
                     .WriteTo.Console()
                     .Enrich.FromLogContext()
                     .CreateLogger();
            try
            {
                logger.Information("Запрос на изменение формы");
                await _formsService.EditForm(model.ToEntity());
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception e)
            {
                logger.Error(e, "Произошла ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка, обратитесь в службу поддержки!");
            }
        }

        [Route("getforms/useruid={u_uid}/mundepuid={m_uid}")]
        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] string u_uid, string m_uid)
        {
            var logger = new LoggerConfiguration()
                     .WriteTo.Sentry(_configuration.GetConnectionString("SENTRY"))
                     .WriteTo.Console()
                     .Enrich.FromLogContext()
                     .CreateLogger();
            try
            {
                logger.Information("Запрос на получение форм");
                return Ok((await _formsService.GetForms(new Form(int.Parse(u_uid),int.Parse(m_uid)))).Select(Form => new FormModel(Form)));
            }
            catch (Exception e)
            {
                logger.Error(e, "Произошла ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка, обратитесь в службу поддержки!");
            }
        }
    }
}
