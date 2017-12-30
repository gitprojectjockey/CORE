using AutoMapper;
using LibraryData.Entities;
using LMS.DataTransfer.Objects;
using System.Collections.Generic;

namespace LMS.DataTransfer.Assemblers.EntityToDto
{
    public class LibraryCardsDtoAssembler : DTOAssemblerBase<IEnumerable<LibraryCardDto>, IEnumerable<LibraryCard>>
    {
        public override IEnumerable<LibraryCardDto> AssembleDTO(IEnumerable<LibraryCard> source)
        {
            return Mapper.Map<IEnumerable<LibraryCard>, IEnumerable<LibraryCardDto>>(source);
        }
    }
}
