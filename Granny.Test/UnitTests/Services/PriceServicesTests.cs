using Granny.DAO.EntitiesRepository.Interface;
using Granny.DAO.UnitOfWork.Interface;
using Granny.DataModel;
using Granny.Services;
using Granny.Services.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Granny.Test.UnitTests.Services
{
    class PriceServicesTests
    {
        private static readonly Mock<IPriceRepository> mockedDao = new Mock<IPriceRepository>();
        private static readonly Mock<IUnitOfWork> mockedUnitOfWork = new Mock<IUnitOfWork>();
        private static IPriceServices service;

        [TestFixture]
        public class WhenPriceServicesCreate
        {
            private Price price;

            public WhenPriceServicesCreate()
            {
                this.price = new Price
                {
                    PriceId = 1,
                    ProductId = 3,
                    RegisterDate = DateTime.Now,
                    UserId = "1",
                    Value = 100
                };
            }

            [Test]
            public async Task Sucess_PriceServices_Create()
            {
                //Arrange
                mockedDao.Invocations.Clear();
                mockedDao.Setup(s => s.AddAsync(It.IsAny<Price>()));

                //Act
                service = new PriceServices(mockedUnitOfWork.Object, mockedDao.Object);
                int priceId = await service.Create(this.price);

                //Assert
                Assert.AreEqual(1, priceId);
                mockedDao.Verify(s => s.AddAsync(It.IsAny<Price>()), Times.Once);
            }
        }

        [TestFixture]
        public class WhenPriceServicesGetBestProductPrice
        {
            private Price price;

            public WhenPriceServicesGetBestProductPrice()
            {
                this.price = new Price
                {
                    PriceId = 1,
                    ProductId = 3,
                    RegisterDate = DateTime.Now,
                    UserId = "1",
                    Value = 100
                };
            }

            [Test]
            public async Task Sucess_PriceServices_GetBestProductPrice()
            {
                //Arrange
                mockedDao.Invocations.Clear();
                mockedDao.Setup(s => s.GetBestProductPrice(It.IsAny<long>()))
                    .ReturnsAsync(this.price);

                //Act
                service = new PriceServices(mockedUnitOfWork.Object, mockedDao.Object);
                Price price = await service.GetBestProductPrice(2);

                //Assert
                Assert.AreSame(this.price, price);
                mockedDao.Verify(s => s.GetBestProductPrice(It.IsAny<long>()), Times.Once);
            }
        }

        [TestFixture]
        public class WhenPriceServicesGetNextProductPrices
        {
            private List<Price> price;

            public WhenPriceServicesGetNextProductPrices()
            {
                this.price = new List<Price>() {
                    new Price
                    {
                        PriceId = 1,
                        ProductId = 3,
                        RegisterDate = DateTime.Now,
                        UserId = "1",
                        Value = 100
                    }
                };
            }

            [Test]
            public async Task Sucess_PriceServices_GetNextProductPrices()
            {
                //Arrange
                mockedDao.Invocations.Clear();
                mockedDao.Setup(s => s.GetNextProductPrices(It.IsAny<long>(), It.IsAny<decimal>()))
                    .ReturnsAsync(this.price);

                //Act
                service = new PriceServices(mockedUnitOfWork.Object, mockedDao.Object);
                IEnumerable<Price> priceList = await service.GetNextProductPrices(2, 100);

                //Assert
                Assert.AreEqual(priceList.ToList().Count, 1);
                mockedDao.Verify(s => s.GetNextProductPrices(It.IsAny<long>(), It.IsAny<decimal>()), Times.Once);
            }
        }

        [TestFixture]
        public class WhenPriceServicesGetPricesByLocation
        {
            private List<Price> price;

            public WhenPriceServicesGetPricesByLocation()
            {
                this.price = new List<Price>() {
                    new Price
                    {
                        PriceId = 1,
                        ProductId = 3,
                        RegisterDate = DateTime.Now,
                        UserId = "1",
                        Value = 100
                    }
                };
            }

            [Test]
            public async Task Sucess_PriceServices_GetPricesByLocation()
            {
                //Arrange
                mockedDao.Invocations.Clear();
                mockedDao.Setup(s => s.GetPricesByLocation(It.IsAny<string>()))
                    .ReturnsAsync(this.price);

                //Act
                service = new PriceServices(mockedUnitOfWork.Object, mockedDao.Object);
                IEnumerable<Price> priceList = await service.GetPricesByLocation("University");

                //Assert
                Assert.AreEqual(priceList.ToList().Count, 1);
                mockedDao.Verify(s => s.GetPricesByLocation(It.IsAny<string>()), Times.Once);
            }
        }

        [TestFixture]
        public class WhenPriceServicesCheckIfExists
        {
            private Price price;

            public WhenPriceServicesCheckIfExists()
            {
                this.price = new Price
                {
                    PriceId = 1,
                    ProductId = 3,
                    RegisterDate = DateTime.Now,
                    UserId = "1",
                    Value = 100
                };
            }

            [Test]
            public async Task Sucess_PriceServices_CheckIfExists()
            {
                //Arrange
                mockedDao.Invocations.Clear();
                mockedDao.Setup(s => s.CheckIfExists(It.IsAny<long>(), It.IsAny<int>()))
                    .ReturnsAsync(this.price);

                //Act
                service = new PriceServices(mockedUnitOfWork.Object, mockedDao.Object);
                int? priceId = await service.CheckIfExists(2, 1);

                //Assert
                Assert.AreEqual(priceId, 1);
                mockedDao.Verify(s => s.CheckIfExists(It.IsAny<long>(), It.IsAny<int>()), Times.Once);
            }
        }

        [TestFixture]
        public class WhenPriceServicesGetByName
        {
            private List<Price> price;

            public WhenPriceServicesGetByName()
            {
                this.price = new List<Price>() {
                    new Price
                    {
                        PriceId = 1,
                        ProductId = 3,
                        RegisterDate = DateTime.Now,
                        UserId = "1",
                        Value = 100
                    }
                };
            }

            [Test]
            public async Task Sucess_PriceServices_GetByName()
            {
                //Arrange
                mockedDao.Invocations.Clear();
                mockedDao.Setup(s => s.GetByName(It.IsAny<string>()))
                    .ReturnsAsync(this.price);

                //Act
                service = new PriceServices(mockedUnitOfWork.Object, mockedDao.Object);
                IEnumerable<Price> priceList = await service.GetByName("Pan");

                //Assert
                Assert.AreEqual(priceList.ToList().Count, 1);
                mockedDao.Verify(s => s.GetByName(It.IsAny<string>()), Times.Once);
            }
        }
    }
}
