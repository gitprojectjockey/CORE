using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using LMS.DataTransfer.Objects;
using LMS.DataTransfer.ResourceModels;
using LMS.Services;
using LMS.WebApi.Filters.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LMS.WebApi.Controllers
{
    [ValidateModel]
    public class BranchController : Controller
    {
        private readonly ILibraryBranchService _libraryBranchService;
        private readonly ILogger _logger;
        private readonly LMSConfigurations _lmsConfigOptions;
        public BranchController( ILibraryBranchService libraryBranchService,
                                 ILoggerFactory loggerFactory,
                                 IOptions<LMSConfigurations> lmsConfig)
        {
            _libraryBranchService = libraryBranchService;
            _logger = loggerFactory.CreateLogger<CatalogController>();
            _lmsConfigOptions = lmsConfig.Value;
        }

       
        [HttpGet]
        [Route("api/branches")]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<LibraryBranchDto> branches = await _libraryBranchService.GetAllAsync();

            List<BranchDetailResourceModel> branchModels = new List<BranchDetailResourceModel>();
            foreach (var branch in branches)
            {
                var branchHours = await _libraryBranchService.GetBranchHoursAsync(branch.Id);

                BranchDetailResourceModel branchDetail = new BranchDetailResourceModel()
                {
                    Id = branch.Id,
                    BranchName = branch.Name,
                    NumberOfAssets = _libraryBranchService.GetAssetCount(branch.LibraryAssets),
                    NumberOfPatrons = _libraryBranchService.GetPatronCount(branch.Patrons),
                    IsOpen = _libraryBranchService.IsBranchOpen(branch.Id),
                    HoursOpen = branchHours,
                    HumanizedBranchHours = LMS.Services.Helpers.DataHelper.HumanizeBusinessHours(branchHours),
                    Address = branch.Address,
                    TotalAssetValue = _libraryBranchService.GetAssetsValueAsync(branch.LibraryAssets),
                    BranchOpenedDate = branch.OpenDate.ToShortDateString(),
                    Description = branch.Description,
                    ImageUrl = branch.ImageUrl,
                    Telephone = branch.Telephone
                };
                branchModels.Add(branchDetail);
            }

            return new JsonResult(branchModels)
            {
                ContentType = "application/json",
                StatusCode = (int)HttpStatusCode.OK
            };
        }
       
        [HttpGet()]
        [Route("api/branches/{branchId}")]
        public async Task<IActionResult> Get(int branchId)
        {
            LibraryBranchDto branch = await _libraryBranchService.GetAsync(branchId);
            return new JsonResult(branch)
            {
                ContentType = "application/json",
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        [HttpGet()]
        [Route("api/branches/{branchId}/hours")]
        public async Task<IActionResult> GetBranchHours(int branchId)
        {
            IEnumerable<BranchHourDto> branchHours = await _libraryBranchService.GetBranchHoursAsync(branchId);
            return new JsonResult(branchHours)
            {
                ContentType = "application/json",
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}
