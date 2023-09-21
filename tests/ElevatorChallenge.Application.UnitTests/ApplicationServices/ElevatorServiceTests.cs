using AutoFixture;
using ElevatorChallenge.Application.Interfaces;
using ElevatorChallenge.Application.Services;
using Moq;

namespace ElevatorChallenge.Application.UnitTests.ApplicationServices
{
    public class ElevatorServiceTests
    {
        private readonly IFixture fixture;

        public ElevatorServiceTests()
        {
            fixture = new Fixture();
            fixture.Register(() => new Mock<IPassengerService>().Object);
        }

        [Fact]
        public void AddPassengers_ShouldAddPassengers_WhenNotFull()
        {
            // Arrange
            var elevatorNumber = 1;
            var currentFloorMock = new Mock<IFloorService>();
            var buildingServicesMock = new Mock<IBuildingService>();
            var elevatorService = new ElevatorService(elevatorNumber, currentFloorMock.Object, buildingServicesMock.Object);

            var passengersToAdd = fixture.CreateMany<IPassengerService>(5).ToList();

            // Act
            elevatorService.AddPassengers(passengersToAdd);

            // Assert
            Assert.Equal(5, elevatorService.GetNumPassengers());
        }

        [Fact]
        public void AddPassengers_ShouldNotAddPassengers_WhenFull()
        {
            // Arrange
            var elevatorNumber = 1;
            var currentFloorMock = new Mock<IFloorService>();
            var buildingServicesMock = new Mock<IBuildingService>();
            var elevatorService = new ElevatorService(elevatorNumber, currentFloorMock.Object, buildingServicesMock.Object);

            var passengersToAdd = fixture.CreateMany<IPassengerService>(15).ToList();

            // Act
            elevatorService.AddPassengers(passengersToAdd);

            // Assert
            Assert.True(elevatorService.isFull);
            Assert.Equal(0, elevatorService.GetNumPassengers());
        }

        [Fact]
        public void CanPickUpPassengers_ShouldReturnTrue_WhenElevatorIsNotFullAndDirectionMatches()
        {
            // Arrange
            var elevatorNumber = 1;
            var currentFloorMock = new Mock<IFloorService>();
            var buildingServicesMock = new Mock<IBuildingService>();
            var elevatorService = new ElevatorService(elevatorNumber, currentFloorMock.Object, buildingServicesMock.Object);
            elevatorService.isGoingUp = true;

            var destinationFloorMock = new Mock<IFloorService>();
            currentFloorMock.Setup(c => c.GetFloorNum()).Returns(2);
            destinationFloorMock.Setup(d => d.GetFloorNum()).Returns(3);
            elevatorService.destinationFloor = destinationFloorMock.Object;

            // Act
            var canPickUp = elevatorService.CanPickUpPassengers();

            // Assert
            Assert.True(canPickUp);
        }

        [Fact]
        public void SetDestinationFloor_ShouldSetDestinationFloorCorrectly()
        {
            // Arrange
            var elevatorNumber = 1;
            var currentFloorMock = new Mock<IFloorService>();
            var buildingServicesMock = new Mock<IBuildingService>();
            var elevatorService = new ElevatorService(elevatorNumber, currentFloorMock.Object, buildingServicesMock.Object);

            var destinationFloorMock = new Mock<IFloorService>();
            buildingServicesMock.Setup(b => b.GetFloor(It.IsAny<int>())).Returns(destinationFloorMock.Object);

            // Act
            elevatorService.SetDestinationFloor(3);

            // Assert
            Assert.Equal(destinationFloorMock.Object, elevatorService.destinationFloor);
        }
    }
}
