using ElevatorChallenge.Application.Interfaces;

namespace ElevatorChallenge.Application.Services
{
    public class BuildingService :IBuildingService
    {
        public List<IElevatorService> elevators;
        public List<IFloorService> floors;
        public BuildingService(int numFloors, int numElevators)
        {
            floors = new List<IFloorService>();
            for (int i = 0; i < numFloors; i++)
            {
                floors.Add(new FloorService(i,this));
            }

            elevators = new List<IElevatorService>();
            for (int i = 0; i < numElevators; i++)
            {
                elevators.Add(new ElevatorService(i, floors[0],this));
            }
        }

        public void AddPassenger(IPassengerService passenger, int floorNum)
        {
            floors[floorNum].AddPassenger(passenger);
        }

        

        public void RunSimulation()
        {
            while (true)
            {
                foreach (var elevator in elevators)
                {
                    elevator.UpdateStatus();
                }

                foreach (var floor in floors)
                {
                    //floor.up

                }
                if (Console.WindowTop >= 0)
                    Console.Clear();
                Console.WriteLine("Elevator Status:");
                foreach (var elevator in elevators)
                {
                    Console.WriteLine($"Elevator {elevator.GetElevatorNum()}: " +
                        $"{elevator.GetPassengers().Count} passengers, {elevator.GetDirection()},  {elevator.GetStatus()}");
                }

                Console.WriteLine("\nFloor Status:");
                foreach (var floor in floors)
                {
                    Console.WriteLine($"Floor {floor.GetFloorNum()}: {floor.GetNumPassengers()}  passengers waiting");
                }

                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("1. Call an elevator");
                Console.WriteLine("2. Exit Simulation");
                Console.WriteLine("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

                if (choice == 1)
                {
                    Console.Write("Enter the floor number: ");
                    int floorNum = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the number of passengers: ");
                    int numPassengers = int.Parse(Console.ReadLine());
                    floors[floorNum].CallElevator(numPassengers, elevators[0]);

                }
                else if (choice == 2)
                {
                    Environment.Exit(0);
                }

            }
        }

        private void OutputStatus()
        {
            Console.WriteLine("Elevator Status:");
            foreach (var elevator in elevators)
            {
                Console.WriteLine($"Elevator {elevator.GetElevatorNum()}: " +
                    $"{elevator.GetPassengers().Count} passengers, {elevator.GetDirection()},  {elevator.GetStatus()}");
            }

            Console.WriteLine("\nFloor Status:");
            foreach (var floor in floors)
            {
                Console.WriteLine($"Floor {floor.GetFloorNum()}: {floor.GetNumPassengers()}  passengers waiting");
            }
        }

        public IFloorService GetFloor(int floorNum)
        {
            return floors[floorNum];
        }

        public IElevatorService GetNearestElevator(int floorNum)
        {
            IElevatorService? nearestElevator = null;
            int minDistance = int.MaxValue;

            foreach (var elevator in elevators)
            {
                int distance = Math.Abs(elevator.GetCurrentFloor().GetFloorNum() - floorNum);
                if (distance < minDistance && elevator.CanPickUpPassengers())
                {
                    nearestElevator = elevator;
                    minDistance = distance;
                }
            }
            return nearestElevator;
        }
    }
}
