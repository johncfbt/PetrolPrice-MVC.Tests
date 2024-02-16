using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using PetroPrice_MVC.Controllers;
using PetroPrice_MVC.Models;
using PetroPrice_MVC.Services;

namespace PetroPrice_MVC.Tests.Controllers
{
    public class PetrolStationControllerTests
    {
        [Fact]
        public void PetrolStationController_ProcessEditReturnPartial_WithvalidModelData_ReturnsPartialView()
        {
            // Arrange
            var fakePetrolStationsDAO = A.Fake<PetrolStationsDAO>();
            var controller = new PetrolStationController(fakePetrolStationsDAO);
            var petrolStation = new PetrolStation
            {
                Id = 31,
                Name = "Test Name",
                Address = "Test Address",
                Price = 2.0m
            };
            A.CallTo(() => fakePetrolStationsDAO.Update(petrolStation)).Returns(0);

            // Act
            var result = controller.ProcessEditReturnPartial(petrolStation);

            // Assert
            result.Should().NotBeNull().And.BeOfType<PartialViewResult>();
            var partialViewResult = result.Should().BeOfType<PartialViewResult>().Subject;
            partialViewResult.ViewName.Should().Be("_petrolStationCard");
            partialViewResult.Model.Should().BeEquivalentTo(petrolStation);
        }

        [Fact]
        public void PetrolStationController_ProcessEditReturnPartial_WithInvalidModelData_ReturnsViewResult()
        {
            // Arrange
            var fakePetrolStationsDAO = A.Fake<PetrolStationsDAO>();
            var controller = new PetrolStationController(fakePetrolStationsDAO);
            var invalidModel = new PetrolStation
            {
                Id = 1,
                Name = "Test namenamename namenamename namenamename namenamename",
                Address = "Test address",
                Price = 2.0m
            };
            controller.ModelState.Clear(); // Clear existing state to ensure no interference
            controller.ModelState.AddModelError("PetrolStation.Name", "The Name field is more than 50 characters");

            // Act
            var result = controller.ProcessEditReturnPartial(invalidModel);

            // Assert
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public void PetrolStationController_Edit_WithValidModelData_ReturnsRedirectToActionResult()
        {
            // Arrange
            var fakePetrolStationsDAO = A.Fake<PetrolStationsDAO>();
            var controller = new PetrolStationController(fakePetrolStationsDAO);
            var validModel = new PetrolStation
            {
                Id = 33,
                Name = "Test Name",
                Address = "Test Address",
                Price = 2.50m
            };

            // Act
            var result = controller.Edit(validModel);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>()
                .Which.ActionName.Should().Be("Index");
        }
    }
}
