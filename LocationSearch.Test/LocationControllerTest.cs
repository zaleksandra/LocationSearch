using Domain.Models;
using LocationSearch.Service.Interfaces;
using LocationSearchApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Xunit;

namespace LocationSearch.Test
{
    public class LocationControllerTest
    {
        private readonly ILocationRepository _locationRepository;
        private readonly ICsvService _csvService;
        private readonly ILogger<LocationController> _logger;

        private LocationController _locationController;
        public LocationControllerTest()
        {
           _locationRepository = new Mock<ILocationRepository>().Object;
            _csvService = new Mock<ICsvService>().Object;
            _logger = new Mock<ILogger<LocationController>>().Object;

            _locationController =new LocationController(_locationRepository, _csvService, _logger);

        }
        [Fact]
        public void GetLocation_WhenThereIsNoError_ReturnOK()
        {
            //Arrange
            var list = new List<Location>() { new Location { Address ="AH Frieswijkstraat 72, Nijkerk",Latitude =52.2165425, Longitude=5.4778534,Id=1},
                                            new Location {Address ="Belemnieterf",Latitude =50.91414,Longitude =5.95549,Id =2} };
            var repoMock = new Mock<ILocationRepository>();
            repoMock.Setup(moq => moq.Find(It.IsAny<Expression<Func<Location, bool>>>())).Returns(list.AsEnumerable());
            double latitude = 52.377846;
            double longitude = 4.883241;
            double maxDistance = 10;
            //Act

           var result = _locationController.GetLocation(latitude, longitude, maxDistance, 1);
            var okResult = result as OkObjectResult;
            //Assert

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);


        }



    }
}
