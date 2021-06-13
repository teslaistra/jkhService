﻿using Forms.Domain.Entities;
using Forms.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Complaints.Domain.Interfaces
{
    public interface IGeoProvider
    {
        Task<FormDTO> getAdress(FormDTO form); 
    }
}