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
    public class VideoService : IVideoService
    {
        IUnitOfWork _unitOfWork;
        ILogger _logger;
        ILMSMaps _mapper;

        public VideoService(IUnitOfWork unitOfWork, ILoggerFactory loggerFactory, ILMSMaps mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = loggerFactory.CreateLogger<VideoService>();
            _mapper = mapper;

            if (!_mapper.Initialized)
                _mapper.Configure();
        }

        public async Task<IEnumerable<VideoDto>> GetAllAsync()
        {
            IEnumerable<Video> videos = await _unitOfWork.VideoRepository.GetAllAsync();
            return DTOAssemblerFactory<IEnumerable<VideoDto>, IEnumerable<Video>>.MakeAssembler().AssembleDTO(videos);
        }
    }
}
