using LMS.DataTransfer.Objects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Services
{
    public interface ILibraryCardService
    {
        Task<IEnumerable<LibraryCardDto>> GetAllAsync();
    }
}
