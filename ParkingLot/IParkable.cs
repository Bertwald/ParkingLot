using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingDeluxe {
    internal interface IParkable : IPlaceable{
        DateTime ParkingTime { get; }
        string ParkingInterval { get; }
        void StartPark();
        int GetParkedTime();

    }
}
