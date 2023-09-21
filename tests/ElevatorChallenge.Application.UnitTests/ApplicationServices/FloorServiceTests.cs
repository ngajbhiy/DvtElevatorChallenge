using AutoFixture;
using ElevatorChallenge.Application.Interfaces;
using ElevatorChallenge.Application.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorChallenge.Application.UnitTests.ApplicationServices
{
    public class FloorServiceTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IBuildingService> _buildingServiceMock;
        private readonly FloorService _floorService;

        public FloorServiceTests()
        {
            _fixture = new Fixture();
            _fixture.Register(() => new Mock<IPassengerService>().Object);
            _buildingServiceMock = new Mock<IBuildingService>();
            _floorService = new FloorService(1, _buildingServiceMock.Object);
        }

        [Fact]
        public void AddPassenger_ShouldAddPassengerToList()
        {
            // Arrange
            var passengerMock = new Mock<IPassengerService>();

            // Act
            _floorService.AddPassenger(passengerMock.Object);

            // Assert
            Assert.Contains(passengerMock.Object, _floorService.passengers);
        }

        [Fact]
        public void CallElevator_ShouldAddPassengersToElevator_WhenElevatorIsNotNull()
        {
            // Arrange
            var elevatorMock = new Mock<IElevatorService>();
            var numPassengers = 3;

            _buildingServiceMock.Setup(b => b.GetNearestElevator(_floorService.floorNum))
                .Returns(elevatorMock.Object);

            var waitingPassengers = _fixture.CreateMany<IPassengerService>(3).ToList();

            foreach (var passenger in waitingPassengers)
            {
                _floorService.passengers.Add(passenger);
            }

            // Act
            _floorService.CallElevator(numPassengers, elevatorMock.Object);

            // Assert
            //elevatorMock.Verify(e => e.AddPassengers(waitingPassengers.Select(p => p).ToList()), Times.Once);
            Assert.Equal(0, _floorService.passengers.Count);
            elevatorMock.Verify(e => e.SetDestinationFloor(_floorService.floorNum), Times.Once);
        }

        [Fact]
        public void CallElevator_ShouldNotAddPassengersToElevator_WhenElevatorIsNull()
        {
            // Arrange
            var numPassengers = 3;

            _buildingServiceMock.Setup(b => b.GetNearestElevator(_floorService.floorNum))
                .Returns((IElevatorService)null);

            var waitingPassengers = _fixture.CreateMany<IPassengerService>(3).ToList();

            foreach (var passenger in waitingPassengers)
            {
                _floorService.passengers.Add(passenger);
            }

            // Act
            _floorService.CallElevator(numPassengers, It.IsAny<IElevatorService>());

            // Assert
            Assert.Equal(numPassengers, _floorService.passengers.Count);
        }

    }
}
