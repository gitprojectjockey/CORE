using LibraryServices.ServiceModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryServices.Abstract
{
    public interface ILibraryPatron
    {
        Task<IEnumerable<PatronModel>> GetAllAsync();
        Task<PatronModel> GetAsync(int id);
        void AddAsync(PatronModel patron);
        Task<IEnumerable<CheckoutHistoryModel>> GetCheckoutHistoryAsync(int patronId);
        Task<IEnumerable<HoldModel>> GetHoldsAsync(int patronId);
        Task<IEnumerable<CheckoutModel>> GetCheckoutsAsync(int id);
    }
}
