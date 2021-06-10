using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Topics.Domain.Entities;

namespace Topics.Presentation.Models
{
    public class TopicModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { set; get; }
        public TopicModel()
        {

        }

        public TopicModel(Topic topic)
        {
            Name = topic?.Name ?? "";
            Description = topic?.Description ?? "";
            Id = (int) topic.Id;
        }

        public Topic ToEntity()
        {
            return new Topic()
            {
                Description = this.Description,
                Name = this.Name,
                Id = this.Id
            };
        } 

    }
}
