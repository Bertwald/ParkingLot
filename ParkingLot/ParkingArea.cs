using ParkingDeluxe;
using ParkingDeluxe.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingDeluxe {
    internal class ParkingGarage {
        private static readonly double parkingFee = 1.50D;
        private static readonly int _maxVehicleSize = 4;
        private int _numberOfSlots;
        private ParkingSlot[] _parkingSlots;
        internal ParkingQueue queue;

        internal ParkingGarage(int slots) {
            this._numberOfSlots = slots;
            _parkingSlots = new ParkingSlot[_numberOfSlots];
            queue = new();

        }

        internal bool CanPark(int size) {
            return false;
        }
        internal void Park(Vehicle vehicle) {

        }

        internal void Run() {

        }
    }
}
