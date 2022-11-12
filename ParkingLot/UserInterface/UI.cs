using ParkingDeluxe.Vehicles;

namespace ParkingDeluxe.UserInterface {
    internal class UI {
        internal static void PrintParking<T>(IEnumerable<T> toPrint) {
            Console.SetCursorPosition(0, 0);
            foreach (T printable in toPrint) {
                if (printable is not null) {
                    Console.WriteLine(printable.ToString());
                }
            }
        }
        internal static void ShowMenu() {
            Console.SetCursorPosition(0, 18);
            PrintMenuOptions();
        }
        internal static Vehicle SetVehicleFromInput(Vehicle input) => input switch {
            //  Here we know    |    
            //  it is a Car     |    But here we do not!
            Car => SetCarFromInput((Car)input),
            Motorcycle => SetMotorcycleFromInput((Motorcycle)input),
            Bus => SetBusFromInput((Bus)input),
            _ => input,
        };
        private static Vehicle SetBusFromInput(Bus input) {
            SetVehicleColorFromInput(input);
            Console.WriteLine();
            Console.WriteLine("Give passenger capacity in range 50-100");
            input.PassengerCapacity = InputModule.GetIntInRange(50, 100);
            return input;
        }
        private static Vehicle SetMotorcycleFromInput(Motorcycle input) {
            SetVehicleColorFromInput(input);
            PrintBrandOptions();
            input.Brand = Motorcycle.s_brands[InputModule.GetIntInRange(0, Motorcycle.s_brands.Length - 1)];
            return input;
        }
        private static Vehicle SetCarFromInput(Car input) {
            SetVehicleColorFromInput(input);
            Console.WriteLine("Is the Car electric? (True/False)");
            input.IsElectric = InputModule.GetBool();
            return input;
        }
        private static Vehicle SetVehicleColorFromInput(Vehicle input) {
            PrintColorOptions();
            input.Color = Vehicle.s_colors[InputModule.GetIntInRange(0, Vehicle.s_colors.Length - 1)];
            return input;
        }
        private static void PrintMenuOptions() {
            Console.WriteLine("Choose an action");
            Console.WriteLine("====");
            Console.WriteLine("P: Park a vehicle");
            Console.WriteLine("U: Unpark a vehicle");
            Console.WriteLine("A: Toggle vehicle input mode");
            Console.WriteLine("Q: Quit");
        }
        private static void PrintColorOptions() {
            Console.WriteLine("ColorOptions");
            Console.WriteLine("====");
            for (int option = 0; option < Vehicle.s_colors.Length; option++) {
                Console.WriteLine($"{option}: {Vehicle.s_colors[option]}");
            }
        }
        private static void PrintBrandOptions() {
            Console.WriteLine("BrandOptions");
            Console.WriteLine("====");
            for (int option = 0; option < Motorcycle.s_brands.Length; option++) {
                Console.WriteLine($"{option}: {Motorcycle.s_brands[option]}");
            }
        }
        private static void MessageDelay() {
            Thread.Sleep(1000);
        }
        internal static void PrintParkingError(string msg, bool isParking) {
            Console.WriteLine($"{(isParking ? "Unable to park because: " : "Could not perform the desired Unparking action due to:")} " + msg);
            MessageDelay();
        }
        internal static void PrintUnParkingInfo(Vehicle vehicle, double cost) {
            Console.WriteLine($"{vehicle.LicenseNumber} is no longer parked, the cost of said parking was {cost} kr");
            MessageDelay();
        }
        internal static void ShowUnparkingInstructions() {
            Console.WriteLine($"Active Choice: Unpark a Vehicle");
            Console.WriteLine($"Give a licensenumber (format XXXNNN)");
        }
        internal static void PrintParkingInfo(Vehicle vehicle) {
            Console.WriteLine($"{vehicle.GetType().Name} arrives to the deluxe parking");
            MessageDelay();
        }
    }
}
