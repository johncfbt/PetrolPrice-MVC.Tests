using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using PetroPrice_MVC.Controllers;
using PetroPrice_MVC.Models;
using PetroPrice_MVC.Services;

namespace PetroPrice_MVC.Tests.Controllers
{
    public class CreateTest
    {
        [Fact]
        public void PetrolStationController_Create_ReturnViewResult()
        {
            // Arrange
            var fakePetrolStationsDAO = A.Fake<PetrolStationsDAO>();
            var controller = new PetrolStationController(fakePetrolStationsDAO);

            // Act
            // "as" keyword for type casting to perform extra assertions, if cast not valid, result will be null and provide valueable info.
            var result = controller.Create() as ViewResult;

            // Assert
            result.Should().NotBeNull().And.BeOfType<ViewResult>();
        }

        [Fact]
        public void PetrolStationController_Create_ReturnRedirectResult()
        {
            // Arrange
            var fakePetrolStationsDAO = A.Fake<PetrolStationsDAO>();
            var controller = new PetrolStationController(fakePetrolStationsDAO);
            var petrolStation = new PetrolStation
            {
                Id = 1,
                Name = "Test name create test",
                Address = "Test address",
                Price = 2.0m
            };
            // Configure the fake DAO to return a predefined ID without actually executing the database operation
            A.CallTo(() => fakePetrolStationsDAO.Create(A<PetrolStation>._)).Returns(123); // Assuming 123 as the predefined ID

            // Act
            var result = controller.Create(petrolStation) as RedirectToActionResult;

            // Assert
            result.Should().NotBeNull().And.BeOfType<RedirectToActionResult>();
            result.ActionName.Should().Be("Index");
        }

        [Fact]
        public void PetrolStationController_Create_InvalidModel_ReturnViewResult()
        {
            // Arrange
            var fakePetrolStationsDAO = A.Fake<PetrolStationsDAO>();
            var controller = new PetrolStationController(fakePetrolStationsDAO);
            var petrolStation = new PetrolStation
            {
                Id = 1,
                Name = "Test namenamename namenamename namenamename namenamename",
                Address = "Test address",
                Price = 2.0m
            };
            controller.ModelState.Clear(); // Clear existing state to ensure no interference
            controller.ModelState.AddModelError("PetrolStation.Name", "The Name field is more than 50 characters"); 

            // Act
            var result = controller.Create(petrolStation) as ViewResult;

            // Assert
            result.Should().NotBeNull().And.BeOfType<ViewResult>();
            result.ViewName.Should().Be("Index"); 
            result.Model.Should().BeOfType<PetrolStation>().Which.Should().BeSameAs(petrolStation); // Ensure model is returned
        }
    }
}
