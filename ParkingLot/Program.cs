using ParkingDeluxe.Logic;

namespace ParkingDeluxe
{
    internal class Program {
        static void Main(string[] args) {
            Console.CursorVisible = false;
            ParkingGarage parkingGarage = new(15, new ParkingQueue());
            parkingGarage.RunMainLoop();
        }
    }
}