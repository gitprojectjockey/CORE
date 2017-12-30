using AutoMapper;
using LibraryData.Entities;
using LMS.DataTransfer.Objects;
using System.Collections.Generic;

namespace LMS.DataTransfer.Assemblers.EntityToDto
{
    public sealed class HoldDtoAssembler : DTOAssemblerBase<IEnumerable<HoldDto>, IEnumerable<Hold>>
    {
        public override IEnumerable<HoldDto> AssembleDTO(IEnumerable<Hold>source)
        {
            return Mapper.Map<IEnumerable<Hold>, IEnumerable<HoldDto>>(source);
        }
    }
}
