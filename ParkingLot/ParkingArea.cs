using ParkingDeluxe;
using ParkingDeluxe.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingDeluxe {
    internal class ParkingArea {
        private int _numberOfSlots;
        private ParkingSlot[] _parkingSlots;
        internal ParkingQueue queue;

        internal ParkingArea(int slots) {
            this._numberOfSlots = slots;

        }
    }
}
