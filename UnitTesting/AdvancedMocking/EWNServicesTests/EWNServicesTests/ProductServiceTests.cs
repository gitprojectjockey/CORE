using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EWNServices.ProductServices.Abstract;
using EWNData.Dto;
using EWNData.Repositories.Abstract;
using EWNServices.ProductService.Concrete;
using EWNServicesTests.TestDataHelpers;
using System.Collections.Generic;
using System.Linq;
using EWNServices.ProductReceiptServices.Abtract;
using EWNServices.UserDefinedExceptions;
using EWNServices.ServiceModels;
using System;
using EWNServices.RetailSpecialServices.Abstract;

namespace EWNServicesTests
{
    [TestClass]
    public class ProductServiceTests
    {
        List<IProduct> _products;
        Mock<IProductRepository> _mockProductRepository;
        Mock<IProductReceiptService> _mockProductReceiptService;
        Mock<IRetailSpecialService> _mockRetailSpecialService;

        [TestInitialize]
        public void Setup()
        {
            _products = ProductDataHelper.GetProductList().ToList();
            _mockProductRepository = new Mock<IProductRepository>();
            _mockProductReceiptService = new Mock<IProductReceiptService>();
            _mockRetailSpecialService = new Mock<IRetailSpecialService>();
        }
        [TestMethod]
        public void The_ProductService_Save_Calls_ProductRepository_Add_With_A_Single_Product()
        {
            //Arrange
            _mockProductRepository.Setup(mock => mock.Add(It.IsAny<IProduct>()));

            //Act - SUT
            IProductService productService = new ProductService(_mockProductRepository.Object, _mockProductReceiptService.Object, _mockRetailSpecialService.Object);
            productService.SaveRange(_products);

            //Assert
            _mockProductRepository.Verify(service => service.Add(It.IsAny<IProduct>()));
        }

         [TestMethod]
        public void The_ProductService_SaveRange_calls_ProductRepository_Add_With_All_ProductObject_In_ProductsList()
        {
            //Arrange
            _mockProductRepository.Setup(mock => mock.Add(It.IsAny<IProduct>()));

            //Act - SUT
            IProductService productService = new ProductService(_mockProductRepository.Object, _mockProductReceiptService.Object, _mockRetailSpecialService.Object);
            productService.SaveRange(_products);

            //Assert
            _mockProductRepository.Verify(service => service.Add(It.IsAny<IProduct>()), Times.Exactly(_products.Count));
        }

        // Testing if a method got called
        [TestMethod]
        public void The_ProductService_SaveWithReceipt_Calls_ProductRepository_Add_Once()
        {
            //Arrange

            IProduct product = _products.FirstOrDefault(p => p.ProductId == 3);

            _mockProductRepository.Setup(mock => mock.Add(It.IsAny<IProduct>()));
            
            _mockProductReceiptService.Setup(mock => mock.CreateReceipt(product)).Returns(() => new ProductReceipt()
            {
                ProductName = "Hammer",
                ProductCost = "33.67",
                PuchaseDate = DateTime.Now
            });

            //Act - SUT
            IProductService productService = new ProductService(_mockProductRepository.Object, _mockProductReceiptService.Object, _mockRetailSpecialService.Object);
            productService.SaveWithReceipt(product);

            //Assert
            _mockProductRepository.Verify(service => service.Add(It.IsAny<IProduct>()),Times.Once);
        }

        // Return Values
        [TestMethod]
        public void The_ProductService_SaveWithReceipt_Calls_ProductReceiptService_ReceiptCreated_PassesBack_OutParameter()
        {
            //Arrange
            IProduct product = _products.FirstOrDefault(p => p.ProductId == 3);

            var receiptCode = "4n7778w234AQ22";
            _mockProductReceiptService.Setup(mock => mock.CreateReceipt(product, out receiptCode)).Returns(() => new ProductReceipt()
            {
                ProductName = "Hammer",
                ProductCost = "33.67",
                PuchaseDate = DateTime.Now
            });

            //Act - SUT
            IProductService productService = new ProductService(_mockProductRepository.Object, _mockProductReceiptService.Object, _mockRetailSpecialService.Object);
            productService.SaveWithReceipt(product, out string code);
           
           Assert.AreEqual("4n7778w234AQ22", code);
        }

