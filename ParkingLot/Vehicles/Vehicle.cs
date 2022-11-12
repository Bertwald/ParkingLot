﻿
using ParkingDeluxe.Interfaces;

namespace ParkingDeluxe.Vehicles {
    internal class Vehicle : IParkable {
        internal static readonly string[] s_colors = { "Röd", "Svart", "Gul", "Blå", "Vit", "Grå", "Grön", "Rosa" };
        // Used by all Randomfunctions in all subclasses
        protected static Random s_random = new();
        public string LicenseNumber { get; }
        internal string Color { get; set; }
        public int Size { get; protected set; }
        public DateTime ParkingTime { get; protected set; }
        public string ParkingInterval { get; set; }

        protected Vehicle(string parkingInteval) {
            ParkingInterval = parkingInteval;
            Color = GenerateColor();
            LicenseNumber = GenerateLicenseNumer();
        }
        internal Vehicle() {
            ParkingInterval = "Unparked";
            Color = GenerateColor();
            LicenseNumber = GenerateLicenseNumer();
        }
        private static string GenerateLicenseNumer() {
            return new string(Enumerable.Range(1, 3).Select(_ => (char)(s_random.Next(65, 91))).ToArray()) +
                   new string(Enumerable.Range(1, 3).Select(_ => (char)s_random.Next(48, 58)).ToArray());
        }
        private static string GenerateColor() {
            return s_colors[s_random.Next(s_colors.Length)];
        }

        public void StartPark() {
            ParkingTime = DateTime.Now;
        }
        public int GetParkedTime() {
            //The difference in time as minutes rounded up
            return ((int)(DateTime.Now.Subtract(ParkingTime)).TotalMinutes) + 1;
        }
    }
}
