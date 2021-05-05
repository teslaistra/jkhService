using jkhService.Domain.Interfaces;
using jkhService.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jkhService.Presentation.Controllers
{   [ApiController]
    [Route("topics")]
    public class jkhController: ControllerBase 
    {
        private readonly ITopicsService _topicsService;
        public jkhController(ITopicsService topicsService)
        {
            _topicsService = topicsService ?? throw new ArgumentNullException(nameof(topicsService));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok((await _topicsService.GetTopics()).Select(topic => new TopicsModel(topic)));
        }
    }
}
