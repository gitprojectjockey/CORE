using LibraryData.Entities;
using System;
using System.Collections.Generic;
using LMS.DataTransfer.Objects;
using LMS.Data.UnitOfWork;
using Microsoft.Extensions.Logging;
using LMS.DataTransfer.ObjectMaps;
using LMS.DataTransfer.Factories;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LMS.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly ILMSMaps _mapper;
        public CheckoutService(IUnitOfWork unitOfWork, ILoggerFactory loggerFactory, ILMSMaps mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = loggerFactory.CreateLogger<CheckoutService>();
            _mapper = mapper;

            if (!_mapper.Initialized)
                _mapper.Configure();
        }

        public async Task Create(Checkout newCheckout)
        {
            await _unitOfWork.CheckoutRepository.InsertAsync(newCheckout);
            await _unitOfWork.SaveAsync();
        }

        public async Task<CheckoutDto> GetAsync(int id)
        {
            Checkout checkout = await _unitOfWork.CheckoutRepository.GetAsync(id);
            return DTOAssemblerFactory<CheckoutDto, Checkout>.MakeAssembler().AssembleDTO(checkout);
        }

        public async Task<IEnumerable<CheckoutDto>> GetAllAsync()
        {
            IEnumerable<Checkout> checkouts = await _unitOfWork.CheckoutRepository.GetAllAsync();
            return DTOAssemblerFactory<IEnumerable<CheckoutDto>, IEnumerable<Checkout>>.MakeAssembler().AssembleDTO(checkouts);
        }

        public async Task<CheckoutDto> GetLatestCheckoutAsync(int assetId)
        {
            IEnumerable<Checkout> checkouts = await _unitOfWork.CheckoutRepository
                .GetAsync(c => c.LibraryAsset.Id == assetId, c => c.OrderBy(ob => ob.Since), null);

            Checkout checkout = checkouts.FirstOrDefault();

            return DTOAssemblerFactory<CheckoutDto, Checkout>.MakeAssembler().AssembleDTO(checkout);
        }

        public async Task<string> GetCurrentHoldPatronAsync(int holdId)
        {
            IEnumerable<Hold> holds = await _unitOfWork.HoldRepository
                .GetAsync(h => h.Id == holdId, null,
                Regex.Replace("LibraryAsset, LibraryCard", @"\s", string.Empty));

            Hold hold = holds.FirstOrDefault();

            IEnumerable<Patron> patrons = await _unitOfWork.PatronRepository
                .GetAsync(p => p.LibraryCard.Id == hold.LibraryCard.Id, null,
                Regex.Replace("LibraryCard", @"\s", string.Empty));

            Patron patron = patrons.FirstOrDefault();

            return $"{patron.FirstName} {patron.LastName}";
        }

        public async Task<string> GetCurrentHoldPlacedAsync(int holdId)
        {
            IEnumerable<Hold> holds = await _unitOfWork.HoldRepository
               .GetAsync(h => h.Id == holdId, null,
               Regex.Replace("LibraryAsset, LibraryCard", @"\s", string.Empty));

            return holds.Select(a => a.HoldPlaced).FirstOrDefault().ToString();
        }

        public async Task<IEnumerable<HoldDto>> GetCurrentHoldsAsync(int assetId)
        {
            IEnumerable<Hold> holds = await _unitOfWork.HoldRepository
               .GetAsync(h => h.LibraryAsset.Id == assetId, null,
               Regex.Replace("LibraryAsset", @"\s", string.Empty));

            return DTOAssemblerFactory<IEnumerable<HoldDto>, IEnumerable<Hold>>.MakeAssembler().AssembleDTO(holds);
        }

        public async Task<string> GetCurrentPatronAsync(int assetId)
        {
            IEnumerable<Checkout> checkouts = await _unitOfWork.CheckoutRepository
                    .GetAsync(c => c.LibraryAsset.Id == assetId, null,
                    Regex.Replace("LibraryAsset,LibraryCard", @"\s", string.Empty));

            Checkout checkout = checkouts.FirstOrDefault();
            if (checkout == null)
            {
                return "Not checked out";
            }

            IEnumerable<Patron> patrons = await _unitOfWork.PatronRepository
                    .GetAsync(p => p.LibraryCard.Id == checkout.LibraryCard.Id, null,
                    Regex.Replace("LibraryCard", @"\s", string.Empty));

            Patron patron = patrons.FirstOrDefault();

            return $"{patron.FirstName} {patron.LastName}";
        }

        public async Task<int> GetAvailableCopiesAsync(int assetId)
        {
            var numberOfCopies = await GetNumberOfCopiesAsync(assetId);
            var checkouts = await _unitOfWork.CheckoutRepository
                .GetAsync(c => c.LibraryAsset.Id == assetId && c.LibraryAsset.Status.Name == "Checked Out");

            return (numberOfCopies - checkouts.Count());
        }

        public async Task CheckoutItemAsync(int assetId, int libraryCardId)
        {
            var checkedout = await IsCheckedOutAsync(assetId);
            if (checkedout) return;

            var assetItems = await _unitOfWork.LibraryAssetRepository
                .GetAsync(a => a.Id == assetId, null, Regex.Replace("Status", @"\s", string.Empty));

            var assetItem = assetItems.FirstOrDefault();

            _unitOfWork.LibraryAssetRepository.Update(assetItem);

            var statuses = await _unitOfWork.StatusRepository.GetAllAsync();
            assetItem.Status = statuses.FirstOrDefault(s => s.Name == "Checked Out");

            IEnumerable<LibraryCard> libraryCards = await _unitOfWork.LibraryCardRepository
                .GetAsync(lc => lc.Id == libraryCardId);

            var checkout = new Checkout
            {
                LibraryAsset = assetItem,
                LibraryCard = libraryCards.FirstOrDefault(),
                Since = DateTime.Now,
                Until = DateTime.Now.AddDays(30)
            };

            await _unitOfWork.CheckoutRepository.InsertAsync(checkout);

            var checkoutHistory = new CheckoutHistory()
            {
                CheckedOut = DateTime.Now,
                CheckedIn = null,
                LibraryAsset = assetItem,
                LibraryCard = libraryCards.FirstOrDefault()
            };

            await _unitOfWork.CheckoutHistoryRepository.InsertAsync(checkoutHistory);

            await _unitOfWork.SaveAsync();
        }

        public async Task CheckInItemAsync(int assetId, int libraryCardId)
        {

            //Update the checkout history entitis
            IEnumerable<CheckoutHistory> checkoutHistoryList = await _unitOfWork.CheckoutHistoryRepository
                .GetAsync(c => c.LibraryAsset.Id == assetId && c.CheckedIn == null, null, Regex.Replace("LibraryCard", @"\s", string.Empty));
            
            CheckoutHistory checkoutHistory = checkoutHistoryList.FirstOrDefault(ch => ch.LibraryCard.Id == libraryCardId);

            checkoutHistory.CheckedIn = DateTime.Now;
            _unitOfWork.CheckoutHistoryRepository.Update(checkoutHistory);


            //Delete checkout that just got checkedIn
            IEnumerable<Checkout> checkouts = await _unitOfWork.CheckoutRepository
                 .GetAsync(c => c.LibraryAsset.Id == assetId, null, Regex.Replace("LibraryCard", @"\s", string.Empty));

            Checkout checkout = checkouts.FirstOrDefault(c => c.LibraryCard.Id == libraryCardId);
            _unitOfWork.CheckoutRepository.Delete(checkout);


            // if there are current holds, check out the item from the earliest hold that is waiting
            IEnumerable<Hold> currentHolds= await _unitOfWork.HoldRepository
                .GetAsync(h => h.LibraryAsset.Id == assetId, null, Regex.Replace("LibraryAsset, LibraryCard", @"\s", string.Empty));
           
            if (currentHolds.Any())
            {
                await  CheckoutFromEarliestHoldAsync(assetId, currentHolds);
                return;
            }

            //Else update the asset to status to Available
            var assetItems = await _unitOfWork.LibraryAssetRepository
                .GetAsync(a => a.Id == assetId, null, Regex.Replace("Status", @"\s", string.Empty));

            var assetItem = assetItems.FirstOrDefault(a => a.Id == assetId);

            _unitOfWork.LibraryAssetRepository.Update(assetItem);

            var statuses = await _unitOfWork.StatusRepository.GetAllAsync();

            assetItem.Status = statuses.FirstOrDefault(s => s.Name == "Available");

            await _unitOfWork.SaveAsync();
        }

        public async Task CheckoutFromEarliestHoldAsync(int assetId, IEnumerable<Hold> currentHolds)
        {
            var earliestHold = currentHolds.OrderBy(h => h.HoldPlaced).FirstOrDefault();
            var card = earliestHold.LibraryCard;
            _unitOfWork.HoldRepository.Delete(earliestHold);
            await _unitOfWork.SaveAsync();
            await CheckoutItemAsync(assetId, card.Id);
        }

        public async Task<IEnumerable<CheckoutHistoryDto>> GetCheckoutHistoryAsync(int assetId)
        {
            IEnumerable<CheckoutHistory> checkoutHistory = await _unitOfWork.CheckoutHistoryRepository
                .GetAsync(ch => ch.LibraryAsset.Id == assetId, null, Regex.Replace("LibraryAsset, LibraryCard", @"\s", string.Empty));

            return DTOAssemblerFactory<IEnumerable<CheckoutHistoryDto>, IEnumerable<CheckoutHistory>>.MakeAssembler().AssembleDTO(checkoutHistory);
        }

        public async Task<bool> IsCheckedOutAsync(int assetId)
        {
            var checkouts = await _unitOfWork.CheckoutRepository
                 .GetAsync(c => c.LibraryAsset.Id == assetId, null, Regex.Replace("LibraryAsset", @"\s", string.Empty));
            return checkouts.Count() >= 1;
        }

        public async Task PlaceHoldAsync(int assetId, int libraryCardId)
        {
            var assetItems = await _unitOfWork.LibraryAssetRepository
                .GetAsync(a => a.Id == assetId, null, Regex.Replace("Status", @"\s", string.Empty));

            var assetItem = assetItems.FirstOrDefault();
            if (assetItem == null) return;

            var cards = await _unitOfWork.LibraryCardRepository.GetAsync(c => c.Id == libraryCardId);

            var card = cards.FirstOrDefault();
            if (card == null) return;

            _unitOfWork.LibraryAssetRepository.Update(assetItem);
            if (assetItem.Status.Name == "Available")
            {
                var statuses = await _unitOfWork.StatusRepository.GetAllAsync();
                assetItem.Status = statuses.FirstOrDefault(s => s.Name == "On Hold");
            }
            var hold = new Hold()
            {
                HoldPlaced = DateTime.Now,
                LibraryAsset = assetItem,
                LibraryCard = card
            };

            await _unitOfWork.HoldRepository.InsertAsync(hold);
            await _unitOfWork.SaveAsync();
        }

        public async Task MarkLostAsync(int assetId)
        {
            var assetItems = await _unitOfWork.LibraryAssetRepository.FindAsync(a => a.Id == assetId);
            var assetItem = assetItems.FirstOrDefault();
            if (assetItem != null)
            {
                _unitOfWork.LibraryAssetRepository.Update(assetItems.FirstOrDefault());
                var statuses = await _unitOfWork.StatusRepository.GetAllAsync();
                assetItem.Status = statuses.FirstOrDefault(s => s.Name == "Lost");
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task MarkFoundAsync(int assetId)
        {
            var assetItems = await _unitOfWork.LibraryAssetRepository.GetAsync(a => a.Id == assetId);
            var assetItem = assetItems.FirstOrDefault();
            if (assetItem == null) return;

            _unitOfWork.LibraryAssetRepository.Update(assetItem);

            var statuses = await _unitOfWork.StatusRepository.GetAsync(s => s.Name == "Available");
            assetItem.Status = statuses.FirstOrDefault();
            var now = DateTime.Now;

            var checkouts = await _unitOfWork.CheckoutRepository
                 .GetAsync(c => c.LibraryAsset.Id == assetId);

            var checkout = checkouts.FirstOrDefault();
            if (checkout != null)
            {
                _unitOfWork.CheckoutRepository.Delete(checkout);
            }

            var checkoutHistory = await _unitOfWork.CheckoutHistoryRepository
                .GetAsync(ch => ch.LibraryAsset.Id == assetId, null, Regex.Replace("LibraryAsset", @"\s", string.Empty));
            var coh = checkoutHistory.FirstOrDefault();
            if (coh != null)
            {
                _unitOfWork.CheckoutHistoryRepository.Update(coh);
                coh.CheckedIn = now;
            }

            await _unitOfWork.SaveAsync();
        }

        public async Task<int> GetNumberOfCopiesAsync(int assetId)
        {
            var assetItems = await _unitOfWork.LibraryAssetRepository.GetAsync(a => a.Id == assetId);
            var assetItem = assetItems.FirstOrDefault();
            return assetItem == null ? 0 : assetItem.NumberOfCopies;
        }
    }
}
