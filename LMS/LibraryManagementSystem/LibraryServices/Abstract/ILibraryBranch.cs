using LibraryServices.ServiceModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryServices.Abstract
{
    public interface ILibraryBranch
    {
        Task<IEnumerable<LibraryBranchDetailModel>> GetAllAsync();
        Task<LibraryBranchModel> GetByIdAsync(int id);
        decimal GetLibraryAssetsValue(IEnumerable<LibraryAssetModel> LibraryAssets);
    }
}
