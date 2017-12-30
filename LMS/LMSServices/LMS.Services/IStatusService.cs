using LibraryData.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Services
{
    public interface IStatusService
    {
        Task<IEnumerable<Status>> GetAllAsync();
        Task<Status> GetAsync(int id);
        void Create(Status newStatus);
    }
}
