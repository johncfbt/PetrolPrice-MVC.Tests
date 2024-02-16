using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using PetroPrice_MVC.Controllers;
using PetroPrice_MVC.Models;
using PetroPrice_MVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PetrolPrice_MVC.Tests.APIs
{
    public class IndexTest
    {
        [Fact]
        public void Index_ReturnsListOfPetrolStations()
        {
            // Arrange
            var fakePetrolStationsDAO = A.Fake<PetrolStationsDAO>();
            var expectedPetrolStations = new List<PetrolStation> {
                    new PetrolStation
                    {
                        Id = 1,
                        Name = "Test Name",
                        Address = "Test Address",
                        Price = 2.50m
                    },
                    new PetrolStation
                    {
                        Id = 2,
                        Name = "Test Name 2",
                        Address = "Test Address 2",
                        Price = 2.50m
                    }
                };
            A.CallTo(() => fakePetrolStationsDAO.GetAllPetrolStations()).Returns(expectedPetrolStations);
            var controller = new PetrolStationControllerAPI(fakePetrolStationsDAO);

            // Act
            var result = controller.Index();

            // Assert
            result.Should().BeOfType<ActionResult<IEnumerable<PetrolStation>>>();
        }

    }
}
