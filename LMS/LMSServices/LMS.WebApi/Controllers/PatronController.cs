using Microsoft.AspNetCore.Mvc;
using LMS.Services;
using LMS.DataTransfer.Objects;
using System.Net;
using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.Extensions.Options;
using LMS.WebApi.Filters.ActionFilters;
using System;
using System.Threading.Tasks;

namespace LMS.WebApi.Controllers
{
    [ValidateModel]
    public class PatronController : Controller
    {
        private readonly IPatronService _patronService;
        private readonly ILogger _logger;
        private readonly LMSConfigurations _lmsConfigOptions;
        public PatronController(IPatronService patronService, ILoggerFactory loggerFactory, IOptions<LMSConfigurations> lmsConfig)
        {
            _patronService = patronService;
            _logger = loggerFactory.CreateLogger<PatronController>();
            _lmsConfigOptions = lmsConfig.Value;
        }

        [HttpGet()]
        [Route("api/patrons/{id}")]
        public async Task<IActionResult> Get([Bind("FirstName,LastName,Address,DataOfBirth,Telephone,Gender,LibraryCard,HomeLibraryBranchId")] int id)
        {
            _logger.LogInformation("Calling api/patrons/{id} controller action");
            var verboseLogging = _lmsConfigOptions.VerboseLoggingOn;
            var messagePrefix = _lmsConfigOptions.MessagePrefix;
            var friendlyMessage = _lmsConfigOptions.FriendlyErrorMessage;

            PatronDto patronDto = await _patronService.GetAsync(id);

            var result = new JsonResult(patronDto)
            {
                StatusCode = (int)HttpStatusCode.OK
            };

            Request.HttpContext.Response.Headers.Add("X-Total-Count", "1");
            return result;
        }

        [HttpGet()]
        [Route("api/patrons")]
        public async Task<IActionResult> GetAll()
        {
            var patronsDto = await _patronService.GetAllAsync();
            var result = new JsonResult(patronsDto)
            {
                ContentType = "application/json",
                StatusCode = (int)HttpStatusCode.OK
            };
            Request.HttpContext.Response.Headers.Add("X-Total-Count", patronsDto.Count().ToString());
            return result;
        }

        [HttpGet]
        [Route("api/patrons/checkouthistory/{patronId}")]
        public async Task<IActionResult> GetCheckoutHistory(int patronId)
        {
            var checkoutHistory = await _patronService.GetCheckoutHistoryAsync(patronId);
            var result = new JsonResult(checkoutHistory)
            {
                ContentType = "application/json",
                StatusCode = (int)HttpStatusCode.OK
            };
            return result;
        }

        [HttpGet]
        [Route("api/patrons/checkouts/{patronId}")]
        public async Task<IActionResult> GetCheckouts(int patronId)
        {
            var checkouts = await _patronService.GetCheckoutsAsync(patronId);
            var result = new JsonResult(checkouts)
            {
                ContentType = "application/json",
                StatusCode = (int)HttpStatusCode.OK
            };
            return result;
        }

        [HttpGet]
        [Route("api/patrons/holds/{patronId}")]
        public async Task<IActionResult> GetHolds(int patronId)
        {
            var holds = await _patronService.GetHoldsAsync(patronId);
            var result = new JsonResult(holds)
            {
                ContentType = "application/json",
                StatusCode = (int)HttpStatusCode.OK
            };
            return result;
        }

        [HttpPost()]
        [Route("api/patrons")]
        public void Create([FromBody][Bind("FirstName,LastName,Address,DataOfBirth,Telephone,Gender,LibraryCard,HomeLibraryBranchId")] PatronDto patronDto)
        {
            _patronService.Create(patronDto);
            var result = new JsonResult(patronDto)
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}