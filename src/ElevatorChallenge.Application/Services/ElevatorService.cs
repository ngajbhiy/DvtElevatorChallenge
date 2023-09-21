using ElevatorChallenge.Application.Interfaces;

namespace ElevatorChallenge.Application.Services
{
    public class ElevatorService : IElevatorService
    {
        public IBuildingService _buildingServices;
        public IFloorService currentFloor;
        public IFloorService destinationFloor;

        public List<IPassengerService> passengers;

        public int elevatorNumber;
        public bool isMoving = false;
        public bool isFull = false;
        public bool isGoingUp = true;

        public int maxPassengerLimit = 10;
        private int i;


        public ElevatorService(int elevatorNumber, IFloorService currentFloor, IBuildingService buildingServices)
        {
            this.elevatorNumber = elevatorNumber;
            this.currentFloor = currentFloor;
            this._buildingServices = buildingServices;
            passengers = new List<IPassengerService>();

        }

        public void AddPassengers(List<IPassengerService> newPassengers)
        {
            if (passengers.Count + newPassengers.Count > maxPassengerLimit)
            {
                Console.WriteLine($"Elevator {elevatorNumber} is full");
                isFull = true;
                return;
            }

            foreach (var passenger in newPassengers)
            {
                passengers.Add(passenger);
            }
        }



        public bool CanPickUpPassengers()
        {
            if (isFull)
                return false;
            if (isGoingUp && currentFloor.GetFloorNum() > destinationFloor?.GetFloorNum())
                return false;
            if(!isGoingUp && currentFloor.GetFloorNum() < destinationFloor?.GetFloorNum())
                return false;
            return true;
        }



        public string GetDirection()
        {
            if (isGoingUp)
                return "Going up";
            return "Going down";
        }

        public int GetElevatorNum()
        {
            return elevatorNumber;
        }

        public int GetNumPassengers()
        {
            return passengers.Count;
        }

        public List<IPassengerService> GetPassengers()
        {
            return passengers;

        }

        public string GetStatus()
        {

            if (isMoving)
                return "Moving";
            else if (isFull)
                return "Full";
            else
                return "Stopped";
        }

        public void SetDestinationFloor(int florrNUm)
        {
            destinationFloor = _buildingServices.GetFloor(florrNUm);
        }
        public void UpdateStatus()
        {
            if (isMoving)
            {
                if (isGoingUp)
                    currentFloor = _buildingServices.GetFloor(currentFloor.GetFloorNum() + 1);
                else
                    currentFloor = _buildingServices.GetFloor(currentFloor.GetFloorNum() - 1);

                if (currentFloor == destinationFloor)
                {
                    isMoving = false;
                    passengers.RemoveAll(p => p.GetDestinationFloor() == currentFloor.GetFloorNum());
                }
            }
            else
            {
                if (passengers.Count > 0)
                {
                    int minFloor = passengers.Min(p => p.GetDestinationFloor());
                    int maxFloor = passengers.Max(p => p.GetDestinationFloor());

                    if (currentFloor.GetFloorNum() < minFloor)
                    {
                        isGoingUp = true;
                        destinationFloor = _buildingServices.GetFloor(minFloor);
                        isMoving = true;
                    }
                    else if (currentFloor.GetFloorNum() > maxFloor)
                    {
                        isGoingUp = true;
                        destinationFloor = _buildingServices.GetFloor(maxFloor);
                        isMoving = true;
                    }
                }
            }
        }

        public IFloorService GetCurrentFloor()
        {
            return currentFloor;
        }
    }
}
