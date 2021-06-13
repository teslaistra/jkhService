using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Complaints;
using Forms.Domain.Entities;

namespace Forms.Domain.Interfaces
{
    public interface IFormRepository
    {
        Task AddForm(Forms.Infrastructure.DTO.FormDTO topic);
        Task DeleteForm(Form topic);
        Task EditForm(Form topic);
    }
}