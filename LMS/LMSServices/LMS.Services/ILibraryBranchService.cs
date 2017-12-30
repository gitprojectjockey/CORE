using LibraryData.Entities;
using LMS.DataTransfer.Objects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Services
{
    public interface ILibraryBranchService
    {
        Task<IEnumerable<LibraryBranchDto>> GetAllAsync();
        Task<IEnumerable<PatronDto>> GetPatronsAsync(int branchId);
        Task<IEnumerable<LibraryAssetDto>> GetAssetsAsync(int branchId);
        Task<LibraryBranchDto> GetAsync(int branchId);
        void Create(LibraryBranchDto newBranch);
        Task<IEnumerable<BranchHourDto>> GetBranchHoursAsync(int branchId);
        bool IsBranchOpen(int branchId);
        int GetAssetCount(IEnumerable<LibraryAssetDto> libraryAssets);
        int GetPatronCount(IEnumerable<PatronDto> patrons);
        decimal GetAssetsValueAsync(IEnumerable<LibraryAssetDto> libraryAssets);
    }
}
