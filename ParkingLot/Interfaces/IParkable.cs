namespace ParkingDeluxe.Interfaces {
    internal interface IParkable : IPlaceable {
        internal DateTime ParkingTime { get; }
        internal string ParkingInterval { get; }
        internal void StartPark();
        internal int GetParkedTime();

    }
}
