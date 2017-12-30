using AutoMapper;
using LibraryData.Entities;
using LMS.DataTransfer.Objects;

namespace LMS.DataTransfer.Assemblers.EntityToDto
{
    public class BookDtoAssembler : DTOAssemblerBase<BookDto,Book>
    {
        public override BookDto AssembleDTO(Book source)
        {
            return Mapper.Map<Book, BookDto>(source);
        }
    }
}
