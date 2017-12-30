using LibraryData.Entities;
using LMS.DataTransfer.Objects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Services
{
    public interface IPatronService
    {
        Task<IEnumerable<PatronDto>> GetAllAsync();
        Task<PatronDto> GetAsync(int id);
        void Create(PatronDto patron);
        Task<IEnumerable<CheckoutHistoryDto>> GetCheckoutHistoryAsync(int patronId);
        Task<IEnumerable<HoldDto>> GetHoldsAsync(int patronId);
        Task<IEnumerable<CheckoutDto>> GetCheckoutsAsync(int id);
        
    }
}
