using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LibraryServices.Concrete;
using LibraryData.Models;
using LibraryServiceTests.TestingHelpers;
using System.Collections.Generic;
using System.Linq;

namespace LibraryServiceTests
{
    [TestClass]
    public class LibraryAssetServiceTests
    {
        InMemoryDatabaseHelper<LibraryData.LibraryContext> _memoryDatabaseHelper;
       
        [TestInitialize]
        public void Setup()
        {
            _memoryDatabaseHelper = new InMemoryDatabaseHelper<LibraryData.LibraryContext>
                 (
                     new SqliteConnection(),
                     new DbContextOptionsBuilder<LibraryData.LibraryContext>(),
                     new SqliteConnectionStringBuilder("DataSource=:memory:")
                 );

            _memoryDatabaseHelper.OpenConnection();
        }

        [TestMethod]
        public void GetById_ValidId_ReturnsEntity()
        {
            List<LibraryAsset> libraryAssets = GetLibraryAssetsWithData();
            using (var libraryContext = new LibraryData.LibraryContext(_memoryDatabaseHelper.GetContextOptions()))
            {
                _memoryDatabaseHelper.EnsureDatabaseCreated(libraryContext);
            }

            using (var libraryContext = new LibraryData.LibraryContext(_memoryDatabaseHelper.GetContextOptions()))
            {
                LibraryAssetService services = new LibraryAssetService(libraryContext);
                services.Add(libraryAssets.FirstOrDefault());
            }
           
            using (var libraryContext = new LibraryData.LibraryContext(_memoryDatabaseHelper.GetContextOptions()))
            {
               LibraryAssetService services = new LibraryAssetService(libraryContext);
               var entity = services.GetById(1);
               Assert.IsNotNull(entity);
            }
        }

        [TestMethod]
        public void GetAll_ReturnsAllEntities()
        {
            List<LibraryAsset> libraryAssets = GetLibraryAssetsWithData();
            using (var libraryContext = new LibraryData.LibraryContext(_memoryDatabaseHelper.GetContextOptions()))
            {
                _memoryDatabaseHelper.EnsureDatabaseCreated(libraryContext);
            }

            using (var libraryContext = new LibraryData.LibraryContext(_memoryDatabaseHelper.GetContextOptions()))
            {
                LibraryAssetService services = new LibraryAssetService(libraryContext);
                foreach (var asset in libraryAssets)
                {
                    services.Add(asset);
                }
            }

            using (var libraryContext = new LibraryData.LibraryContext(_memoryDatabaseHelper.GetContextOptions()))
            {
                LibraryAssetService services = new LibraryAssetService(libraryContext);
                var entities = services.GetAll();
                Assert.AreEqual(2,entities.Count());
            }
        }

        [TestCleanup]
        public void TearDown()
        {
            _memoryDatabaseHelper.CloseConnection();
            _memoryDatabaseHelper.Dispose();
        }

        #region HelperMethods
        private List<LibraryAsset> GetLibraryAssetsWithData()
        {
            var assetCollection = new List<LibraryAsset>();

            LibraryBranch libraryBranch1 = new LibraryBranch()
            {
                ImageUrl = "/images/branches/1.png",
                Address = "88 Lakeshore Dr",
                Name = "Lake Shore Branch",
                Telephone = "555-1234",
                OpenDate = new DateTime(1975 - 05 - 13),
                Description = "The oldest library branch in Lakeview, the Lake Shore Branch was opened in 1975. Patrons of all ages enjoy the wide selection of literature available at Lake Shore library. The coffee shop is open during library hours of operation."
            };

            Status status1 = new Status()
            {
                Name = "Checked Out",
                Description = "A library asset that has been checked out"
            };

            var libraryAsset1 = new LibraryAsset()
            {
                Cost = 18.00m,
                Location = libraryBranch1,
                Status = status1,
                NumberOfCopies = 3,
                Title = "Jane Austen",
                Year = 1975,
                ImageUrl = "/images/emma.png"
            };

            assetCollection.Add(libraryAsset1);

            LibraryBranch libraryBranch2 = new LibraryBranch()
            {
                ImageUrl = "/images/branches/1.png",
                Address = "85 Pulty Dr",
                Name = "Inner City Branch",
                Telephone = "999-7777",
                OpenDate = new DateTime(1922 - 05 - 13),
                Description = "The oldest library branch in the city of Pulty, the Pulty Branch was opened in 1935. Patrons of all ages enjoy the wide selection of literature available at Inner City library. The coffee shop is open during library hours of operation."
            };

            Status status2 = new Status()
            {
                Name = "Checked In",
                Description = "A library asset that has been checked in"
            };

            var libraryAsset2 = new LibraryAsset()
            {
                Cost = 18.00m,
                Location = libraryBranch2,
                Status = status2,
                NumberOfCopies = 6,
                Title = "Peter Piper",
                Year = 1922,
                ImageUrl = "/images/emma.png"
            };

            assetCollection.Add(libraryAsset2);

            return assetCollection;
        }
    #endregion
    }
}

