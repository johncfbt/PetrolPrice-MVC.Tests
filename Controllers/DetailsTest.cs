using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetroPrice_MVC.Controllers;
using PetroPrice_MVC.Models;
using PetroPrice_MVC.Services;

namespace PetroPrice_MVC.Tests.Controllers
{
    public class DetailsTest
    {
        [Fact]
        public void PetrolStationController_Details_ReturnViewResult_WithValidModel()
        {
            // Arrange
            var fakePetrolStationsDAO = A.Fake<PetrolStationsDAO>();
            var controller = new PetrolStationController(fakePetrolStationsDAO);
            var petrolStationId = 21;
            var expectedPetrolStation = new PetrolStation
            {
                Id = 21,
                Name = "Test name",
                Address = "Test address",
                Price = 2.0m
            };
            A.CallTo(() => fakePetrolStationsDAO.GetPetrolStationById(petrolStationId)).Returns(expectedPetrolStation);


            // Act
            var result = controller.Details(petrolStationId) as ViewResult;

            // Assert
            result.Should().NotBeNull().And.BeOfType<ViewResult>();
            result.Model.Should().BeOfType<PetrolStation>().Which.Should().BeSameAs(expectedPetrolStation);
        }

        [Fact]
        public void PetrolStationController_Details_NotFoundResult_WhenPetrolStationNotFound()
        {
            // Arrange
            var fakePetrolStationsDAO = A.Fake<PetrolStationsDAO>();
            var controller = new PetrolStationController(fakePetrolStationsDAO);
            var petrolStationId = 1;
            A.CallTo(() => fakePetrolStationsDAO.GetPetrolStationById(petrolStationId)).Returns(null);

            // Act
            var result = controller.Details(petrolStationId) as NotFoundResult;

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void ShowOnePetrolStationJSON_Returns_JsonResult_With_Valid_Model()
        {
            // Arrange
            var fakePetrolStationsDAO = A.Fake<PetrolStationsDAO>();
            var controller = new PetrolStationController(fakePetrolStationsDAO);
            var petrolStationId = 22;
            var expectedPetrolStation = new PetrolStation
            {
                Id = petrolStationId,
                Name = "Test Name",
                Address = "Test Address",
                Price = 2.0m
            };
            A.CallTo(() => fakePetrolStationsDAO.GetPetrolStationById(petrolStationId)).Returns(expectedPetrolStation);

            // Act
            var result = controller.ShowOnePetrolStationJSON(petrolStationId);

            // Assert
            result.Should().NotBeNull().And.BeOfType<JsonResult>();
            var jsonResult = result.Should().BeOfType<JsonResult>().Subject;
            var model = jsonResult.Value.Should().BeAssignableTo<PetrolStation>().Subject;
            model.Should().BeEquivalentTo(expectedPetrolStation);
        }
    }
}
