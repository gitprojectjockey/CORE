using AutoMapper;
using LibraryData.Entities;
using LMS.DataTransfer.Objects;
using System.Collections.Generic;

namespace LMS.DataTransfer.Assemblers.EntityToDto
{
    public class LibraryBranchHoursDtoAssembler : DTOAssemblerBase<IEnumerable<BranchHourDto>, IEnumerable<BranchHour>>
    {
        public override IEnumerable<BranchHourDto> AssembleDTO(IEnumerable<BranchHour> source)
        {
            return Mapper.Map<IEnumerable<BranchHour>, IEnumerable<BranchHourDto>>(source);
        }
    }
}
