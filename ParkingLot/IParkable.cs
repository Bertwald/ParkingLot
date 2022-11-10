using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingDeluxe {
    internal interface IParkable : IPlaceable{
        internal DateTime ParkingTime { get; }
        internal string ParkingInterval { get; }
        internal void StartPark();
        internal int GetParkedTime();

    }
}
