using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using PetroPrice_MVC.Controllers;
using PetroPrice_MVC.Models;
using PetroPrice_MVC.Services;

namespace PetrolPrice_MVC.Tests.Controllers
{
    public class IndexTest
    {
        [Fact]
        public void Index_ReturnsViewResult_WithListOfPetrolStations()
        {
            // Arrange
            var fakePetrolStationsDAO = A.Fake<PetrolStationsDAO>();
            var expectedPetrolStations = new List<PetrolStation>
            {
                new PetrolStation { Id = 1, Name = "Station 1", Address = "Address 1", Price = 2.5m },
                new PetrolStation { Id = 2, Name = "Station 2", Address = "Address 2", Price = 2.6m }
            };
            A.CallTo(() => fakePetrolStationsDAO.GetAllPetrolStations()).Returns(expectedPetrolStations);
            var controller = new PetrolStationController(fakePetrolStationsDAO);

            // Act
            var result = controller.Index();

            // Assert
            result.Should().NotBeNull().And.BeOfType<ViewResult>();
            var viewResult = result as ViewResult;
            viewResult.Model.Should().BeOfType<List<PetrolStation>>().Which.Should().BeEquivalentTo(expectedPetrolStations);
        }
    }
}
