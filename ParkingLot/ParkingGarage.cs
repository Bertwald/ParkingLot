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
    internal enum InputModes {
        Manual,
        Automatic,
        MAX
    }
    internal enum Command {
        Park,
        Unpark,
        ToggleInput,
        Quit,
        DoNothing
    }
    internal class ParkingGarage {
        private static readonly double _parkingFee = 1.50D;
        private static readonly bool _sizeBasedFee = false;
        private int _numberOfSlots;
        private bool[] _isIndexOccupied;
        private Dictionary<Vehicle, int> _parkedVehicles;
        private ParkingQueue _queue;
        internal InputModes InputMode { get; set; }

        internal ParkingGarage(int slots, ParkingQueue queue) {
            this._numberOfSlots = slots;
            _isIndexOccupied = new bool[_numberOfSlots * 2];
            _parkedVehicles = new Dictionary<Vehicle, int>();
            _queue = queue;
        }
        internal bool Park(Vehicle vehicle) {
            bool skipOne = false;
            for (int i = 0; i < _isIndexOccupied.Length; i += vehicle.Size) {
                if (!_isIndexOccupied[i] && i + vehicle.Size <= _isIndexOccupied.Length) {
                    for (int j = 0; j < vehicle.Size; j++) {
                        if (_isIndexOccupied[i + j] == true) {
                            skipOne = true;
                            continue;
                        }
                    }
                    if (skipOne == true) {
                        skipOne = false;
                        continue;
                    }
                    _parkedVehicles.Add(vehicle, i);
                    vehicle.StartPark();
                    vehicle.ParkingInterval = vehicle.Size <= 2 ? (i >> 1).ToString() : (i >> 1) + "-" + ((i + vehicle.Size -1) >> 1).ToString();
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
            for (int i = 0; i < vehicle.Size; i++) {
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
            bool commandSucceeded;
            do {
                Console.Clear();
                ListParkedCars();
                PrintInputMode();
                UI.ShowMenu();
                Command command = InputModule.GetCommand();
                commandSucceeded = ProcessCommand(command);
            } while (commandSucceeded);
        }

        private void PrintInputMode() {
            Console.WriteLine("Active InputMode : " + InputMode);
        }

        private bool ProcessCommand(Command command) => command switch {
            Command.Park => ParkingProcess(),
            Command.Unpark => UnParkingProcess(),
            Command.ToggleInput => ToggleInputProcess(),
            Command.DoNothing => true,
            _ => false,
        };

        private bool ToggleInputProcess() {
            InputMode = (InputModes)((int)(InputMode + 1) % (int)InputModes.MAX);
            return true;
        }

        private bool UnParkingProcess() {
            UI.ShowUnparkingInstruction();
            string licenceNumber = InputModule.GetString();
            var vehicles = from vehicle in _parkedVehicles
                           where vehicle.Key.LicenseNumber == licenceNumber
                           select vehicle.Key;
            if (vehicles is null || vehicles.Count() == 0) {
                UI.PrintUnParkingError("Could not find any car with that licensenumber");
                return true;
            } else {
                var vehicle = vehicles.First();
                double cost = UnPark(vehicles.First());
                UI.PrintUnParkingInfo(vehicle, cost);
                return true;
            }
        }

        private bool ParkingProcess() {
            bool success;
            if (InputMode == InputModes.Manual) {
                var vehicle = _queue.Dequeue();
                if (vehicle is null) {
                    UI.PrintUnParkingError("The queue returned null");
                    return true;
                } else {
                    vehicle = UI.SetVehicleFromInput(vehicle);
                    success = Park(vehicle);
                    if (!success) {
                        UI.PrintUnParkingError($"The {vehicle.GetType().Name} could not fit");
                    }
                    return true;
                }
            } else /* InputMode == InputMode.Automatic */{
                var vehicle = _queue.Dequeue();
                if (vehicle is null) {
                    UI.PrintUnParkingError("The queue returned null");
                    return true;
                } else {
                    success = Park(vehicle);
                    if (!success) {
                        UI.PrintUnParkingError($"The {vehicle.GetType().Name} could not fit");
                    }
                    return true;
                }
            }
        }
    }
}
