
namespace ParkingDeluxe.Vehicles {
    internal class Bus : Vehicle {
        internal int PassengerCapacity { get; set; }
        internal Bus() {
            PassengerCapacity = GeneratePassengerCapacity();
            Size = 4;
        }
        private static int GeneratePassengerCapacity() {
            return 5 * s_random.Next(10, 21);
        }
        public override string ToString() {
            // Ex Output: Plats 3-4 Buss LKJ223 Gul 55
            return $"Plats {ParkingInterval} \tBuss\t {LicenseNumber} \t {Color} \t {PassengerCapacity}";
        }
    }
}
