namespace ElevatorChallenge.Application.Interfaces
{
    public interface IBuildingService
    {
        void AddPassenger(IPassengerService passenger, int floorNum);
        void RunSimulation();
        IFloorService GetFloor(int floorNum);
        IElevatorService GetNearestElevator(int floorNum);
        
    }
}
