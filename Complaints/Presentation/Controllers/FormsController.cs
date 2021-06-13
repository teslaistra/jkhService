using Forms.Domain.Interfaces;
using Forms.Presentation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ILogger<FormsController> _logger;

        private readonly Logger logger = new LoggerConfiguration()
                .WriteTo.Sentry("https://8472251de833404e9ecd48cdfeb6ed00@o661932.ingest.sentry.io/5764923")
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .CreateLogger();

        public FormsController(IFormService exerciseService, ILogger<FormsController> logger)
        {
            _formsService = exerciseService ?? throw new ArgumentNullException(nameof(Domain.Services.FormsService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Route("addforms")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FormModel model)
        {
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
                .WriteTo.Sentry("https://8472251de833404e9ecd48cdfeb6ed00@o661932.ingest.sentry.io/5764923")
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

        [Route("getforms")]
        [HttpGet]
        public async Task<IActionResult> Get([FromBody] FormModel model)
        {
            try
            {
                logger.Information("Запрос на получение форм");
                return Ok((await _formsService.GetForms(model.ToEntity())).Select(Form => new FormModel(Form)));
            }
            catch (Exception e)
            {
                logger.Error(e, "Произошла ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка, обратитесь в службу поддержки!");
            }
        }
    }
}
