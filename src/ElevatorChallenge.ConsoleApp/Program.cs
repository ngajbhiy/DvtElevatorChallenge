
using ElevatorChallenge.Application.Interfaces;
using ElevatorChallenge.Application.Services;



class program 
{
    public static IBuildingService buildingService;
    static void Main(string[] args)
    {
        buildingService = new BuildingService(10, 3);
        Random random = new Random();

        for (int i = 0; i < 10; i++)
        {
            int currentFloor = random.Next(0, 10);
            int destinationFloor = random.Next(0, 10);
            while (destinationFloor == currentFloor)
            {
                destinationFloor = random.Next(0, 10);
            }

            IPassengerService passenger = new PassengerService(currentFloor, destinationFloor);
            buildingService.AddPassenger(passenger, currentFloor);

        }

        buildingService.RunSimulation();

    }
    
}
