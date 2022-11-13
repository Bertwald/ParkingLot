
namespace ParkingDeluxe.Vehicles {
    internal class Motorcycle : Vehicle {
        internal string Brand { get; set; }
        internal static readonly string[] s_brands = { "Harley", "Yamaha", "Honda", "Kawasaki" };
        internal Motorcycle() {
            Brand = GenerateBrand();
            Size = 1;
        }

        private static string GenerateBrand() {
            return s_brands[s_random.Next(s_brands.Length)];
        }

        public override string ToString() {
            // Ex Output: Plats 2 MC GHJ456 Svart Harley
            return $"Plats {ParkedInInterval} \tMC\t {LicenseNumber} \t {Color} \t {Brand}   \t| Tid Parkerad: {TimeOfParking}";
        }
    }
}
