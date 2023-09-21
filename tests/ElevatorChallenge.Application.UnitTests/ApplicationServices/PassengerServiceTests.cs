using AutoFixture;
using AutoFixture.AutoMoq;
using ElevatorChallenge.Application.Services;

namespace ElevatorChallenge.Application.UnitTests.ApplicationServices
{
    public class PassengerServiceTests
    {
        [Fact]
        public void GetCurrentFloor_ShouldReturnCurrentFloor()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var currentFloor = fixture.Create<int>();
            var destinationFloor = fixture.Create<int>();
            var passengerService = new PassengerService(currentFloor, destinationFloor);

            // Act
            var result = passengerService.GetCurrentFloor();

            // Assert
            Assert.Equal(currentFloor, result);
        }

        [Fact]
        public void GetDestinationFloor_ShouldReturnDestinationFloor()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var currentFloor = fixture.Create<int>();
            var destinationFloor = fixture.Create<int>();
            var passengerService = new PassengerService(currentFloor, destinationFloor);

            // Act
            var result = passengerService.GetDestinationFloor();

            // Assert
            Assert.Equal(destinationFloor, result);
        }

        [Fact]
        public void GetCurrentFloor_WithNegativeFloor_ShouldReturnNegativeFloor()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var currentFloor = fixture.Create<int>() * -1; // Create a negative floor
            var destinationFloor = fixture.Create<int>();
            var passengerService = new PassengerService(currentFloor, destinationFloor);

            // Act
            var result = passengerService.GetCurrentFloor();

            // Assert
            Assert.Equal(currentFloor, result);
        }

        [Fact]
        public void GetDestinationFloor_WithNegativeFloor_ShouldReturnNegativeFloor()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var currentFloor = fixture.Create<int>();
            var destinationFloor = fixture.Create<int>() * -1; // Create a negative floor
            var passengerService = new PassengerService(currentFloor, destinationFloor);

            // Act
            var result = passengerService.GetDestinationFloor();

            // Assert
            Assert.Equal(destinationFloor, result);
        }

        [Fact]
        public void GetDestinationFloor_WithSameCurrentAndDestinationFloor_ShouldReturnSameFloor()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var currentFloor = fixture.Create<int>();
            var destinationFloor = currentFloor; // Set the destination floor to be the same as current floor
            var passengerService = new PassengerService(currentFloor, destinationFloor);

            // Act
            var result = passengerService.GetDestinationFloor();

            // Assert
            Assert.Equal(currentFloor, result);
        }
    }
}
