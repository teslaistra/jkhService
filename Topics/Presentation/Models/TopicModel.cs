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

        public TopicModel()
        {

        }

        public TopicModel(Topic topic)
        {
            Name = topic?.Name ?? throw new ArgumentNullException(nameof(topic.Name));
            Description = topic?.Description ?? throw new ArgumentNullException(nameof(topic.Description));
        }

        public Topic ToEntity()
        {
            return new Topic()
            {
                Description = this.Description,
                Name = this.Name
            };
        } 

    }
}
