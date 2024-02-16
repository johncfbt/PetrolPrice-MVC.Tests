using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using PetroPrice_MVC.Controllers;
using PetroPrice_MVC.Models;
using PetroPrice_MVC.Services;
using System.Net;

namespace PetrolPrice_MVC.Tests.APIs
{
    public class CreateTest
    {
        [Fact]
        public void PetrolStationControllerAPI_Create_ReturnsOkResult_WithNewI()
        {
            // Arrange
            var fakePetrolStationsDAO = A.Fake<PetrolStationsDAO>();
            var controller = new PetrolStationControllerAPI(fakePetrolStationsDAO);
            var petrolStation = new PetrolStation
            {
                Name = "Test Name",
                Address = "Test Address",
                Price = 2.50m
            };

            A.CallTo(() => fakePetrolStationsDAO.Create(A<PetrolStation>._)).Returns(1); // Assuming the new ID returned is 1

            // Act
            var result = controller.Create(petrolStation);

            // Assert
            result.Should().BeOfType<ActionResult<int>>();
        }
    }
}
