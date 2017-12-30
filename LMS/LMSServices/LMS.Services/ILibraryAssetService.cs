using LibraryData.Entities;
using LMS.DataTransfer.Objects;
using LMS.DataTransfer.ResourceModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Services
{
    public interface ILibraryAssetService
    {
        Task<IEnumerable<LibraryAssetDto>> GetAllAsync();
        Task<LibraryAssetDto> GetAsync(int id);
        void Create(LibraryAsset newAsset);
        Task<string> GetAuthorOrDirectorAsync(int id);
        Task<string> GetDeweyIndexAsync(int id);
        Task<string> GetAssetTypeAsync(int id);
        Task<string> GetTitleAsync(int id);
        Task<string> GetIsbnAsync(int id);
        Task<LibraryBranchDto> GetCurrentLocationAsync(int id);
        Task<LibraryCardDto> GetLibraryCardByAssetIdAsync (int id);
        Task<AssetCheckoutResourceModel> GetLastestAssetCheckoutAsync(int id);
    }
}
