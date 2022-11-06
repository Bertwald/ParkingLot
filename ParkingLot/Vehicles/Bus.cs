using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingDeluxe.Vehicles
{
    internal class Bus : Vehicle {
        internal int PassengerCapacity { get; private set; }
        internal Bus(string parkingInterval) : base(parkingInterval) {
            PassengerCapacity = GeneratePassengerCapacity();
        }
        public Bus() {
            PassengerCapacity = GeneratePassengerCapacity();
            Size = 4;
        }

        private int GeneratePassengerCapacity() {
            return 5 * Random.Next(10, 21);
        }

        public override string ToString() {
            // Ex Output: Plats 3-4 Buss LKJ223 Gul 55
            return $"Plats {ParkingInterval} \tBuss\t {LicenseNumber} \t {Color} \t {PassengerCapacity}";
        }
    }
}
