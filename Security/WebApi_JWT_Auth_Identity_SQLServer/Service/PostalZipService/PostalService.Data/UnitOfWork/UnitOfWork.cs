using EWN.Data.Repo.Core;
using PostalService.Data.Entities;
using PostalService.Data.Repositories;
using System;
using System.Threading.Tasks;

namespace PostalService.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GeoDataDbContext _context;
        private bool disposed = false;
        public UnitOfWork(GeoDataDbContext context)
        {
            _context = context;
        }

        public IZipCodeRepository ZipCode => new ZipCodeRepository(_context);

        public IStateCodeRepository State => new StateCodeRepository(_context);

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
