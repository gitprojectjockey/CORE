using EWN.Data.Repo.Core;
using LibraryData.Entities;
using LMS.Data.DataContext;
using System;
using System.Threading.Tasks;

namespace LMS.Data.UnitOfWork
{
    //The unit of work class serves one purpose: to make sure that when you use multiple repositories, 
    //they share a single database context.That way, when a unit of work is complete you can call the 
    //SaveChanges method on that instance of the context and be assured that all related changes will be coordinated.

    public class UnitOfWork : IUnitOfWork
    {
        private readonly LMSContext _context;
        private bool disposed = false;

        public UnitOfWork(LMSContext context)
        {
            _context = context;
        }

        public IRepository<Patron> PatronRepository => new Repository<Patron>(_context);
        public IRepository<LibraryAsset> LibraryAssetRepository => new Repository<LibraryAsset>(_context);
        public IRepository<LibraryBranch> LibraryBranchRepository => new Repository<LibraryBranch>(_context);
        public IRepository<CheckoutHistory> CheckoutHistoryRepository => new Repository<CheckoutHistory>(_context);
        public IRepository<Checkout> CheckoutRepository => new Repository<Checkout>(_context);
        public IRepository<Hold> HoldRepository => new Repository<Hold>(_context);
        public IRepository<Book> BookRepository => new Repository<Book>(_context);
        public IRepository<Video> VideoRepository => new Repository<Video>(_context);
        public IRepository<LibraryCard> LibraryCardRepository => new Repository<LibraryCard>(_context);
        public IRepository<Status> StatusRepository => new Repository<Status>(_context);
        public IRepository<BranchHour> BranchHourRepository  => new Repository<BranchHour>(_context);


        public async Task SaveAsync()
        {
           await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
