using LibraryServices.ServiceModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryServices.Abstract
{
    public interface ILibraryCheckout
    {
        Task<LibraryAssetCheckoutModel> GetByIdAsync(int id);
        ////void AddAsync(CheckoutModel newCheckout);
        //Task<IEnumerable<CheckoutHistoryModel>> GetCheckoutHistoryAsync(int id);
        void HoldItem(int assetId, int libraryCardId);
        void CheckoutItem(int assetId, int libraryCardId);
        void CheckInItem(int assetId,int libraryCardId);
        //Task<CheckoutModel> GetLatestCheckoutAsync(int id);
        //Task<int> GetNumberOfCopiesAsync(int id);
        //Task<int> GetAvailableCopiesAsync(int id);
        //Task<bool> IsCheckedOutAsync(int id);

        //Task<string> GetCurrentHoldPatronAsync(int id);
        //Task<string> GetCurrentHoldPlacedAsync(int id);
        //Task<string> GetCurrentCheckoutPatronAsync(int id);
        Task<LibraryAssetCheckoutModel> GetHoldTypeCheckoutAsync(int id);

        //void MarkItemLost(int id);
        void MarkItemFound(int id);
    }
}
