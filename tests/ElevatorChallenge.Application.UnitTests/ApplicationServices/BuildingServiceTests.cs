using ElevatorChallenge.Application.Interfaces;
using ElevatorChallenge.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorChallenge.Application.UnitTests.ApplicationServices
{
    public class BuildingServiceTests
    {
        [Fact]
        public void TestAddPassenger()
        {
            BuildingService buildingService = new BuildingService(10, 3);
            IPassengerService passenger = new PassengerService(0,1);

            buildingService.AddPassenger(passenger, 0);
            Assert.Equal(1, buildingService.GetFloor(0).GetNumPassengers());
        }

        [Fact]
        public void TestGetFloor()
        {
            // Arrange
            BuildingService buildingService = new BuildingService(10, 3);

            // Act
            IFloorService floor = buildingService.GetFloor(0);

            // Assert
            Assert.Equal(0, floor.GetFloorNum());
        }

        [Fact]
        public void TestGetNearestElevator()
        {
            BuildingService buildingService = new BuildingService(10, 3);
            IElevatorService elevator1 = buildingService.GetNearestElevator(0);
            elevator1.SetDestinationFloor(5);

            IElevatorService elevator2 = buildingService.GetNearestElevator(9);
            elevator2.SetDestinationFloor(5);

            IElevatorService nearestElevator = buildingService.GetNearestElevator(3);

            Assert.Equal(elevator1, nearestElevator);
        }
    }
}
