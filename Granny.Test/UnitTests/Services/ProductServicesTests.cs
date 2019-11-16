using Granny.DAO.EntitiesRepository.Interface;
using Granny.DAO.UnitOfWork.Interface;
using Granny.DataModel;
using Granny.Services;
using Granny.Services.Interfaces;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Granny.Test.UnitTests.Services
{
    class ProductServicesTests
    {
        private static readonly Mock<IProductRepository> mockedDao = new Mock<IProductRepository>();
        private static readonly Mock<IUnitOfWork> mockedUnitOfWork = new Mock<IUnitOfWork>();
        private static IProductServices service;

        [TestFixture]
        public class WhenProductServiceCreate
        {
            private Product product;

            public WhenProductServiceCreate()
            {
                this.product = new Product
                {
                    Name = "Pan",
                    ProductId = 3
                };
            }

            [Test]
            public async Task Sucess_ProductService_Create()
            {
                //Arrange
                mockedDao.Invocations.Clear();
                mockedDao.Setup(s => s.AddAsync(It.IsAny<Product>()));

                //Act
                service = new ProductServices(mockedUnitOfWork.Object, mockedDao.Object);
                await service.Create(this.product);

                //Assert
                mockedDao.Verify(s => s.AddAsync(It.IsAny<Product>()), Times.Once);
            }
        }

        [TestFixture]
        public class WhenProductServiceGetById
        {
            private Product product;

            public WhenProductServiceGetById()
            {
                this.product = new Product
                {
                    Name = "Pan",
                    ProductId = 3
                };
            }

            [Test]
            public void Sucess_ProductService_GetById()
            {
                //Arrange
                mockedDao.Invocations.Clear();
                mockedDao.Setup(s => s.Get(It.IsAny<long>()))
                    .Returns(this.product);

                //Act
                service = new ProductServices(mockedUnitOfWork.Object, mockedDao.Object);
                Product product = service.GetById(2);

                //Assert
                Assert.AreSame(this.product, product);
                mockedDao.Verify(s => s.Get(It.IsAny<long>()), Times.Once);
            }
        }
    }
}
