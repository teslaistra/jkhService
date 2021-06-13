using System.Threading.Tasks;
using Forms.Domain.Entities;

namespace Forms.Domain.Interfaces
{
    public interface IFormService
    {
        Task AddForm(Form form);
        Task EditForm(Form form);
        Task<Form[]> GetForms(Form form);
    }
}