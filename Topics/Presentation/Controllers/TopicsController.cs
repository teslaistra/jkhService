using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Topics.Domain.Interfaces;
using Topics.Domain.Services;
using Topics.Presentation.Models;
using Topics.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Topics.Presentation.Controllers
{
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private readonly ITopicService _topicService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<TopicsController> _logger;

        public TopicsController(ITopicService exerciseService, IConfiguration configuration, ILogger<TopicsController> logger)
        {
            _topicService = exerciseService ?? throw new ArgumentNullException(nameof(TopicsService));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }
        [Route("gettopics")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var logger = new LoggerConfiguration()
                 .WriteTo.Sentry(_configuration.GetConnectionString("SENTRY"))
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .CreateLogger();
            try
            {
                logger.Information("Запрос на получение тем");
                return Ok((await _topicService.GetTopics())
                    .Select(topic => new TopicModel(topic)));
            }
            catch (Exception e)
            {
                logger.Error(e, "Произошла ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка, обратитесь в службу поддержки!");
            }
        }

        [Route("addtopics")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TopicModel model)
        {
            var logger = new LoggerConfiguration()
                 .WriteTo.Sentry(_configuration.GetConnectionString("SENTRY"))
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .CreateLogger();
            try
            {
                logger.Information("Запрос на добавление темы");
                await _topicService.AddTopic(model.ToEntity());
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception e)
            {
                logger.Error(e, "Произошла ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка, обратитесь в службу поддержки!");
            }
        }
        [Route("deletetopics/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var logger = new LoggerConfiguration()
                 .WriteTo.Sentry(_configuration.GetConnectionString("SENTRY"))
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .CreateLogger();
            try
            {
                logger.Information("Запрос на удаление темы");
                await _topicService.DeleteTopic(int.Parse(id));
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception e)
            {
                logger.Error(e, "Произошла ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка, обратитесь в службу поддержки!");
            }
        }
        [Route("edittopics")]
        [HttpPost]
        public async Task<IActionResult> Put([FromBody] TopicModel model)
        {
            var logger = new LoggerConfiguration()
                 .WriteTo.Sentry(_configuration.GetConnectionString("SENTRY"))
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .CreateLogger();

            try
            {
                logger.Information("Запрос на изменение темы");
                await _topicService.EditTopic(model.ToEntity());
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception e)
            {
                logger.Error(e, "Произошла ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка, обратитесь в службу поддержки!");
            }
        }
    }
}
