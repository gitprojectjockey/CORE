using AutoMapper;
using LibraryData.Entities;
using LMS.DataTransfer.Objects;

namespace LMS.DataTransfer.Assemblers.EntityToDto
{
    public sealed class PatronDtoAssembler : DTOAssemblerBase<PatronDto,Patron>
    {
        public override PatronDto AssembleDTO(Patron source)
        {
            return Mapper.Map<Patron, PatronDto>(source);
        }
    }
}
