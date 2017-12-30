using System.Collections.Generic;
using LibraryData.Entities;
using LMS.Data.UnitOfWork;
using System.Linq;
using LMS.DataTransfer.Objects;
using LMS.DataTransfer.Factories;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using LMS.DataTransfer.ObjectMaps;
using System.Threading.Tasks;

namespace LMS.Services
{
    public class PatronService : IPatronService
    {
        IUnitOfWork _unitOfWork;
        ILogger _logger;
        ILMSMaps _mapper;
        public PatronService(IUnitOfWork unitOfWork, ILoggerFactory loggerFactory, ILMSMaps mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = loggerFactory.CreateLogger<PatronService>();
            _mapper = mapper;

            if (!_mapper.Initialized)
                _mapper.Configure();
        }

        public async Task<PatronDto> GetAsync(int id)
        {
            IEnumerable<Patron> patrons = await _unitOfWork.PatronRepository.GetAsync(p => p.Id == id, p => p.OrderBy(ob => ob.LastName), Regex.Replace("HomeLibraryBranch, LibraryCard", @"\s", string.Empty));
            Patron patron = patrons.FirstOrDefault();
            return DTOAssemblerFactory<PatronDto, Patron>.MakeAssembler().AssembleDTO(patron);
        }

        public async Task<IEnumerable<PatronDto>> GetAllAsync()
        {
            IEnumerable<Patron> patrons = await _unitOfWork.PatronRepository
                  .GetAsync(null, p => p.OrderBy(ob => ob.LastName),
                  Regex.Replace("HomeLibraryBranch, LibraryCard", @"\s", string.Empty));

            return DTOAssemblerFactory<IEnumerable<PatronDto>, IEnumerable<Patron>>.MakeAssembler().AssembleDTO(patrons);
        }

        public async Task<IEnumerable<CheckoutHistoryDto>> GetCheckoutHistoryAsync(int patronId)
        {
            IEnumerable<Patron> patrons = await _unitOfWork.PatronRepository
                 .GetAsync(p => p.Id == patronId, null, Regex.Replace("LibraryCard", @"\s", string.Empty));

            Patron patron = patrons.FirstOrDefault();

            IEnumerable<CheckoutHistory> checkoutHistory = await _unitOfWork.CheckoutHistoryRepository
                    .GetAsync(co => co.LibraryCard.Id == patron.LibraryCard.Id,
                    co => co.OrderByDescending(ob => ob.CheckedOut),
                    Regex.Replace("LibraryAsset,LibraryCard", @"\s", string.Empty));

            return DTOAssemblerFactory<IEnumerable<CheckoutHistoryDto>, IEnumerable<CheckoutHistory>>.MakeAssembler().AssembleDTO(checkoutHistory);
        }

        public async Task<IEnumerable<CheckoutDto>> GetCheckoutsAsync(int patronId)
        {
            IEnumerable<Patron> patrons = await _unitOfWork.PatronRepository
                    .GetAsync(p => p.Id == patronId, null, Regex.Replace("LibraryCard", @"\s", string.Empty));

            var patronCardId = patrons.FirstOrDefault().LibraryCard.Id;

            IEnumerable<Checkout> checkOuts = await _unitOfWork.CheckoutRepository
                       .GetAsync(c => c.LibraryCard.Id == patronCardId, null,
                       Regex.Replace("LibraryCard,LibraryAsset", @"\s", string.Empty));

            return DTOAssemblerFactory<IEnumerable<CheckoutDto>, IEnumerable<Checkout>>.MakeAssembler().AssembleDTO(checkOuts);
            
        }

        public async Task<IEnumerable<HoldDto>> GetHoldsAsync(int patronId)
        {
            IEnumerable<Patron> patrons = await _unitOfWork.PatronRepository
                     .GetAsync(p => p.Id == patronId, null, Regex.Replace("LibraryCard", @"\s", string.Empty));

            var patronCardId = patrons.FirstOrDefault().LibraryCard.Id;

            IEnumerable<Hold> holds = await _unitOfWork.HoldRepository
                    .GetAsync(h => h.LibraryCard.Id == patronCardId,h => h.OrderByDescending(ob => ob.HoldPlaced),
                    Regex.Replace("LibraryCard, LibraryAsset", @"\s", string.Empty));

            return DTOAssemblerFactory<IEnumerable<HoldDto>, IEnumerable<Hold>>.MakeAssembler().AssembleDTO(holds); ;
        }

        public void Create(PatronDto patronDto)
        {
            Patron patron = EntityAssemblerFactory<Patron, PatronDto>.MakeAssembler().AssembleEntity(patronDto);
            _unitOfWork.PatronRepository.InsertAsync(patron);
            _unitOfWork.SaveAsync();
        }
    }
}
