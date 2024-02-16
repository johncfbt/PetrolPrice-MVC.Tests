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
    
    public class DetailsTest
    {
        [Fact]
        public void Details_ReturnsPetrolStation()
        {
            // Arrange
            var fakePetrolStationsDAO = A.Fake<PetrolStationsDAO>();
            var expectedPetrolStation = new PetrolStation {
                Id = 1,
                Name = "Test Name",
                Address = "Test Address",
                Price = 2.50m
            };
            A.CallTo(() => fakePetrolStationsDAO.GetPetrolStationById(A<int>._)).Returns(expectedPetrolStation);
            var controller = new PetrolStationControllerAPI(fakePetrolStationsDAO);

            // Act
            var result = controller.Details(1);

            // Assert
            result.Should().BeOfType<ActionResult<PetrolStation>>();
        }
    }
}
