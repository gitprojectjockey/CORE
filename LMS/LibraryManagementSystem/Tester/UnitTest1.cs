using LibraryServices.Abstract;
using LibraryServices.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tester
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var optionsBuilder = new DbContextOptionsBuilder<LibraryData.LibraryContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=LibraryManagementSystem;Trusted_Connection=True;MultipleActiveResultSets=true");
            LibraryData.LibraryContext _context = new LibraryData.LibraryContext(optionsBuilder.Options);
            ILibraryPatron _libraryService = new LibraryPatronService(_context);


            var checkoutHistory = _libraryService.GetCheckoutHistory(4);

            var checkouts = _libraryService.GetCheckouts(4);

        }
    }
}
