using ParkingDeluxe.Vehicles;

namespace ParkingDeluxe {
    internal class Program {
        static void Main(string[] args) {

            //Car car = new("1");
            //Motorcycle motorcycle1 = new("2");
            //Motorcycle motorcycle2 = new("2");
            //Bus bus1 = new("3-4");
            //Console.WriteLine(car);
            //Console.WriteLine(motorcycle1);
            //Console.WriteLine(motorcycle2);
            //Console.WriteLine(bus1);

            //Create objects
            Console.CursorVisible = false;
            ParkingGarage parkingGarage = new(15, new ParkingQueue());
            //parkingGarage.Park(motorcycle1);
            //parkingGarage.Park(car);
            //parkingGarage.Park(bus1);
            //parkingGarage.Park(motorcycle2);
            parkingGarage.Update();
            //parkingGarage.ListParkedCars();

            //Start Main loop

        }
    }
}