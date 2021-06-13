using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Topics.Domain.Entities;

namespace Topics.Infrastructure.DTO
{
    public class TopicDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Topic ToEntity()
        {
            return new Topic()
            {
                Name = Name,
                Description = Description,
                Id = Id
            };
        }
    }
}
