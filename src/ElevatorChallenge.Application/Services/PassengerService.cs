using ElevatorChallenge.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
