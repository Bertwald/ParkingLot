using ParkingDeluxe.Vehicles;

namespace ParkingDeluxe.Logic {
    /**
     * Represents a continual stream of vehicles to a parkinghouse
     * Will Fill itself randomly at the back end when getting a member from the front
     * IFF size is less than a given number.
     * Can also manually enqueue Vehicles
     */
    internal class ParkingQueue {
        private static readonly Random s_random = new();
        private static readonly int s_targetLength = 10;
        private readonly Queue<Vehicle> _vehicleQueue = new();

        internal ParkingQueue() {
            for (int n = 0; n < s_targetLength; n++) {
                _vehicleQueue.Enqueue(GetRandomVehicle(s_random.Next()));
            }
        }
        internal void Enqueue(Vehicle vehicle) {
            _vehicleQueue.Enqueue(vehicle);
        }
        internal Vehicle Dequeue() {
            if (_vehicleQueue.Count <= s_targetLength) {
                _vehicleQueue.Enqueue(GetRandomVehicle(s_random.Next()));
            }
            return _vehicleQueue.Dequeue();
        }
        private static Vehicle GetRandomVehicle(int rand) => (rand % 3) switch {
            0 => new Car(),
            1 => new Bus(),
            2 => new Motorcycle(),
            _ => new Bus(),
        };
    }
}
