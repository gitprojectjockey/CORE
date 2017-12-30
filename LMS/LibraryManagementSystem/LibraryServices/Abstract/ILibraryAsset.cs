using LibraryServices.ServiceModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryServices.Abstract
{
    public interface ILibraryAsset
    {
        Task<IEnumerable<LibraryAssetModel>> GetAllAsync();
        Task<LibraryAssetDetailModel> GetAssetDetailsAsync(int id);
        Task<LibraryAssetModel> GetByIdAsync(int id);
        Task<IEnumerable<BookModel>> GetBookAssetsAsync();
        Task<IEnumerable<VideoModel>> GetVideoAssetsAsync();
        void AddAsync(LibraryAssetModel libraryAssetModel);
        Task<string> GetAuthorOrDirectorAsync(int id);
        Task<string> GetDeweyIndexAsync(int id);
        Task<string> GetAssetTypeAsync(int id);
        Task<string> GetAssetTitleAsync(int id);
        Task<string> GetIsbnAsync(int id);
        Task<LibraryCardModel> GetLibraryCardByAssetIdAsync(int id);
        Task<LibraryBranchModel> GetCurrentLocationAsync(int id);
        Task<string> GetAssetLocationNameAsync(int id);
    }
}
