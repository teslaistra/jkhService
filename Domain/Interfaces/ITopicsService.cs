using jkhService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jkhService.Domain.Interfaces
{
    /// <summary>
    /// Служба для работы с интерфейсами
    /// </summary>
    public interface ITopicsService
    {
        /// <summary>
        /// Возвращает список тем
        /// </summary>
        /// <returns></returns>
        Task<Topics[]> GetTopics();
    }
}
