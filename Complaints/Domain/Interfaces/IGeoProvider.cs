using Forms.Infrastructure.DTO;
using System.Threading.Tasks;

namespace Complaints.Domain.Interfaces
{
    public interface IGeoProvider
    {
        Task<FormDTO> GetAdress(FormDTO form);
    }
}
