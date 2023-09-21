

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorChallenge.Application.Interfaces
{
    public interface IFloorService
    {
        void AddPassenger(IPassengerService passenger);
        List<IPassengerService> GetPassengers();
        int GetFloorNum();
        int GetNumPassengers();

        void CallElevator(int numPassengers, IElevatorService elevatorSerive);
    }
}
