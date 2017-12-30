using AutoMapper;
using LibraryData.Entities;
using LMS.DataTransfer.Objects;

namespace LMS.DataTransfer.Assemblers.EntityToDto
{
    public class VideoDtoAssembler : DTOAssemblerBase<VideoDto,Video>
    {
        public override VideoDto AssembleDTO(Video source)
        {
            return Mapper.Map<Video, VideoDto>(source);
        }
    }
}
