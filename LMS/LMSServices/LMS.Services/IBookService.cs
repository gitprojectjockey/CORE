using LibraryData.Entities;
using LMS.DataTransfer.Objects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllAsync();
        Task<IEnumerable<BookDto>> GetByAuthorAsync(string author);
        Task<IEnumerable<BookDto>> GetByISBNAsync(string isbn);
        Task<BookDto> GetAsync(int id);
        void Create(Book newBook);
    }
}
