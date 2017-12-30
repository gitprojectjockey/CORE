using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LibraryData.Entities;
using LMS.Data.UnitOfWork;
using LMS.DataTransfer.Factories;
using LMS.DataTransfer.ObjectMaps;
using LMS.DataTransfer.Objects;
using Microsoft.Extensions.Logging;

namespace LMS.Services
{
    public class LibraryBranchService : ILibraryBranchService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly ILMSMaps _mapper;
        public LibraryBranchService(IUnitOfWork unitOfWork, ILoggerFactory loggerFactory, ILMSMaps mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = loggerFactory.CreateLogger<CheckoutService>();
            _mapper = mapper;

            if (!_mapper.Initialized)
                _mapper.Configure();
        }

        public async Task<IEnumerable<LibraryBranchDto>> GetAllAsync()
        {
            IEnumerable<LibraryBranch> branches = await _unitOfWork.LibraryBranchRepository.GetAsync(null, null, Regex.Replace("LibraryAssets,Patrons", @"\s", string.Empty));
            return DTOAssemblerFactory<IEnumerable<LibraryBranchDto>, IEnumerable<LibraryBranch>>.MakeAssembler().AssembleDTO(branches);
        }

        public async Task<LibraryBranchDto> GetAsync(int id)
        {
            IEnumerable<LibraryBranch> branches = await _unitOfWork.LibraryBranchRepository.GetAsync(null, null, Regex.Replace("LibraryAssets,Patrons", @"\s", string.Empty));
            var branch = branches.FirstOrDefault(br => br.Id == id);
            return DTOAssemblerFactory<LibraryBranchDto, LibraryBranch>.MakeAssembler().AssembleDTO(branch);
        }

        public async Task<IEnumerable<BranchHourDto>> GetBranchHoursAsync(int branchId)
        {
            IEnumerable<BranchHour> branchHours = await _unitOfWork.BranchHourRepository
                .GetAsync(br => br.Branch.Id == branchId, br => br.OrderBy(ob => ob.DayOfWeek), Regex.Replace("Branch", @"\s", string.Empty));

            return DTOAssemblerFactory<IEnumerable<BranchHourDto>, IEnumerable<BranchHour>>.MakeAssembler().AssembleDTO(branchHours);
        }

        public int GetAssetCount(IEnumerable<LibraryAssetDto> libraryAssets) => libraryAssets != null ? libraryAssets.Count() : 0;

        public decimal GetAssetsValueAsync(IEnumerable<LibraryAssetDto> libraryAssets) => libraryAssets != null ? libraryAssets.Sum(a => a.Cost) : 0m;

        public int GetPatronCount(IEnumerable<PatronDto> patrons) => patrons != null ? patrons.Count() : 0;

        public bool IsBranchOpen(int branchId)
        {
            return true;
        }

        public Task<IEnumerable<LibraryAssetDto>> GetAssetsAsync(int branchId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PatronDto>> GetPatronsAsync(int branchId)
        {
            throw new NotImplementedException();
        }

        public void Create(LibraryBranchDto newBranch)
        {
            throw new NotImplementedException();
        }
    }
}
