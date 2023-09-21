using ElevatorChallenge.Application.Interfaces;

namespace ElevatorChallenge.Application.Services
{
    public class PassengerService : IPassengerService
    {
        private int currentFloor;
        private int destinationFloor;
        public PassengerService(int currentFloor, int destinationFloor)
        {
            this.currentFloor = currentFloor;
            this.destinationFloor = destinationFloor;
        }
        public int GetCurrentFloor()
        {
            return currentFloor;
        }

        public int GetDestinationFloor()
        {
            return destinationFloor;
        }
    }
}
