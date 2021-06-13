using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forms.Domain.Entities;
using Forms.Domain;
using Forms.Domain.Interfaces;
using Forms.Infrastructure.DTO;
using Complaints.Domain.Interfaces;

namespace Forms.Domain.Services
{
    public class FormsService : IFormService
    {
        private readonly IFormRepository _IformRepository;
        private readonly IGeoProvider _IGeoProvider;
        public FormsService(IFormRepository repository, IGeoProvider provider)
        {
            _IformRepository = repository ?? throw new ArgumentNullException(nameof(repository));
            _IGeoProvider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public async Task AddForm(Form form)
        {
            if (form == null)
            {
                Console.WriteLine("bad argument");
                throw new ArgumentNullException(nameof(form));
            }
           await _IGeoProvider.getAdress(form);
           // await _IformRepository.AddForm(new FormDTO(form));
        }

        public async Task DeleteForm(Form form)
        {
            if (form == null)
            {
                Console.WriteLine("bad argument");
                throw new ArgumentNullException(nameof(form));
            }

            await _IformRepository.DeleteForm(form);
        }

        public async Task EditForm(Form form)
        {
            if (form == null)
            {
                Console.WriteLine("bad argument");
                throw new ArgumentNullException(nameof(form));
            }
            await _IformRepository.EditForm(form);
        }
    }
}
