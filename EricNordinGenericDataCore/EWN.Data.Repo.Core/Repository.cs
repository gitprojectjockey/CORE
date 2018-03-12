using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EWN.Data.Repo.Core
{
    public  class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        private readonly DbContext _context;
        private DbSet<TEntity> dbSet;
        public Repository(DbContext context)
        {
            _context = context;
            dbSet = _context.Set<TEntity>();
        }

        //The Get method uses lambda expressions to allow the calling code to specify a filter condition 
        //and a column to order the results by, and a string parameter lets the caller provide a 
        //comma-delimited list of navigation properties for eager loading:
        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties == null) includeProperties = "";
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public virtual async Task<TEntity> GetAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.SingleOrDefaultAsync(predicate);
        }

        public virtual async Task<IEnumerable<TEntity>>FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }

        public virtual async Task InsertAsync(TEntity entity)
        {
           await dbSet.AddAsync(entity);
        }

        public virtual async Task InsertRangeAsync(IEnumerable<TEntity> entities)
        {
           await dbSet.AddRangeAsync(entities);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
             dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entitiesToUpdate)
        {
            foreach (var entityToUpdate in entitiesToUpdate)
            {
                dbSet.Attach(entityToUpdate);
                _context.Entry(entityToUpdate).State = EntityState.Modified;
            }
        }

        public virtual void Delete(object id)
        {
             Task<TEntity> entityToDelete = dbSet.FindAsync(id);
             Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entitiesToDelete)
        {
            foreach (var entityToDelete in entitiesToDelete)
            {
                if (_context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    dbSet.Attach(entityToDelete);
                }
                dbSet.Remove(entityToDelete);
            }
        }
    }
}

