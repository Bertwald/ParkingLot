using ParkingDeluxe.UserInterface;
using ParkingDeluxe.Vehicles;

namespace ParkingDeluxe.Logic {
    internal enum InputMode {
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
        private static readonly double s_parkingFee = 1.50D;
        private static readonly bool s_spaceBasedFee = false;
        private readonly int _numberOfSpaces;
        private readonly bool[] _occupiedSpaces;
        private readonly Dictionary<Vehicle, int> _parkedVehicles;
        private readonly ParkingQueue _queue;
        private InputMode _inputMode;

        internal ParkingGarage(int slots, ParkingQueue queue) {
            _numberOfSpaces = slots;
            _occupiedSpaces = new bool[_numberOfSpaces * 2];
            _parkedVehicles = new Dictionary<Vehicle, int>();
            _queue = queue;
            _inputMode = InputMode.Manual;
        }
        internal bool Park(Vehicle vehicle) {
            //Due to parking layout we cannot park the bus at all consequent spaces, and even if we could that would result in non optimal bus placement
            //The number of spaces need to be an even number for "optimal behavior"
            for (int parkingNumber = 0; parkingNumber < _occupiedSpaces.Length; parkingNumber += vehicle.Size) {
                if (IsSpaceFree(parkingNumber) && CanVehicleFit(parkingNumber, vehicle.Size)) {
                    if (IsNeededSpaceUnoccupied(parkingNumber, vehicle.Size)) {
                        _parkedVehicles.Add(vehicle, parkingNumber);
                        vehicle.StartPark();
                        vehicle.ParkingInterval = GetParkingInterval(vehicle.Size, parkingNumber);
                        SetVehicleOccupation(vehicle, true);
                        return true;
                    }
                }
            }
            return false;
        }

        private bool IsNeededSpaceUnoccupied(int parkingNumber, int size) {
            for (int j = 1; j < size; j++) {
                if (_occupiedSpaces[parkingNumber + j] == true) {
                    return false;
                }
            }
            return true;
        }

        private bool IsSpaceFree(int parkingNumber) {
            return !_occupiedSpaces[parkingNumber];
        }
        private bool CanVehicleFit(int parkingNumber, int size) {
            return parkingNumber + size <= _occupiedSpaces.Length;
        }
        private string GetParkingInterval(int vehicleSize, int parkingNumber) {
            return vehicleSize <= 2 ? (parkingNumber >> 1).ToString() :
                                      (parkingNumber >> 1) + "-" + (parkingNumber + vehicleSize - 1 >> 1).ToString();
        }
        internal void UnPark(Vehicle vehicle) {
            SetVehicleOccupation(vehicle, false);
            RemoveVehicleFromDatabase(vehicle);
            double fee = GetParkingFee(vehicle);
            UI.PrintUnParkingInfo(vehicle, fee);
        }

        private void SetVehicleOccupation(Vehicle vehicle, bool occupation) {
            int startindex = _parkedVehicles[vehicle];
            for (int i = 0; i < vehicle.Size; i++) {
                _occupiedSpaces[startindex + i] = occupation;
            }
        }
        private void RemoveVehicleFromDatabase(Vehicle vehicle) {
            // If we cannot remove the vehicle, we need to check if it is still in the dictionary/database
            if (!_parkedVehicles.Remove(vehicle)) {
                if (_parkedVehicles.ContainsKey(vehicle)) {
                    UI.PrintParkingError("Could not remove vehicle entry from database, contact technical support", false);
                }
            }
        }
        private static double GetParkingFee(Vehicle vehicle) {
            return s_spaceBasedFee ? vehicle.GetParkedTime() * s_parkingFee * vehicle.Size / 2 :
                                    vehicle.GetParkedTime() * s_parkingFee;
        }

        internal void ListParkedCars() {
            var vehicles = from data in _parkedVehicles
                           orderby data.Value
                           select data.Key;
            UI.PrintParking<Vehicle>(vehicles);
        }

        internal void Run() {
            bool continueRunning;
            do {
                Console.Clear();
                ListParkedCars();
                UI.ShowMenu();
                PrintInputMode();
                Command command = InputModule.GetCommand();
                continueRunning = ProcessCommand(command);
            } while (continueRunning);
        }

        private void PrintInputMode() {
            Console.WriteLine("Active InputMode : " + _inputMode);
        }

        private bool ProcessCommand(Command command) => command switch {
            Command.Park => InitiateParkingProcess(),
            Command.Unpark => InitiateUnparkingProcess(),
            Command.ToggleInput => ToggleInputMode(),
            Command.Quit => false,
            Command.DoNothing => true,
            _ => true,
        };

        private bool ToggleInputMode() {
            _inputMode = (InputMode)((int)(_inputMode + 1) % (int)InputMode.MAX);
            return true;
        }

        private bool InitiateUnparkingProcess() {
            UI.ShowUnparkingInstructions();
            string licenceNumber = InputModule.GetString();
            var vehicles = from vehicle in _parkedVehicles
                           where vehicle.Key.LicenseNumber == licenceNumber
                           select vehicle.Key;
            if (vehicles is null || !vehicles.Any()) {
                UI.PrintParkingError("Could not find any car with that licensenumber", false);
                return true;
            } else {
                var vehicle = vehicles.First();
                UnPark(vehicles.First());
                return true;
            }
        }
        private bool InitiateParkingProcess() {
            bool couldParkCar;
            var vehicle = _queue.Dequeue();
            if (vehicle is null) {
                UI.PrintParkingError("The queue returned null", true);
                return true;
            }
            UI.PrintParkingInfo(vehicle);
            if (_inputMode == InputMode.Manual) {
                vehicle = UI.SetVehicleFromInput(vehicle);
            }
            couldParkCar = Park(vehicle);
            if (!couldParkCar) {
                UI.PrintParkingError($"The {vehicle.GetType().Name} could not fit", true);
            }
            return true;
        }
    }
}
