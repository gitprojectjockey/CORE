using Microsoft.AspNetCore.Mvc;
using LibraryServices.Abstract;
using System.Linq;
using LMS.ViewModels.Catalog;
using System.Threading.Tasks;
using LibraryServices.ServiceModels;
using System.Collections.Generic;

namespace LMS.Controllers
{
    public class CatalogController : Controller
    {
        ILibraryAsset _libraryAssetService;
        ILibraryCheckout _libraryCheckoutService;

        public CatalogController(ILibraryAsset libraryAssetService, ILibraryCheckout libraryCheckoutService)
        {
            _libraryAssetService = libraryAssetService;
            _libraryCheckoutService = libraryCheckoutService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var assetModels = await _libraryAssetService.GetAllAsync();
            List<AssetIndexListingViewModel> assetIndexList = new List<AssetIndexListingViewModel>();
            foreach (var assetIndex in assetModels)
            {
                var assetIndexModel = new AssetIndexListingViewModel();
                assetIndexModel.Id = assetIndex.Id;
                assetIndexModel.ImageUrl = assetIndex.ImageUrl;
                assetIndexModel.Title = assetIndex.Title;
                assetIndexModel.AuthorOrDirector = await _libraryAssetService.GetAuthorOrDirectorAsync(assetIndex.Id);
                assetIndexModel.DeweyCallNumber = await _libraryAssetService.GetDeweyIndexAsync(assetIndex.Id);
                assetIndexModel.NumberOfCopies = assetIndex.NumberOfCopies.ToString();
                assetIndexModel.Type = await _libraryAssetService.GetAssetTypeAsync(assetIndex.Id);
                assetIndexList.Add(assetIndexModel);
            }

            var viewModel = new AssetIndexViewModel()
            {
                Assets = assetIndexList
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            LibraryAssetDetailModel assetDetails = await _libraryAssetService.GetAssetDetailsAsync(id);

            CheckoutViewModel checkoutViewModel = new CheckoutViewModel()
            {
                Title = assetDetails.Title,
                AssetId = assetDetails.AssetId,
                ImageUrl = assetDetails.ImageUrl,
                HoldCount = assetDetails.CurrentHolds.Count(),
                IsCheckedOut = assetDetails.LatestCheckout.IsCheckedOut
            };

            var holdViewModels = assetDetails.CurrentHolds.Select(ch => new AssetHoldViewModel()
            {
                PatronName = ch.PatronName,
                HoldPlaced = ch.HoldPlaced
            });

            var assetDetailViewModel = new AssetDetailViewModel()
            {
                AssetId = assetDetails.AssetId,
                Title = assetDetails.Title,
                AuthorOrDirector = assetDetails.AuthorOrDirector,
                Type = assetDetails.Type,
                Year = assetDetails.Year,
                ISBN = assetDetails.ISBN,
                DeweyCallNumber = assetDetails.Dewey,
                Status = assetDetails.Status,
                Cost = assetDetails.Cost,
                CurrentLocation = assetDetails.CurrentLocation,
                ImageUrl = assetDetails.ImageUrl,
                PatronName = assetDetails.PatronName,
                LatestCheckout = checkoutViewModel,
                CurrentAssociatedLibraryCard = assetDetails.CurrentAssociatedLibraryCard,
                CheckoutHistory = assetDetails.CheckoutHistory,
                CurrentHolds = holdViewModels
            };
            return View(assetDetailViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Checkout(int assetId)
        {
            LibraryAssetCheckoutModel assetCheckout = await _libraryCheckoutService.GetByIdAsync(assetId);

            var model = new CheckoutViewModel
            {
                AssetId = assetId,
                ImageUrl = assetCheckout.ImageUrl,
                Title = assetCheckout.Title,
                LibraryCardId = "",
                IsCheckedOut = assetCheckout.IsCheckedOut
            };

            return View("Checkout", model);
        }

        [HttpGet]
        public async Task<IActionResult> Hold(int assetId)
        {
            
            LibraryAssetCheckoutModel assetHoldTypeCheckout = await _libraryCheckoutService.GetHoldTypeCheckoutAsync(assetId);

            var model = new CheckoutViewModel
            {
                AssetId = assetId,
                ImageUrl = assetHoldTypeCheckout.ImageUrl,
                Title = assetHoldTypeCheckout.Title,
                LibraryCardId = "",
                HoldCount = assetHoldTypeCheckout.HoldCount
            };

            return View("Hold", model);
        }

        [HttpPost]
        public IActionResult PlaceCheckout(int assetId, int libraryCardId)
        {
            _libraryCheckoutService.CheckoutItem(assetId, libraryCardId);
            return RedirectToAction("Detail", new {id = assetId });
        }
        
        public IActionResult PlaceCheckIn(int assetId, int libraryCardId)
        {
            _libraryCheckoutService.CheckInItem(assetId, libraryCardId);
            return RedirectToAction("Detail", new {id = assetId });
        }

        [HttpPost]
        public IActionResult PlaceHold(int assetId, int libraryCardId)
        {
            _libraryCheckoutService.HoldItem(assetId, libraryCardId);
            return RedirectToAction("Detail", new { id = assetId });
        }



        //public IActionResult MarkLost(int id)
        //{
        //    _libraryCheckoutService.MarkLost(id);
        //    return RedirectToAction("Detail", new { id = id });
        //}

        //public IActionResult MarkFound(int id)
        //{
        //    _libraryCheckoutService.MarkFound(id);
        //    return RedirectToAction("Detail", new { id = id });
        //}



       

        //public IActionResult Create()
        //{
        //    return View();
        //}

        //public IActionResult Edit()
        //{
        //    return View();
        //}

        //public IActionResult Delete()
        //{
        //    return View();
        //}
    }
}
