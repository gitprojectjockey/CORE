using System;
using System.Collections.Generic;
using LibraryData.Entities;
using Microsoft.Extensions.Logging;
using LMS.Data.UnitOfWork;
using LMS.DataTransfer.ObjectMaps;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Services
{
    public class StatusService : IStatusService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly ILMSMaps _mapper;

        public StatusService(IUnitOfWork unitOfWork, ILoggerFactory loggerFactory, ILMSMaps mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = loggerFactory.CreateLogger<StatusService>();
            _mapper = mapper;

            if (!_mapper.Initialized)
                _mapper.Configure();
        }

        public void Create(Status newStatus)
        {
            _unitOfWork.StatusRepository.InsertAsync(newStatus);
            _unitOfWork.SaveAsync();
        }

        public async Task<Status> GetAsync(int statusId)
        {
            var statuses = await _unitOfWork.StatusRepository.GetAsync(s => s.Id == statusId);
            return statuses.FirstOrDefault();
        }

        public async Task<IEnumerable<Status>> GetAllAsync()
        {
            return await _unitOfWork.StatusRepository.GetAllAsync();
        }
    }
}
