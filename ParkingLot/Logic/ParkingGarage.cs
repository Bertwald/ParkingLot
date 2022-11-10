using ParkingDeluxe.UserInterface;
using ParkingDeluxe.Vehicles;


namespace ParkingDeluxe.Logic
{
    internal enum InputMode
    {
        Manual,
        Automatic,
        MAX
    }
    internal enum Command
    {
        Park,
        Unpark,
        ToggleInput,
        Quit,
        DoNothing
    }
    internal class ParkingGarage
    {
        private static readonly double _parkingFee = 1.50D;
        private static readonly bool _sizeBasedFee = false;
        private readonly int _numberOfSlots;
        private readonly bool[] _isIndexOccupied;
        private readonly Dictionary<Vehicle, int> _parkedVehicles;
        private readonly ParkingQueue _queue;
        internal InputMode _inputMode;

        internal ParkingGarage(int slots, ParkingQueue queue)
        {
            _numberOfSlots = slots;
            _isIndexOccupied = new bool[_numberOfSlots * 2];
            _parkedVehicles = new Dictionary<Vehicle, int>();
            _queue = queue;
            _inputMode = InputMode.Manual;
        }
        internal bool Park(Vehicle vehicle)
        {
            bool skipOne = false;
            for (int i = 0; i < _isIndexOccupied.Length; i += vehicle.Size)
            {
                if (!_isIndexOccupied[i] && i + vehicle.Size <= _isIndexOccupied.Length)
                {
                    for (int j = 0; j < vehicle.Size; j++)
                    {
                        if (_isIndexOccupied[i + j] == true)
                        {
                            skipOne = true;
                            continue;
                        }
                    }
                    if (skipOne == true)
                    {
                        skipOne = false;
                        continue;
                    }
                    _parkedVehicles.Add(vehicle, i);
                    vehicle.StartPark();
                    vehicle.ParkingInterval = vehicle.Size <= 2 ? (i >> 1).ToString() : (i >> 1) + "-" + (i + vehicle.Size - 1 >> 1).ToString();
                    for (int j = 0; j < vehicle.Size; j++)
                    {
                        _isIndexOccupied[i + j] = true;
                    }
                    return true;
                }
            }
            return false;
        }
        internal double UnPark(Vehicle vehicle)
        {
            //Unblock parking lot
            int startindex = _parkedVehicles[vehicle];
            for (int i = 0; i < vehicle.Size; i++)
            {
                _isIndexOccupied[startindex + i] = false;
            }
            _parkedVehicles.Remove(vehicle);
            return _sizeBasedFee ? vehicle.GetParkedTime() * _parkingFee * vehicle.Size / 2 : vehicle.GetParkedTime() * _parkingFee;
        }

        internal void ListParkedCars()
        {
            var vehicles = from data in _parkedVehicles
                           orderby data.Value
                           select data.Key;
            UI.PrintParking<Vehicle>(vehicles);
        }

        internal void RunMainLoop()
        {
            bool continueRunning;
            do
            {
                Console.Clear();
                ListParkedCars();
                PrintInputMode();
                UI.ShowMenu();
                Command command = InputModule.GetCommand();
                continueRunning = ProcessCommand(command);
            } while (continueRunning);
        }

        private void PrintInputMode()
        {
            Console.WriteLine("Active InputMode : " + _inputMode);
        }

        private bool ProcessCommand(Command command) => command switch
        {
            Command.Park => ParkingProcess(),
            Command.Unpark => UnParkingProcess(),
            Command.ToggleInput => ToggleInputMode(),
            Command.DoNothing => true,
            _ => false,
        };

        private bool ToggleInputMode()
        {
            _inputMode = (InputMode)((int)(_inputMode + 1) % (int)InputMode.MAX);
            return true;
        }

        private bool UnParkingProcess()
        {
            UI.ShowUnparkingInstruction();
            string licenceNumber = InputModule.GetString();
            var vehicles = from vehicle in _parkedVehicles
                           where vehicle.Key.LicenseNumber == licenceNumber
                           select vehicle.Key;
            if (vehicles is null || !vehicles.Any())
            {
                UI.PrintParkingError("Could not find any car with that licensenumber", false);
                return true;
            }
            else
            {
                var vehicle = vehicles.First();
                double cost = UnPark(vehicles.First());
                UI.PrintUnParkingInfo(vehicle, cost);
                return true;
            }
        }
        private bool ParkingProcess()
        {
            bool success;
            var vehicle = _queue.Dequeue();
            if (vehicle is null)
            {
                UI.PrintParkingError("The queue returned null", true);
                return true;
            }
            UI.PrintParkingInfo(vehicle);
            if (_inputMode == InputMode.Manual)
            {
                vehicle = UI.SetVehicleFromInput(vehicle);
            }
            success = Park(vehicle);
            if (!success)
            {
                UI.PrintParkingError($"The {vehicle.GetType().Name} could not fit", true);
            }
            return true;
        }
    }
}
