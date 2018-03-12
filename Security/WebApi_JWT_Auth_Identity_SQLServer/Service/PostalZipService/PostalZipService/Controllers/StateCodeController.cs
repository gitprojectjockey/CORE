using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostalService.Data.Entities;
using PostalService.Data.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PostalZipService.Controllers
{
    [Route("api/[controller]")]
    public class ZipCodeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ZipCodeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        [HttpGet,Route("Get")]
        public async Task<IActionResult> GetAsync()
        {
            var currentUser = HttpContext.User;
            IEnumerable<State> states = null;
            if (currentUser.HasClaim(c => c.Type == "user.sales.region"))
            {
                var salesRegion = currentUser.Claims.FirstOrDefault(c => c.Type == "user.sales.region").Value;
                states = await _unitOfWork.State.GetByUsRegionAsync(salesRegion);
                var x = await _unitOfWork.ZipCode.GetPagedZipCodes(1,10);
                var resultSuccess = new JsonResult(states)
                {
                    ContentType = "application/json",
                    StatusCode = (int)HttpStatusCode.OK
                };

                return resultSuccess;
            }
            else
            {
                var message = "User claim (US Region) was not found in claims token.";
                return new JsonResult(message)
                {
                     StatusCode = (int)HttpStatusCode.Forbidden,
                     ContentType = "application/json",
                };
            }
        }
    }
}