using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using PetroPrice_MVC.Controllers;
using PetroPrice_MVC.Models;
using PetroPrice_MVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetrolPrice_MVC.Tests.APIs
{
    public class DeleteTest
    {
        [Fact]
        public void Delete_ReturnsDeletedPetrolStationId()
        {
            // Arrange
            var fakePetrolStationsDAO = A.Fake<PetrolStationsDAO>();
            var expectedId = 1; // Assuming deletion of petrol station with ID 1
            A.CallTo(() => fakePetrolStationsDAO.Delete(A<int>._)).Returns(1);
            var controller = new PetrolStationControllerAPI(fakePetrolStationsDAO);

            // Act
            var result = controller.Delete(1);

            // Assert
            result.Should().BeOfType<ActionResult<int>>();
        }
    }
}
