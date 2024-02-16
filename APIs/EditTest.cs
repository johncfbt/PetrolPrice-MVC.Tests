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
    public class EditTest
    {
        [Fact]
        public void ProcessEdit_ReturnsUpdatedPetrolStation()
        {
            // Arrange
            var fakePetrolStationsDAO = A.Fake<PetrolStationsDAO>();
            var PetrolStation = new PetrolStation {
                Id = 31,
                Name = "Test Name",
                Address = "Test Address",
                Price = 2.0m
            };
            A.CallTo(() => fakePetrolStationsDAO.Update(PetrolStation
                )).Returns(0);
            var controller = new PetrolStationControllerAPI(fakePetrolStationsDAO);

            // Act
            var result = controller.ProcessEdit(PetrolStation);

            // Assert
            result.Should().NotBeNull().And.BeOfType<ActionResult<PetrolStation>>();
        }
    }
}
