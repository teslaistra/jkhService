using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Topics.Domain.Entities;
using Topics.Domain.Interfaces;

namespace Topics.Domain.Services
{
    public class TopicsService : ITopicService
    {
        private readonly ITopicRepository _topicRepository;

        public TopicsService(ITopicRepository repository)
        {
            _topicRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Topic[]> GetTopics()
        {
            return await _topicRepository.GetTopics();
        }

        public async Task AddTopic(Topic topic)
        {
            if (topic == null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            await _topicRepository.AddTopic(topic);
        }

        public async Task DeleteTopic(int id)
        {
            await _topicRepository.DeleteTopic(id);
        }

        public async Task EditTopic(Topic topic)
        {
            if (topic == null)
            {
                throw new ArgumentNullException(nameof(topic));
            }
            await _topicRepository.EditTopic(topic);
        }
    }
}
