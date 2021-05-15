using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Topics.Domain.Entities;

namespace Topics.Infrastructure.DTO
{
    public class TopicDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public Topic ToEntity()
        {
            return new Topic()
            {
                Name = name,
                Description = description
            };
        }
    }
}
