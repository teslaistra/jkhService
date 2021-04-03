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
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(new TopicsModel[]
            {
                new TopicsModel(){Name = "Лифтовое оборудование", Description= "лифты и все все все"},
                new TopicsModel(){Name = "Пожарное оборудование", Description = "вентеляция и прочее"}
            });
        }
    }
}
