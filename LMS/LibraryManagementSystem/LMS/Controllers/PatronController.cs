using LibraryServices.Abstract;
using LibraryServices.ServiceModels;
using LMS.ViewModels.Patron;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Controllers
{
    public class PatronController : Controller
    {
        private readonly ILibraryPatron _libraryPatronService;
        public PatronController(ILibraryPatron libraryPatronService)
        {
            _libraryPatronService = libraryPatronService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var allPatrons = await _libraryPatronService.GetAllAsync();

            var patronDetails = allPatrons.Select(p => new PatronDetailViewModel() {
                Id = p.Id,
                LibraryCardId = p.LibraryCard.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Address = p.Address,
                Gender = p.Gender,
                MemberSince = p.LibraryCard.Created,
                Telephone = p.Telephone,
                AssetsCheckedOut = p.LibraryCard.Checkouts,
                HomeLibrary = p.HomeLibraryBranch.Name,
                CheckoutHistory = _libraryPatronService.GetCheckoutHistoryAsync(p.Id).Result,
                Holds =  _libraryPatronService.GetHoldsAsync(p.Id).Result,
                OverdueFees = p.LibraryCard.Fees
            });

            return View("Index", new PatronIndexViewModel(patronDetails));
        }

        [HttpGet]
        public async Task<ActionResult> Detail(int id)
        {
            var patronModel = await _libraryPatronService.GetAsync(id);
            PatronDetailViewModel model = new PatronDetailViewModel()
            {
                Id = patronModel.Id,
                LibraryCardId = patronModel.LibraryCard.Id,
                FirstName = patronModel.FirstName,
                LastName = patronModel.LastName,
                Address = patronModel.Address,
                Gender = patronModel.Gender,
                MemberSince = patronModel.LibraryCard.Created,
                Telephone = patronModel.Telephone,
                AssetsCheckedOut = await _libraryPatronService.GetCheckoutsAsync(patronModel.Id),
                HomeLibrary = patronModel.HomeLibraryBranch.Name,
                CheckoutHistory = await _libraryPatronService.GetCheckoutHistoryAsync(patronModel.Id),
                Holds = await _libraryPatronService.GetHoldsAsync(patronModel.Id),
                OverdueFees = patronModel.LibraryCard.Fees
            };
            return View("Detail", model);
        }

        [HttpPost]
        public ActionResult CreateAsync([FromBody] PatronModel patron)
        {
            _libraryPatronService.AddAsync(patron);
            return RedirectToAction("Index");
        }
    }
}
