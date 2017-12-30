using LibraryServices.Abstract;
using LMS.ViewModels.Branch;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Controllers
{
    public class BranchController : Controller
    {
        private readonly ILibraryBranch _branchService;
        public BranchController(ILibraryBranch branchService)
        {
            _branchService = branchService;
        }

        public async Task<IActionResult> Index()
        {
            var branchModels = await _branchService.GetAllAsync();

            var branchDetailModels = branchModels.Select(br => new BranchDetailViewModel()
            {
                Id = br.Id,
                BranchName = br.BranchName,
                NumberOfAssets = br.NumberOfAssets,
                NumberOfPatrons = br.NumberOfPatrons,
                IsOpen = br.IsOpen
            }).ToList();

            var model = new BranchIndexViewModel()
            {
                Branches = branchDetailModels
            };

            return View(model);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var branch = await _branchService.GetByIdAsync(id);
            var model = new BranchDetailViewModel
            {
                BranchName = branch.Name,
                Description = branch.Description,
                Address = branch.Address,
                Telephone = branch.Telephone,
                BranchOpenedDate = branch.OpenDate.ToString("yyyy-MM-dd"),
                NumberOfPatrons = branch.Patrons.Count(),
                NumberOfAssets = branch.LibraryAssets.Count(),
                TotalAssetValue = _branchService.GetLibraryAssetsValue(branch.LibraryAssets),
                ImageUrl = branch.ImageUrl
                //HoursOpen = _branchService.GetLibraryBranchHoursAsync(id)
            };

            return View(model);
        }

      
    }
}

