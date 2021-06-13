using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Complaints;
using Forms.Domain.Entities;

namespace Forms.Domain.Interfaces
{
    public interface IFormService
    {
        Task AddForm(Form topic);
        Task EditForm(Form topic);
    }
}