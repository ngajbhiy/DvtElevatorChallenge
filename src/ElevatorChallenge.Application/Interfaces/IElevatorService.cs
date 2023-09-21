
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorChallenge.Application.Interfaces
{
    public interface IElevatorService
    {
        void AddPassengers(List<IPassengerService> newPassengers);
        List<IPassengerService> GetPassengers();
        void SetDestinationFloor(int florrNUm);
        IFloorService GetCurrentFloor();
        int GetElevatorNum();
        string GetDirection();
        string GetStatus();
        bool CanPickUpPassengers();
        void UpdateStatus();
        int GetNumPassengers();
    }
}
