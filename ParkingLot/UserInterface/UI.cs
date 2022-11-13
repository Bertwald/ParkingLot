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
        internal static void ShowMenu(int row) {
            Console.SetCursorPosition(0, row);
            PrintMenuOptions();
        }
        internal static void FullParkingNotification(int row) {
            Console.SetCursorPosition(0, row);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[PARKERINGEN FULL]");
            Console.ForegroundColor = ConsoleColor.Gray;
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
            Console.WriteLine("Ange antal passagerare (50-100) följt av ENTER");
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
            Console.WriteLine("Är det en Elbil? (True/False) Följt av ENTER");
            input.IsElectric = InputModule.GetBool();
            return input;
        }
        private static Vehicle SetVehicleColorFromInput(Vehicle input) {
            PrintColorOptions();
            input.Color = Vehicle.s_colors[InputModule.GetIntInRange(0, Vehicle.s_colors.Length - 1)];
            return input;
        }
        private static void PrintMenuOptions() {
            Console.WriteLine("Välj funktion");
            Console.WriteLine("====");
            Console.WriteLine("P: Parkera ett fordon");
            Console.WriteLine("U: Checka ut ett fordon från parkeringen");
            Console.WriteLine("A: Växla inmatningsläge för fordon (manuellt, automatiskt)");
            Console.WriteLine("Q: Avsluta");
        }
        private static void PrintColorOptions() {
            Console.WriteLine("Välj färg på fordonet");
            Console.WriteLine("====");
            for (int option = 0; option < Vehicle.s_colors.Length; option++) {
                Console.WriteLine($"{option}: {Vehicle.s_colors[option]}");
            }
        }
        private static void PrintBrandOptions() {
            Console.WriteLine("Välj tillverkare");
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
            Console.WriteLine($"{vehicle.LicenseNumber} har lämnat parkeringen, kostnaden för parkeringen var {cost} kr");
            MessageDelay();
        }
        internal static void ShowUnparkingInstructions() {
            Console.WriteLine($"Ange registreringsnummer (format ABC123) för att checka ut ett fordon");
        }
        internal static void NotifyNewArrival(Vehicle vehicle) {
            Console.WriteLine($"{vehicle.LicenseNumber} ({vehicle.GetType().Name}) anlände till parkeringen för att parkeras");
            MessageDelay();
        }
    }
}
