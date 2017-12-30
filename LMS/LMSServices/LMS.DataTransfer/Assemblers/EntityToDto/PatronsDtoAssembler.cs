using System.Collections.Generic;
using LMS.DataTransfer.Objects;
using LibraryData.Entities;
using AutoMapper;

namespace LMS.DataTransfer.Assemblers.EntityToDto
{
    public class PatronsDtoAssembler : DTOAssemblerBase<IEnumerable<PatronDto>, IEnumerable<Patron>>
    {
        public override IEnumerable<PatronDto> AssembleDTO(IEnumerable<Patron> source)
        {
            return Mapper.Map<IEnumerable<Patron>, IEnumerable<PatronDto>>(source);
        }
    }
}
