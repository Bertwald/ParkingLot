using ParkingDeluxe.Vehicles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ParkingDeluxe {
    internal class UI {
        internal static void PrintParking<T>(IEnumerable<T> toPrint) {
            Console.SetCursorPosition(0, 0);
            foreach(T printable in toPrint) {
                Console.WriteLine(printable.ToString());
            }
        }
        internal static void ShowMenu() {
            Console.SetCursorPosition(0, 18);
            PrintMenuOptions();
        }
        internal static Vehicle? SetVehicleFromInput(Vehicle input) => input switch {
            //  Here we know    |    
            //  it is a Car     |    But here we do not!
            Car => SetCarFromInput((Car)input),
            Motorcycle => SetMotorcycleFromInput((Motorcycle)input),
            Bus => SetBusFromInput((Bus)input),
            _ => null,
        };

        private static Vehicle SetBusFromInput(Bus input) {
            SetVehicleColorFromInput(input);
            Console.WriteLine();
            Console.WriteLine("Give passengerCapacity in range 50-100");
            input.PassengerCapacity = InputModule.GetIntInRange(50, 100);
            return input;
        }

        private static Vehicle SetMotorcycleFromInput(Motorcycle input) {
            SetVehicleColorFromInput(input);
            PrintBrandOptions();
            input.Brand = Motorcycle.brands[InputModule.GetIntInRange(0, Motorcycle.brands.Length - 1)];
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
            input.Color = Vehicle.colors[InputModule.GetIntInRange(0,Vehicle.colors.Length-1)];
            return input;
        }

        private static void PrintMenuOptions() {
            Console.WriteLine("Choose an action");
            Console.WriteLine("====");
            Console.WriteLine("P: Park a vehicle");
            Console.WriteLine("U: Unpark a vehicle");
            Console.WriteLine("A : Toggle manual vehicle Info");
            Console.WriteLine("Q: Quit");
        }
        private static void PrintColorOptions() {
            Console.WriteLine("ColorOptions");
            Console.WriteLine("====");
            for(int option = 0; option < Vehicle.colors.Length; option++) {
                Console.WriteLine($"{option}: {Vehicle.colors[option]}");
            }
        }
        private static void PrintBrandOptions() {
            Console.WriteLine("BrandOptions");
            Console.WriteLine("====");
            for (int option = 0; option < Motorcycle.brands.Length; option++) {
                Console.WriteLine($"{option}: {Motorcycle.brands[option]}");
            }
        }
        private static void MessageDelay() {
            Thread.Sleep(2000);
        }

        internal static void PrintUnParkingError(string msg) {
            Console.WriteLine("Could not perform the desired Unparking action due to: " + msg);
            MessageDelay();
        }

        internal static void PrintUnParkingInfo(Vehicle vehicle, double cost) {
            Console.WriteLine($"{vehicle.LicenseNumber} is no longer parked, the cost of said parking was {cost} kr");
            MessageDelay();
        }

        internal static void ShowUnparkingInstruction() {
            Console.WriteLine($"Give a licensenumber (format XXXNNN)");
        }
    }
}
