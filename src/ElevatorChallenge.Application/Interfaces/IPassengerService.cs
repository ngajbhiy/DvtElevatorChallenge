using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorChallenge.Application.Interfaces
{
    public interface IPassengerService
    {
        int GetCurrentFloor();
        int GetDestinationFloor();
    }
}
