using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Topics.Domain.Entities;

namespace Topics.Domain.Interfaces
{
    public interface ITopicRepository
    {
        Task<Topic[]> GetTopics();
        Task AddTopic(Topic topic);
        Task DeleteTopic(int id);
        Task EditTopic(Topic topic);
    }
}
