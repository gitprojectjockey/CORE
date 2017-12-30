
using Microsoft.EntityFrameworkCore;
using System;

namespace LibraryServiceTests.TestingHelpers
{
    public class InMemoryDatabaseHelper <TContext> : DbContextOptions<TContext> where TContext: DbContext
    {
        SqliteConnection  _connection;
        DbContextOptions _options;

        public InMemoryDatabaseHelper(SqliteConnection connection,
            DbContextOptionsBuilder<TContext> optionsBuilder,
            SqliteConnectionStringBuilder connectionStringBuilder) 
        {
            _connection = connection;
            _connection.ConnectionString = connectionStringBuilder.ToString();
            _options = optionsBuilder.UseSqlite(_connection).Options;
        }

        public void OpenConnection()
        {
            _connection.Open();
        }

        public void EnsureDatabaseCreated(DbContext context)
        {
            context.Database.EnsureCreated();
        }

        public DbContextOptions<TContext> GetContextOptions()
        {
            return _options as DbContextOptions<TContext>;
        }

        public void CloseConnection()
        {
            _connection.Close();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_connection != null)
                {
                    _connection.Dispose();
                    _connection = null;
                }
            }
        }
    }
}
