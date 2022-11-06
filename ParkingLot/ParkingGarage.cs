using ParkingDeluxe;
using ParkingDeluxe.Vehicles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ParkingDeluxe {
    internal class ParkingGarage {
        private static readonly double parkingFee = 1.50D;
        //private static readonly int _maxVehicleSize = 4;
        //private ParkingSlot[] _parkingSlots;
        private int _numberOfSlots;
        private bool[] _isIndexOccupied;
        private Dictionary<Vehicle, int> parkedVehicles;
        internal ParkingQueue queue;

        internal ParkingGarage(int slots) {
            this._numberOfSlots = slots;
            _isIndexOccupied = new bool[_numberOfSlots*2];
            parkedVehicles = new Dictionary<Vehicle, int>();
            queue = new();
        }
        //Not working as intended. very much not so
        //internal bool CanPark(int size) {
        //    for (int i = 0; i < _isIndexOccupied.Length; i += size) {
        //        for (int j = 0; j < size; j++) {
        //            if (_isIndexOccupied[i + j] == true) {
        //                continue;
        //            }
        //        }
        //        if (!_isIndexOccupied[i]) {
        //            return true;
        //        }
        //    }
        //    return false;
        //}
            internal bool Park(Vehicle vehicle) {
            bool skipOne = false;
            for (int i = 0; i < _isIndexOccupied.Length; i += vehicle.Size) {
                if (!_isIndexOccupied[i] && i+vehicle.Size <= _isIndexOccupied.Length) {
                    for (int j = 0; j < vehicle.Size; j++) {
                        if(_isIndexOccupied[i + j] == true) {
                            skipOne = true;
                            continue;
                        }
                    }
                    if(skipOne == true) {
                        skipOne = false;
                        continue;
                    }
                    parkedVehicles.Add(vehicle, i);
                    vehicle.StartPark();
                    vehicle.ParkingInterval = vehicle.Size <= 2 ? (i >> 1).ToString() : (i >> 1) + "-" + ((i + vehicle.Size) >> 1).ToString();
                    for (int j = 0; j < vehicle.Size; j++) {
                        _isIndexOccupied[i + j] = true;
                    }
                    return true;
                }
            }
            return false;
        }
        internal double UnPark(Vehicle vehicle) {
            //Unblock parking lot
            int startindex = parkedVehicles[vehicle];
            for(int i = 0; i < vehicle.Size; i++) {
                _isIndexOccupied[startindex + i] = false;
            }
            parkedVehicles.Remove(vehicle);
            return vehicle.GetParkedTime() * parkingFee;
        }

        internal void ListParkedCars() {
            var vehicles = from data in parkedVehicles
                           orderby data.Value
                           select data.Key;
            foreach (var vehicle in vehicles) {
                Console.WriteLine(vehicle.ToString());
            }

        }

        internal void Run() {

        }
    }
}
