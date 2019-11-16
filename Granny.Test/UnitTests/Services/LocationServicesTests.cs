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
    class LocationServicesTests
    {
        private static readonly Mock<ILocationRepository> mockedDao = new Mock<ILocationRepository>();
        private static readonly Mock<IUnitOfWork> mockedUnitOfWork = new Mock<IUnitOfWork>();
        private static ILocationServices service;

        [TestFixture]
        public class WhenLocationServiceCreate
        {
            private Location location;

            public WhenLocationServiceCreate()
            {
                this.location = new Location
                {
                    Name = "University",
                    LocationId = 2
                };
            }

            [Test]
            public async Task Sucess_LocationService_Create()
            {
                //Arrange
                mockedDao.Invocations.Clear();
                mockedDao.Setup(s => s.AddAsync(It.IsAny<Location>()));

                //Act
                service = new LocationService(mockedUnitOfWork.Object, mockedDao.Object);
                int locationId = await service.Create(this.location);

                //Assert
                Assert.AreEqual(2, locationId);
                mockedDao.Verify(s => s.AddAsync(It.IsAny<Location>()), Times.Once);
            }
        }

        [TestFixture]
        public class WhenLocationServiceGetByName
        {
            private Location location;

            public WhenLocationServiceGetByName()
            {
                this.location = new Location
                {
                    Name = "University",
                    LocationId = 2
                };
            }

            [Test]
            public async Task Sucess_LocationService_GetByName()
            {
                //Arrange
                mockedDao.Invocations.Clear();
                mockedDao.Setup(s => s.GetByName(It.IsAny<string>()))
                    .ReturnsAsync(this.location);

                //Act
                service = new LocationService(mockedUnitOfWork.Object, mockedDao.Object);
                Location location = await service.GetByName("Colegio");

                //Assert
                Assert.AreSame(this.location, location);
                mockedDao.Verify(s=>s.GetByName(It.IsAny<string>()), Times.Once);
            }
        }
    }
}
