
using LibraryData.Entities;
using LMS.Data.DataContext;
using LMS.Data.UnitOfWork;
using LMS.DataTransfer.ObjectMaps;
using LMS.DataTransfer.Objects;
using LMS.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LMS.Tests
{
    [TestClass]
    public class TestPatronService
    {
        [TestMethod]
        public void Test_Get ()
        {
            var data = new List<Patron>()
            {
                new Patron()
                {
                    Id =1,
                    FirstName = "Eric",
                    LastName = "Wayne",
                    Address = "555 Maplewood Drive",
                    DateOfBirth = DateTime.Now,
                    Gender = "M",
                    HomeLibraryBranchId = 3,
                    Telephone = "555-555-5555"
                },
                new Patron()
                {
                    Id =2,
                    FirstName = "Eric",
                    LastName = "Wayne",
                    Address = "555 Maplewood Drive",
                    DateOfBirth = DateTime.Now,
                    Gender = "M",
                    HomeLibraryBranchId = 3,
                    Telephone = "555-555-5555"
                }
            };

            Mock<ILoggerFactory> logger = new Mock<ILoggerFactory>();
            ILMSMaps mapper = new LMSMaps();

            var options = new DbContextOptionsBuilder<LMSContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                .Options;

            //using(LMSContext context = new LMSContext(options))
            //{
            //    IUnitOfWork unitOfWork = new UnitOfWork(context);
            //    PatronService patronService = new PatronService(unitOfWork, logger.Object, mapper);
            //    foreach(var p in data)
            //            patronService.Create(p);

            //    PatronDto patron = patronService.GetAsync(2);
            //}

            //using (LMSContext context = new LMSContext(options))
            //{
            //    IUnitOfWork unitOfWork = new UnitOfWork(context);
            //    PatronService patronService = new PatronService(unitOfWork, logger.Object, mapper);
            //    PatronDto patron = patronService.Get(2);
            //}
        }
    }
}
