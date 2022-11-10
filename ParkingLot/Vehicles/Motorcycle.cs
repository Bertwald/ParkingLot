
namespace ParkingDeluxe.Vehicles
{
    internal class Motorcycle : Vehicle {
        internal string Brand { get; set; }
        internal static readonly string[] brands = { "Harley", "Yamaha", "Honda", "Kawasaki" };
        internal Motorcycle() {
            Brand = GenerateBrand();
            Size = 1;
        }

        private static string GenerateBrand() {
            return brands[Random.Next(brands.Length)];
        }

        public override string ToString() {
            // Ex Output: Plats 2 MC GHJ456 Svart Harley
            return $"Plats {ParkingInterval} \tMC\t {LicenseNumber} \t {Color} \t {Brand}";
        }
    }
}
