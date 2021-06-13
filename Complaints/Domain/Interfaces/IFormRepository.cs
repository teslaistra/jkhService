using System.Threading.Tasks;
using Forms.Infrastructure.DTO;

namespace Forms.Domain.Interfaces
{
    public interface IFormRepository
    {
        Task AddForm(FormDTO form);
        Task EditForm(FormDTO form);
        Task<FormDTO[]> GetForms(FormDTO form);
    }
}