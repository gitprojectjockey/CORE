using LMS.DataTransfer.Objects;
using LMS.DataTransfer.ResourceModels;
using LMS.Services;
using LMS.WebApi.Filters.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LMS.WebApi.Controllers
{
    [ValidateModel]
    public class CatalogController : Controller
    {
        private readonly ILibraryAssetService _libraryAssetService;
        private readonly ICheckoutService _checkoutService;
        private readonly IBookService _bookService;
        private readonly IVideoService _videoService;
        private readonly ILogger _logger;
        private readonly LMSConfigurations _lmsConfigOptions;
        public CatalogController(ILibraryAssetService libraryAssetService,
                                 ICheckoutService checkoutService,
                                 IBookService bookService,
                                 IVideoService videoService,
                                 ILoggerFactory loggerFactory,
                                 IOptions<LMSConfigurations> lmsConfig)
        {
            _libraryAssetService = libraryAssetService;
            _checkoutService = checkoutService;
            _bookService = bookService;
            _videoService = videoService;
            _logger = loggerFactory.CreateLogger<CatalogController>();
            _lmsConfigOptions = lmsConfig.Value;
        }

        [HttpGet()]
        [Route("api/catalog")]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<LibraryAssetDto> assets = await _libraryAssetService.GetAllAsync();
            var result = new JsonResult(assets)
            {
                ContentType = "application/json",
                StatusCode = (int)HttpStatusCode.OK
            };
            return result;
        }

        [HttpGet()]
        [Route("api/catalog/{assetId}")]
        public async Task<IActionResult> Get(int assetId)
        {
            LibraryAssetDto asset = await _libraryAssetService.GetAsync(assetId);
            var result = new JsonResult(asset)
            {
                ContentType = "application/json",
                StatusCode = (int)HttpStatusCode.OK
            };
            return result;
        }

        [HttpGet()]
        [Route("api/catalog/detail/{assetId}")]
        public async Task<IActionResult> Detail(int assetId)
        {
            var currentHolds = new List<AssetHoldResourceModel>();
            LibraryAssetDto asset = await _libraryAssetService.GetAsync(assetId);
            IEnumerable<HoldDto> holds = await _checkoutService.GetCurrentHoldsAsync(assetId);

            foreach (var hold in holds)
            {
                var currentHold = new AssetHoldResourceModel()
                {
                    HoldPlaced = await _checkoutService.GetCurrentHoldPlacedAsync(hold.Id),
                    PatronName = await _checkoutService.GetCurrentHoldPatronAsync(hold.Id)
                };
                currentHolds.Add(currentHold);
            }

            LibraryBranchDto libraryBranch = await _libraryAssetService.GetCurrentLocationAsync(assetId);

            var resourceModel = new AssetDetailResourceModel
            {
                AssetId = assetId,
                Title = asset.Title,
                Type = await _libraryAssetService.GetAssetTypeAsync(assetId),
                Year = asset.Year,
                Cost = asset.Cost,
                Status = asset.Status.Name,
                ImageUrl = asset.ImageUrl,
                AuthorOrDirector = await _libraryAssetService.GetAuthorOrDirectorAsync(assetId),
                CurrentLocation = libraryBranch.Name,
                Dewey = await _libraryAssetService.GetDeweyIndexAsync(assetId),
                CheckoutHistory = await _checkoutService.GetCheckoutHistoryAsync(assetId),
                CurrentAssociatedLibraryCard = await _libraryAssetService.GetLibraryCardByAssetIdAsync(assetId),
                ISBN = await _libraryAssetService.GetIsbnAsync(assetId),
                LatestCheckout = await _libraryAssetService.GetLastestAssetCheckoutAsync(assetId),
                CurrentHolds = currentHolds,
                PatronName = await _checkoutService.GetCurrentPatronAsync(assetId)
            };

            var result = new JsonResult(resourceModel)
            {
                ContentType = "application/json",
                StatusCode = (int)HttpStatusCode.OK
            };

            return result;
        }

        [HttpGet]
        [Route("api/catalog/get/{assetId}/title")]
        public async Task<IActionResult> GetAssetTitle(int assetId)
        {
            string title = await _libraryAssetService.GetTitleAsync(assetId);
            return new JsonResult(title)
            {
                ContentType = "application/json",
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        [HttpGet]
        [Route("api/catalog/get/{assetId}/type")]
        public async Task<IActionResult> GetAssetType(int assetId)
        {
            string type = await _libraryAssetService.GetAssetTypeAsync(assetId);
            return new JsonResult(type)
            {
                ContentType = "application/json",
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        [HttpGet]
        [Route("api/catalog/checkouts/get/{assetId}")]
        public async Task<IActionResult> GetCheckout(int assetId)
        {
            LibraryAssetDto asset = await _libraryAssetService.GetAsync(assetId);
            var assetCheckoutResourceModel = new AssetCheckoutResourceModel()
            {
                AssetId = asset.Id,
                ImageUrl = asset.ImageUrl,
                Title = asset.Title,
                LibraryCardId = "",
                IsCheckedOut = await _checkoutService.IsCheckedOutAsync(assetId)
            };

            return new JsonResult(assetCheckoutResourceModel)
            {
                ContentType = "application/json",
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        [HttpGet]
        [Route("api/catalog/holds/get/{assetId}")]
        public async Task<IActionResult> GetHolds(int assetId)
        {
            LibraryAssetDto asset = await _libraryAssetService.GetAsync(assetId);
            IEnumerable<HoldDto> holds = await _checkoutService.GetCurrentHoldsAsync(assetId);
            var assetCheckoutResourceModel = new AssetCheckoutResourceModel()
            {
                AssetId = asset.Id,
                ImageUrl = asset.ImageUrl,
                Title = asset.Title,
                LibraryCardId = "",
                HoldCount = holds.Count()
            };

            return new JsonResult(assetCheckoutResourceModel)
            {
                ContentType = "application/json",
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        [HttpGet]
        [Route("api/catalog/books/get/{bookId}")]
        public async Task<IActionResult> GetBook(int bookId)
        {
            BookDto book = await _bookService.GetAsync(bookId);
            return new JsonResult(book)
            {
                ContentType = "application/json",
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        [HttpGet]
        [Route("api/catalog/books/get")]
        public async Task<IActionResult> GetBooks()
        {
           IEnumerable<BookDto> books = await _bookService.GetAllAsync();
            return new JsonResult(books)
            {
                ContentType = "application/json",
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        [HttpGet]
        [Route("api/catalog/books/get/{bookId}/deweyIndex")]
        public async Task<IActionResult> GetDeweyIndex(int bookId)
        {
            string deweyIndex = await _libraryAssetService.GetDeweyIndexAsync(bookId);
            return new JsonResult(deweyIndex)
            {
                ContentType = "application/json",
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        [HttpGet]
        [Route("api/catalog/books/get/{bookId}/isbn")]
        public async Task<IActionResult> GetIsbn(int bookId)
        {
            string isbn = await _libraryAssetService.GetIsbnAsync(bookId);
            return new JsonResult(isbn)
            {
                ContentType = "application/json",
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        [HttpGet]
        [Route("api/catalog/videos/get")]
        public async Task<IActionResult> GetVideos()
        {
            IEnumerable<VideoDto> videos = await _videoService.GetAllAsync();
            return new JsonResult(videos)
            {
                ContentType = "application/json",
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        [HttpGet]
        [Route("api/catalog/get/{assetId}/libraryCard")]
        public async Task<IActionResult> GetLibraryCard(int assetId)
        {
            LibraryCardDto card = await _libraryAssetService.GetLibraryCardByAssetIdAsync(assetId);
            return new JsonResult(card)
            {
                ContentType = "application/json",
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        [HttpGet]
        [Route("api/catalog/get/{assetId}/authurOrDirectory")]
        public async Task<IActionResult> GetAuthorOrDirectory(int assetId)
        {
            string name = await _libraryAssetService.GetAuthorOrDirectorAsync(assetId);
            return new JsonResult(name)
            {
                ContentType = "application/json",
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        [HttpGet]
        [Route("api/catalog/get/{assetId}/currentBranchLocation")]
        public async Task<IActionResult> GetAssetCurrentBranchLocation(int assetId)
        {
            LibraryBranchDto branch = await _libraryAssetService.GetCurrentLocationAsync(assetId);
            return new JsonResult(branch)
            {
                ContentType = "application/json",
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        [HttpPost]
        [Route("api/catalog/asset/checkin/{assetId}/{libraryCardId}")]
        public async Task CheckIn(int assetId, int libraryCardId)
        {
            await _checkoutService.CheckInItemAsync(assetId, libraryCardId);
        }

        [HttpPost]
        [Route("api/catalog/asset/checkout/{assetId}/{libraryCardId}")]
        public async Task PlaceCheckout(int assetId, int libraryCardId)
        {
             await _checkoutService.CheckoutItemAsync(assetId, libraryCardId);
        }

        [HttpPost]
        [Route("api/catalog/asset/markLost/{assetId}")]
        public async Task<IActionResult> MarkLost(int assetId)
        {
            await _checkoutService.MarkLostAsync(assetId);
            return RedirectToAction("Detail", new { id = assetId });
        }

        [HttpPost]
        [Route("api/catalog/asset/markFound/{assetId}")]
        public async Task<IActionResult> MarkFound(int assetId)
        {
            await _checkoutService.MarkFoundAsync(assetId);
            return RedirectToAction("Detail", new { id = assetId });
        }

        [HttpPost]
        [Route("api/catalog/asset/hold/{assetId}/{libraryCardId}")]
        public async Task<IActionResult> PlaceHold(int assetId, int libraryCardId)
        {
            await _checkoutService.PlaceHoldAsync(assetId, libraryCardId);
            return RedirectToAction("Detail", new { id = assetId });
        }

        [HttpPost]
        [Route("api/catalog/asset")]
        public IActionResult Create([FromBody]LibraryAssetDto libraryAsset)
        {
            //TODO: Implement Create
            return View();
        }

        public IActionResult Edit()
        {
            //TODO: Implement Edit
            return View();
        }

        public IActionResult Delete()
        {
            //TODO: Implement Delete
            return View();
        }

    }
}
