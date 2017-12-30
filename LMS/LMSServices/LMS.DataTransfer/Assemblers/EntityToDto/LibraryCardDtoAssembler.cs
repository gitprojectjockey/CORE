using AutoMapper;
using LibraryData.Entities;
using LMS.DataTransfer.Objects;

namespace LMS.DataTransfer.Assemblers.EntityToDto
{
    public class LibraryCardDtoAssembler : DTOAssemblerBase<LibraryCardDto,LibraryCard>
    {
        public override LibraryCardDto AssembleDTO(LibraryCard source)
        {
            return Mapper.Map<LibraryCard, LibraryCardDto>(source);
        }
    }
}
