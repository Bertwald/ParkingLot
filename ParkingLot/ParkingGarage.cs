using ParkingDeluxe;
using ParkingDeluxe.Vehicles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingDeluxe {
    internal class ParkingGarage {
        private static readonly double parkingFee = 1.50D;
        //private static readonly int _maxVehicleSize = 4;
        //private ParkingSlot[] _parkingSlots;
        private int _numberOfSlots;
        private bool[] _isIndexOccupied;
        private Dictionary<string, int> parkedVehicles;
        internal ParkingQueue queue;

        internal ParkingGarage(int slots) {
            this._numberOfSlots = slots;
            _isIndexOccupied = new bool[_numberOfSlots*2];
            parkedVehicles = new Dictionary<string, int>();
            queue = new();
        }

        internal bool CanPark(int size) {
            for (int i = 0; i < _isIndexOccupied.Length; i += size) {
                if (!_isIndexOccupied[i]) {
                    return true;
                }
            }
            return false;
        }
            internal void Park(Vehicle vehicle) {
            for (int i = 0; i < _isIndexOccupied.Length; i += vehicle.Size) {
                if (!_isIndexOccupied[i] && i+vehicle.Size <= _isIndexOccupied.Length) {
                    parkedVehicles.Add(vehicle.LicenseNumber, i);
                    vehicle.StartPark();
                    vehicle.ParkingInterval = vehicle.Size <= 2 ? (i >> 1).ToString() : (i >> 1) + "-" + ((i + vehicle.Size) >> 1).ToString();
                    for (int j = 0; j < vehicle.Size; j++) {
                        _isIndexOccupied[i + j] = true;
                    }
                }
            }
        }
        internal double UnPark(Vehicle vehicle) {
            //Unblock parking lot
            int startindex = parkedVehicles[vehicle.LicenseNumber];
            for(int i = 0; i < vehicle.Size; i++) {
                _isIndexOccupied[startindex + i] = false;
            }
            parkedVehicles.Remove(vehicle.LicenseNumber);
            return vehicle.GetParkedTime() * parkingFee;
        }

        internal void Run() {

        }
    }
}
