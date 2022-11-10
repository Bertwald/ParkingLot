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
    internal enum InputMode {
        Manual,
        Automatic,
    }
    internal class ParkingGarage {
        private static readonly double _parkingFee = 1.50D;
        private static readonly bool _sizeBasedFee = false;
        //private static readonly int _maxVehicleSize = 4;
        //private ParkingSlot[] _parkingSlots;
        private int _numberOfSlots;
        private bool[] _isIndexOccupied;
        private Dictionary<Vehicle, int> _parkedVehicles;
        private ParkingQueue _queue;
        internal InputMode InputMode { get; set; }

        internal ParkingGarage(int slots, ParkingQueue queue) {
            this._numberOfSlots = slots;
            _isIndexOccupied = new bool[_numberOfSlots*2];
            _parkedVehicles = new Dictionary<Vehicle, int>();
            _queue = queue;   
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
                    _parkedVehicles.Add(vehicle, i);
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
            int startindex = _parkedVehicles[vehicle];
            for(int i = 0; i < vehicle.Size; i++) {
                _isIndexOccupied[startindex + i] = false;
            }
            _parkedVehicles.Remove(vehicle);
            return _sizeBasedFee ? vehicle.GetParkedTime() * _parkingFee * vehicle.Size / 2 : vehicle.GetParkedTime() * _parkingFee;
        }

        internal void ListParkedCars() {
            var vehicles = from data in _parkedVehicles
                           orderby data.Value
                           select data.Key;
            UI.PrintParking<Vehicle>(vehicles);
        }

        internal void Update() {

        }
    }
}
