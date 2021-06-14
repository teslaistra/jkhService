using System;
using System.Threading.Tasks;
using Forms.Domain.Entities;
using Forms.Domain.Interfaces;
using Forms.Infrastructure.DTO;
using Complaints.Domain.Interfaces;
using System.Linq;

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
                throw new ArgumentNullException(nameof(form));
            }
            FormDTO form_w_geo = await _IGeoProvider.GetAdress(new FormDTO(form));
            await _IformRepository.AddForm(form_w_geo);
        }

        public async Task EditForm(Form form)
        {
            if (form == null)
            {
                throw new ArgumentNullException(nameof(form));
            }
            if (form.Adress != "")
            {
                FormDTO form_w_NewGeo = await _IGeoProvider.GetAdress(new FormDTO(form));
                await _IformRepository.EditForm(form_w_NewGeo);
            }
            else
            {
                await _IformRepository.EditForm(new FormDTO(form));
            }
        }

        public async Task<Form[]> GetForms(Form form)
        {
            if (form == null)
            {
                throw new ArgumentNullException(nameof(form));
            }
            return (await _IformRepository.GetForms(new FormDTO(form))).Select(form => new Form(form)).ToArray();
        }
    }
}
