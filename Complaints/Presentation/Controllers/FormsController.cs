using Forms.Domain.Interfaces;
using Forms.Presentation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Forms.Presentation.Controllers
{
    [ApiController]
    [Route("forms")]
    public class FormsController : ControllerBase
    {
        private readonly IFormService _formsService;
        private readonly ILogger<FormsController> _logger;

        public FormsController(IFormService exerciseService, ILogger<FormsController> logger)
        {
            _formsService = exerciseService ?? throw new ArgumentNullException(nameof(Domain.Services.FormsService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        //[HttpGet]
       
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FormModel model)
        {
/*            var logger = new LoggerConfiguration()
                .WriteTo.Sentry("https://8472251de833404e9ecd48cdfeb6ed00@o661932.ingest.sentry.io/5764923")
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .CreateLogger();*/
            try
            {
            //    logger.Information("Запрос на добавление формы");
                await _formsService.AddForm(model.ToEntity());
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception e)
            {
                //logger.Error(e, "Произошла ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка, обратитесь в службу поддержки!");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] FormModel model)
        {
          /*  var logger = new LoggerConfiguration()
                .WriteTo.Sentry("https://8472251de833404e9ecd48cdfeb6ed00@o661932.ingest.sentry.io/5764923")
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .CreateLogger();*/
            try
            {
               // logger.Information("Запрос на удаление темы");
                await _formsService.DeleteForm(model.ToEntity());
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception e)
            {
               // logger.Error(e, "Произошла ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка, обратитесь в службу поддержки!");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] FormModel model)
        {
           /* var logger = new LoggerConfiguration()
                .WriteTo.Sentry("https://8472251de833404e9ecd48cdfeb6ed00@o661932.ingest.sentry.io/5764923")
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .CreateLogger();*/
            try
            {
             //   logger.Information("Запрос на изменение темы");
                await _formsService.EditForm(model.ToEntity());
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception e)
            {
             //   logger.Error(e, "Произошла ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка, обратитесь в службу поддержки!");
            }
        }
    }
}
