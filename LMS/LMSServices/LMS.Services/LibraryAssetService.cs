using System.Collections.Generic;
using LibraryData.Entities;
using LMS.DataTransfer.Objects;
using LMS.Data.UnitOfWork;
using Microsoft.Extensions.Logging;
using LMS.DataTransfer.Factories;
using System.Text.RegularExpressions;
using System.Linq;
using LMS.DataTransfer.ObjectMaps;
using System.Threading.Tasks;
using LMS.DataTransfer.ResourceModels;

namespace LMS.Services
{
    public class LibraryAssetService : ILibraryAssetService
    {
        IUnitOfWork _unitOfWork;
        ILogger _logger;
        ILMSMaps _mapper;
        ICheckoutService _checkoutService;
        IStatusService _statusService;
        public LibraryAssetService(ICheckoutService checkoutService,IStatusService statusService, IUnitOfWork unitOfWork, ILoggerFactory loggerFactory, ILMSMaps mapper)
        {
            _checkoutService = checkoutService;
            _statusService = statusService;
            _unitOfWork = unitOfWork;
            _logger = loggerFactory.CreateLogger<LibraryAssetService>();
            _mapper = mapper;

            if (!_mapper.Initialized)
                _mapper.Configure();
        }

        public void Create(LibraryAsset newAsset)
        {
            _unitOfWork.LibraryAssetRepository.InsertAsync(newAsset);
            _unitOfWork.SaveAsync();
        }

        public async Task<LibraryAssetDto> GetAsync(int id)
        {
            IEnumerable<LibraryAsset> libraryAssets = await _unitOfWork.LibraryAssetRepository.
                       GetAsync(la => la.Id == id, null, Regex.Replace("Status, Location", @"\s", string.Empty));

            LibraryAsset libraryAsset = libraryAssets.FirstOrDefault();

           // libraryAsset.Status = GetCurrentStatus(libraryAsset);

            return DTOAssemblerFactory<LibraryAssetDto, LibraryAsset>.MakeAssembler().AssembleDTO(libraryAsset);
        }

        public async Task<IEnumerable<LibraryAssetDto>> GetAllAsync()
        {
            IEnumerable<LibraryAsset> libraryAssets = await _unitOfWork.LibraryAssetRepository
                    .GetAsync(null, null, Regex.Replace("Status, Location", @"\s", string.Empty));

            return DTOAssemblerFactory<IEnumerable<LibraryAssetDto>, IEnumerable<LibraryAsset>>
                    .MakeAssembler()
                    .AssembleDTO(libraryAssets);
        }

        public async Task<string> GetAuthorOrDirectorAsync(int id)
        {
            IEnumerable<LibraryAsset> assets = await _unitOfWork.LibraryAssetRepository.GetAllAsync();

            bool isBook = assets.OfType<Book>().Where(a => a.Id == id).Any();
            bool isVideo = assets.OfType<Video>().Where(a => a.Id == id).Any();
            Book book = await _unitOfWork.BookRepository.GetAsync(id);
            Video video = await _unitOfWork.VideoRepository.GetAsync(id);
            return isBook ? book.Author : video.Director ?? "Unknown";
        }

        public async Task<LibraryBranchDto> GetCurrentLocationAsync(int id)
        {
            LibraryAsset asset = await _unitOfWork.LibraryAssetRepository.GetAsync(id);
            return DTOAssemblerFactory<LibraryBranchDto, LibraryBranch>.MakeAssembler().AssembleDTO(asset.Location);
        }

        public async Task<AssetCheckoutResourceModel> GetLastestAssetCheckoutAsync(int id)
        {
            LibraryAsset asset = await _unitOfWork.LibraryAssetRepository.GetAsync(id);
            AssetCheckoutResourceModel checkoutResourceModel = new AssetCheckoutResourceModel()
            {
                AssetId = id,
                ImageUrl = asset.ImageUrl,
                Title = asset.Title,
                LibraryCardId = "",
                IsCheckedOut = await _checkoutService.IsCheckedOutAsync(id)
            };

            return checkoutResourceModel;
        }

        public async Task<string> GetDeweyIndexAsync(int bookId)
        {
            Book book = await _unitOfWork.BookRepository.GetAsync(bookId);
            return book != null ? book.DeweyIndex : "Unknown";
        }

        public async Task<string> GetIsbnAsync(int bookId)
        {
            Book book = await _unitOfWork.BookRepository.GetAsync(bookId);
            return book != null ? book.ISBN : "Unknown";
        }

        public async Task<LibraryCardDto> GetLibraryCardByAssetIdAsync(int assetId)
        {
            IEnumerable<Checkout> checkouts = await _unitOfWork.CheckoutRepository.GetAsync(c => c.LibraryAsset.Id == assetId,null, Regex.Replace("LibraryCard,LibraryAsset", @"\s", string.Empty));
            var card = checkouts.FirstOrDefault()?.LibraryCard;
            return DTOAssemblerFactory<LibraryCardDto, LibraryCard>.MakeAssembler().AssembleDTO(card);
        }

        public async Task<string> GetTitleAsync(int id)
        {
            LibraryAsset asset = await _unitOfWork.LibraryAssetRepository.GetAsync(id);
            return asset.Title;
        }

        public async Task<string> GetAssetTypeAsync(int id)
        {
            IEnumerable<LibraryAsset> assets  = await _unitOfWork.LibraryAssetRepository.GetAllAsync();
            var books = assets.OfType<Book>().Where(a => a.Id == id);
            return books.Any() ? "Book" : "Video";
        }

       
    }
}
