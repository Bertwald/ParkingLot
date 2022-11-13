namespace ParkingDeluxe.Interfaces {
    internal interface IParkable : IMeasurable {
        internal DateTime TimeOfParking { get; }
        internal string ParkedInInterval { get; }
        internal void StartParkingTimer();
        internal int GetParkedTimeInMinutes();

    }
}
