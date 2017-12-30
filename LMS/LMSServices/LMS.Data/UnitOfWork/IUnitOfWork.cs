using EWN.Data.Repo.Core;
using LibraryData.Entities;
using System;
using System.Threading.Tasks;

namespace LMS.Data.UnitOfWork
{
    //The unit of work class serves one purpose: to make sure that when you use multiple repositories, 
    //they share a single database context.That way, when a unit of work is complete you can call the 
    //SaveChanges method on that instance of the context and be assured that all related changes will be coordinated.
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Patron> PatronRepository{get;}
        IRepository<LibraryAsset> LibraryAssetRepository { get; }
        IRepository<LibraryBranch> LibraryBranchRepository { get; }
        IRepository<CheckoutHistory> CheckoutHistoryRepository { get; }
        IRepository<Checkout> CheckoutRepository { get; }
        IRepository<Hold> HoldRepository { get; }
        IRepository<Book> BookRepository { get; }
        IRepository<Video> VideoRepository { get; }
        IRepository<LibraryCard> LibraryCardRepository{ get; }
        IRepository<Status> StatusRepository { get; }
        IRepository<BranchHour> BranchHourRepository { get; }

        Task SaveAsync();
    }
}
