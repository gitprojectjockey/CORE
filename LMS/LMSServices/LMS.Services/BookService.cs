using System.Collections.Generic;
using LibraryData.Entities;
using LMS.DataTransfer.Objects;
using LMS.Data.UnitOfWork;
using Microsoft.Extensions.Logging;
using LMS.DataTransfer.Factories;
using LMS.DataTransfer.ObjectMaps;
using System.Threading.Tasks;

namespace LMS.Services
{
    public class BookService : IBookService
    {
        IUnitOfWork _unitOfWork;
        ILogger _logger;
        ILMSMaps _mapper;
        public BookService(IUnitOfWork unitOfWork, ILoggerFactory loggerFactory, ILMSMaps mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = loggerFactory.CreateLogger<BookService>();
            _mapper = mapper;

            if (!_mapper.Initialized)
                _mapper.Configure();
        }
        public void Create(Book newBook)
        {
            _unitOfWork.BookRepository.InsertAsync(newBook);
            _unitOfWork.SaveAsync();
        }

        public async Task<BookDto> GetAsync(int id)
        {
            Book book = await _unitOfWork.BookRepository.GetAsync(id);
            return DTOAssemblerFactory<BookDto, Book>.MakeAssembler().AssembleDTO(book);
        }

        public async  Task<IEnumerable<BookDto>> GetAllAsync()
        {
            IEnumerable<Book> books = await _unitOfWork.BookRepository.GetAllAsync();
            return DTOAssemblerFactory<IEnumerable<BookDto>, IEnumerable<Book>>.MakeAssembler().AssembleDTO(books);
        }

        public async Task<IEnumerable<BookDto>> GetByAuthorAsync(string author)
        {
            IEnumerable<Book> books = await _unitOfWork.BookRepository.GetAsync(book => book.Author == author, null, null);
            return DTOAssemblerFactory<IEnumerable<BookDto>, IEnumerable<Book>>.MakeAssembler().AssembleDTO(books);
        }

        public async Task<IEnumerable<BookDto>> GetByISBNAsync(string isbn)
        {
            IEnumerable<Book> books = await _unitOfWork.BookRepository.GetAsync(book => book.Author == isbn, null, null);
            return DTOAssemblerFactory<IEnumerable<BookDto>, IEnumerable<Book>>.MakeAssembler().AssembleDTO(books);
        }
    }
}
