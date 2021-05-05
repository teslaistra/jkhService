using jkhService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jkhService.Presentation.Models
{
    public class TopicModel
    {
        
        public string Name { get; set; }

        public string Description { get; set; }
    
        public TopicModel(Topics topic)
        {
            Name = topic?.Name ?? throw new ArgumentNullException(nameof(topic.Name));
            Description = topic?.Description ?? throw new ArgumentNullException(nameof(topic.Description));

        }
    }
}
