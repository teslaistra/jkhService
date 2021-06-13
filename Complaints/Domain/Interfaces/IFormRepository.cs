using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Complaints;
using Forms.Domain.Entities;
using Forms.Infrastructure.DTO;

namespace Forms.Domain.Interfaces
{
    public interface IFormRepository
    {
        Task AddForm(Forms.Infrastructure.DTO.FormDTO topic);
        Task EditForm(FormDTO topic);
        //Task<FormDTO[]> GetForms(FormDTO topic);
    }
}