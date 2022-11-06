using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingDeluxe.Vehicles
{
    internal class Motorcycle : Vehicle {
        internal string Brand { get; }
        internal Motorcycle(string parkingInterval) : base(parkingInterval) {
            Brand = GenerateBrand();
        }

        private static string GenerateBrand() {
            string[] brands = {"Harley", "Yamaha", "Honda", "Kawasaki" };
            return brands[Random.Next(brands.Length)];
        }

        public override string ToString() {
            // Ex Output: Plats 2 MC GHJ456 Svart Harley
            return $"Plats {ParkingInterval} \tMC\t {LicenseNumber} \t {Color} \t {Brand}";
        }
    }
}