        // Multiple Return Values using call backs
        [TestMethod]
        public void The_ProductService_SaveWithReceipt_Calls_ProductReceiptService_CreateNewReceiptId_ForEachProduct()
        {
            // Arrange
            var receiptId = Guid.NewGuid().ToString("D");
            _mockProductReceiptService.Setup(p => p.CreateUniqueReceiptId())
                .Returns(() => receiptId)
                .Callback(() => receiptId = Guid.NewGuid().ToString("D"));

            // Act - SUT
            IProductService productService = new ProductService(_mockProductRepository.Object, _mockProductReceiptService.Object, _mockRetailSpecialService.Object);
            productService.SaveWithReceipt(_products);

            // Assert
            _mockProductReceiptService.Verify(service => service.CreateUniqueReceiptId(), Times.Exactly(_products.Count));
        }

        // Argument Tracking
        // Make sure the arguments passed in have the values you expect
        [TestMethod]
        public void The_ProductService_SaveWithReceiptHeader_Calls_ProductReceiptService_CreateReceiptHeader_WithCorrectParameterValues()
        {

            // Arrange
            var product = _products.FirstOrDefault(p => p.ProductId == 2);

            _mockProductReceiptService.Setup(service => service.CreateReceiptHeader(It.IsAny<string>(),It.IsAny<string>(),It.IsAny<string>()));

            // Act - SUT
            IProductService productService = new ProductService(_mockProductRepository.Object, _mockProductReceiptService.Object, _mockRetailSpecialService.Object);
            productService.SaveWithReceiptHeader(product);

            // Assert
            _mockProductReceiptService.Verify(service => service
                 .CreateReceiptHeader(It.Is<string>(fn => fn.Equals(product.ProductName, StringComparison.InvariantCultureIgnoreCase)),
                                      It.Is<string>(fn => fn.Equals(product.Price.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                                      It.Is<string>(fn => fn.Equals(product.Description, StringComparison.InvariantCultureIgnoreCase))));
        }

        // Parameters and Execution
        [TestMethod]
        public void The_ProductService_SaveWithProductSpecialStatus_Calls_RetailSpecialService_GetProductSalesStatus_ReturnsDefault_AddIsCalled()
        {
            // Arrange
            var product = _products.FirstOrDefault(p => p.ProductId == 1);
            _mockRetailSpecialService.Setup(service => service.GetProductSalesStatus(It.IsAny<IProduct>())).Returns(ProductSpecialStatus.Default);
            _mockProductRepository.Setup(service => service.Add(It.IsAny<IProduct>()));


            // Act - SUT
            IProductService productService = new ProductService(_mockProductRepository.Object, _mockProductReceiptService.Object, _mockRetailSpecialService.Object);
            productService.SaveWithProductSpecialStatus(product);

            // Assert
            _mockProductRepository.Verify(service => service.Add(It.IsAny<IProduct>()), Times.Once);
        }

        // Parameter and Execution
        [TestMethod]
        public void The_ProductService_SaveWithProductSpecialStatus_Calls_RetailSpecialService_GetProductSalesStatus_ReturnsSpecial_AddSpecialIsCalled()
        {
            // Arrange
            var product = _products.FirstOrDefault(p => p.ProductId == 1);
            _mockRetailSpecialService.Setup(service => service.GetProductSalesStatus(It.IsAny<IProduct>())).Returns(ProductSpecialStatus.Special);
            _mockProductRepository.Setup(service => service.Add(It.IsAny<IProduct>()));


            // Act - SUT
            IProductService productService = new ProductService(_mockProductRepository.Object, _mockProductReceiptService.Object, _mockRetailSpecialService.Object);
            productService.SaveWithProductSpecialStatus(product);

            // Assert
            _mockProductRepository.Verify(service => service.AddSpecial(It.IsAny<IProduct>()), Times.Once);
        }

        // Conditional Mock Setup
        [TestMethod]
        public void The_ProductService_SaveWithProductSpecialStatus_Calls_RetailSpecialService_GetProductSalesStatus_ReturnsCorrectStatus_BasedOnPrice()
        {
            // Arrange
            var product = _products.FirstOrDefault(p => p.ProductId == 2);

            _mockRetailSpecialService.Setup(service => service.GetProductSalesStatus(It.IsAny<IProduct>()))
                .Returns(() => product.Price <= 33.22m ? ProductSpecialStatus.Default : ProductSpecialStatus.Special);

            // Act - SUT

            IProductService productService = new ProductService(_mockProductRepository.Object, _mockProductReceiptService.Object, _mockRetailSpecialService.Object);
            productService.SaveWithProductSpecialStatus(product);

            // Assert
            
            _mockRetailSpecialService.Verify(service => service.GetProductSalesStatus(It.IsAny<IProduct>()));
        }

        // Dealing with exceptions - Setup throw
        [TestMethod]
        [ExpectedException(typeof(SaveWithReceiptException))]
        public void The_ProductService_SaveWithReceipt_Throw_SaveWithReceiptException()
        {
            // Arrange
            var product = _products.FirstOrDefault(p => p.ProductId == 3);
            _mockProductReceiptService.Setup(service => service.CreateReceipt(It.IsAny<IProduct>()))
                .Throws(new ProductPriceException());

            // Act - SUT
            IProductService productService = new ProductService(_mockProductRepository.Object, _mockProductReceiptService.Object, _mockRetailSpecialService.Object);
            productService.SaveWithReceipt(product);

            // Assert
            // No assert neccesary
        }

        // Testing property was Set with VerifySet
        [TestMethod]
        public void ProductService_Save_SetsProperty_ProductRepository_LocalTimeZone()
        {
            // Arrange
            var product = _products.FirstOrDefault(p => p.ProductId == 3);
           

            // Act - SUT
            IProductService productService = new ProductService(_mockProductRepository.Object, _mockProductReceiptService.Object, _mockRetailSpecialService.Object);
            productService.Save(product);

            // Assert
            // No assert neccesary
            _mockProductRepository.VerifySet(prop => prop.LocalTimeZone = It.IsAny<string>());

        }

        // Testing property Get with VerifyGet
        [TestMethod]
        public void ProductService_SaveRange_GetsProperty_ProductRepository_LocalTimeZone()
        {
            // Arrange
            var localTimeZone = System.TimeZoneInfo.Local.DisplayName;
            _mockProductRepository.Setup(service => service.LocalTimeZone).Returns(localTimeZone);

            // Act - SUT
            IProductService productService = new ProductService(_mockProductRepository.Object, _mockProductReceiptService.Object, _mockRetailSpecialService.Object);
            productService.SaveRange(_products);

            // Assert
            // No assert neccesary
            _mockProductRepository.VerifyGet(prop => prop.LocalTimeZone);
        }

        // Testing  Nested  property hierarchy Get with VerifyGet
        [TestMethod]
        public void ProductService_Save_GetsProperty_ProductRepo_Product_Company_CompanyName()
        {
            // Arrange
            var product = _products.FirstOrDefault(p => p.ProductId == 3);
            var localTimeZone = TimeZoneInfo.Local.DisplayName;
            _mockProductRepository.Setup(service => service.Product.Company.CompanyName).Returns("Howell Industries");

            // Act - SUT
            IProductService productService = new ProductService(_mockProductRepository.Object, _mockProductReceiptService.Object, _mockRetailSpecialService.Object);
            productService.SaveRange(_products);

            // Assert
            // No assert neccesary
            _mockProductRepository.VerifyGet(prop => prop.Product.Company.CompanyName);
        }

        // Stubbing properties
        
        [TestMethod]
        public void ProductService_SaveWithAddText_GetsProperty_RetailSpecialService_AdText()
        {
            // Arrange
            var product = _products.FirstOrDefault(p => p.ProductId == 3);

            //Stubbing the property
            _mockRetailSpecialService.SetupProperty(service => service.AdText,"This is the ad text");
            //Now pretend that the propery needs to be changed on the fly for some reason...
            _mockRetailSpecialService.Object.AdText = "I have changed the text of the AdText propery stub";

            // Act - SUT
            IProductService productService = new ProductService(_mockProductRepository.Object, _mockProductReceiptService.Object, _mockRetailSpecialService.Object);
            productService.SaveWithAdText(product);

            // Assert
            // No assert neccesary
            _mockRetailSpecialService.VerifyGet(prop => prop.AdText);
        }

    }
}
