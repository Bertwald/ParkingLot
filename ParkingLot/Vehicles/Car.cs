
namespace ParkingDeluxe.Vehicles
{
    internal class Car : Vehicle {
        internal bool IsElectric { get; set; }
        internal Car() {
            IsElectric = Random.Next()%2 == 0;
            Size = 2;
        }

        public override string ToString() {
            // Ex Output: Plats 1 Bil ABC123 Röd Elbil
            return $"Plats {ParkingInterval} \tBil\t {LicenseNumber} \t {Color} \t {(IsElectric ? "Elbil" : "Fossilbil")}";
        }
    }
}
