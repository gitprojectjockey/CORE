using LMS.DataTransfer.Objects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Services
{
    public interface IVideoService
    {
        Task<IEnumerable<VideoDto>> GetAllAsync();
    }
}
