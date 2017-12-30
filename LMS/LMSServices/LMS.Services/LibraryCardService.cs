using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LibraryData.Entities;
using LMS.Data.UnitOfWork;
using LMS.DataTransfer.Factories;
using LMS.DataTransfer.ObjectMaps;
using LMS.DataTransfer.Objects;
using Microsoft.Extensions.Logging;

namespace LMS.Services
{
    public class LibraryCardService : ILibraryCardService
    {
        IUnitOfWork _unitOfWork;
        ILogger _logger;
        ILMSMaps _mapper;

        public LibraryCardService(IUnitOfWork unitOfWork, ILoggerFactory loggerFactory, ILMSMaps mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = loggerFactory.CreateLogger<LibraryCardService>();
            _mapper = mapper;

            if (!_mapper.Initialized)
                _mapper.Configure();
        }

        public async Task<IEnumerable<LibraryCardDto>> GetAllAsync()
        {
            IEnumerable<LibraryCard> cards = await _unitOfWork.LibraryCardRepository.GetAllAsync();
            return DTOAssemblerFactory<IEnumerable<LibraryCardDto>, IEnumerable<LibraryCard>>.MakeAssembler().AssembleDTO(cards);
        }
    }
}
