using jkhService.Domain.Entities;
using jkhService.Domain.Interfaces;
using jkhService.Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jkhService.Domain.Services
{
    public class TopicsService
    {
        public async Task<Topics[]> GetTopics()
        {
            return new Topics[]
            {
                new TopicModel(){Name = "Лифтовое оборудование", Description= "лифты и все все все"},
                new TopicModel(){Name = "Пожарное оборудование", Description = "вентеляция и прочее"}
            };
        }
    }
}
