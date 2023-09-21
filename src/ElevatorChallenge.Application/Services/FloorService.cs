using ElevatorChallenge.Application.Interfaces;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorChallenge.Application.Services
{
    public class FloorService : IFloorService
    {
        public List<IPassengerService> passengers;
        public int floorNum;
        private IBuildingService _buildingServices;

        public FloorService(int floorNum, IBuildingService buildingServices)
        {
            this.floorNum = floorNum;
            _buildingServices = buildingServices;
            passengers = new List<IPassengerService>();
        }

        public void AddPassenger(IPassengerService passenger)
        {
            passengers.Add(passenger);
        }

        public void CallElevator(int numPassengers, IElevatorService elevatorSerive)
        {

            IElevatorService elevator = _buildingServices.GetNearestElevator(floorNum);
            if (elevator != null) 
            {
                List<IPassengerService> waitingPassengers = passengers.Take(numPassengers).ToList();
                elevator.AddPassengers(waitingPassengers);
                passengers.RemoveAll(p => waitingPassengers.Contains(p));
                elevator.SetDestinationFloor(floorNum);

            }
        }

        public int GetFloorNum()
        {
            return floorNum;
        }

        public int GetNumPassengers()
        {
            return passengers.Count;
        }

        public List<IPassengerService> GetPassengers()
        {
            return passengers;
        }
    }
}
