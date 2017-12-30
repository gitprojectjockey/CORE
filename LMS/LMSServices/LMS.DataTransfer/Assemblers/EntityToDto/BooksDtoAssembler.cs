using AutoMapper;
using LibraryData.Entities;
using LMS.DataTransfer.Objects;
using System.Collections.Generic;

namespace LMS.DataTransfer.Assemblers.EntityToDto
{
    public class BooksDtoAssembler : DTOAssemblerBase<IEnumerable<BookDto>,IEnumerable<Book>>
    {
        public override IEnumerable<BookDto> AssembleDTO(IEnumerable<Book> source)
        {
           return Mapper.Map<IEnumerable<Book>,IEnumerable<BookDto>>(source);
        }
    }
}
