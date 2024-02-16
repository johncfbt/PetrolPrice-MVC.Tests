using FakeItEasy;
using FluentAssertions;
using PetroPrice_MVC.Controllers;
using PetroPrice_MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.Tests.Controllers
{
    public class PetrolStationControllerTests
    {
        [Fact]
        public void PetrolStationController_Delete_WithValidModelData_ReturnsViewResult()
        {
            // Arrange
            var fakePetrolStationsDAO = A.Fake<PetrolStationsDAO>();
            var controller = new PetrolStationController(fakePetrolStationsDAO);
            var validId = 41; //no need retrieve data, testing controller behavior not data access layer. as there will always result no matter the Id.

            // Act
            var result = controller.Delete(validId);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.ViewName.Should().Be("Index");
        }

        [Fact]
        public void PetrolStationController_Delete_WithInvalidModelData_ReturnsNotFoundResult()
        {
            // Arrange
            var fakePetrolStationsDAO = A.Fake<PetrolStationsDAO>();
            var controller = new PetrolStationController(fakePetrolStationsDAO);
            var invalidId = 100; // Assuming an invalid ID
                                 // Mocking behavior of GetPetrolStationById to return null, indicating no entity found
            A.CallTo(() => fakePetrolStationsDAO.GetPetrolStationById(invalidId)).Returns(null);

            // Act
            var result = controller.Delete(invalidId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
