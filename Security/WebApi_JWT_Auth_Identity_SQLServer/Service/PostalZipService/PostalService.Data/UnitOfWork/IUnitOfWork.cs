using PostalService.Data.Repositories;
using System;
using System.Threading.Tasks;

namespace PostalService.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IZipCodeRepository ZipCode { get; }
        IStateCodeRepository State { get; }
        Task SaveAsync();
    }
}
