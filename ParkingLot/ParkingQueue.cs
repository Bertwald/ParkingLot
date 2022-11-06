using ParkingDeluxe.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingDeluxe {
    /**
     * Represents a continual stream of vehicles to a parkinghouse
     * Will Fill itself randomly at the back end when getting a member from the front
     * IFF size is less than a given number.
     * Can also manually enqueue Vehicles
     */
    internal class ParkingQueue {
        private static Random random = new Random();
        private static readonly int targetLength = 10;
        private Queue<Vehicle> vehicleQueue = new Queue<Vehicle>();

        internal ParkingQueue() {
            for(int n = 0; n < targetLength; n++) {
                vehicleQueue.Enqueue(GetRandomVehicle(random.Next()));
            }
        }

        internal void Enqueue(Vehicle vehicle) {
            vehicleQueue.Enqueue(vehicle);
        }

        internal Vehicle Dequeue() {
            if (vehicleQueue.Count <= targetLength) {
                vehicleQueue.Enqueue(GetRandomVehicle(random.Next()));
            }
            return vehicleQueue.Dequeue();
        }

        internal static Vehicle GetRandomVehicle(int rand) => (rand % 3) switch {
            0 => new Car(),
            1 => new Bus(),
            2 => new Motorcycle(),
            _ => new Car(),
        };
    }
}
