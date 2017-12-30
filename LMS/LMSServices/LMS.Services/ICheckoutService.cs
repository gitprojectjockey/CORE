using LibraryData.Entities;
using LMS.DataTransfer.Objects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Services
{
    public interface ICheckoutService
    {
        Task<IEnumerable<CheckoutDto>> GetAllAsync();
        Task<IEnumerable<CheckoutHistoryDto>> GetCheckoutHistoryAsync(int id);
        Task<IEnumerable<HoldDto>> GetCurrentHoldsAsync(int id);
        Task<string> GetCurrentPatronAsync(int id);
        Task<CheckoutDto> GetAsync(int id);
        Task<CheckoutDto> GetLatestCheckoutAsync(int id);

        Task Create(Checkout newCheckout);
        Task PlaceHoldAsync(int assetId, int libraryCardId);
        Task CheckoutItemAsync(int assetId, int libraryCardId);
        Task CheckInItemAsync(int assetId,int libraryCardId);
        Task CheckoutFromEarliestHoldAsync(int id, IEnumerable<Hold> currentHolds);
       

        Task MarkLostAsync(int id);
        Task MarkFoundAsync(int id);

        Task<int> GetNumberOfCopiesAsync(int id);
        Task<int> GetAvailableCopiesAsync(int id);

        Task<bool> IsCheckedOutAsync(int id);

        Task<string> GetCurrentHoldPatronAsync(int id);
        Task<string> GetCurrentHoldPlacedAsync(int id);
    }
}
