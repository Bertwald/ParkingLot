﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingDeluxe.Vehicles
{
    internal class Vehicle : IParkable
    {
        // Used by all Randomfunctions in all subclasses
        protected static Random Random = new Random();
        public string LicenseNumber { get; }
        public string Color { get; }

        public int Size { get; protected set; }
        public DateTime ParkingTime { get; protected set; }
        public string ParkingInterval { get; protected set; }

        protected Vehicle(string parkingInteval) {
            ParkingInterval = parkingInteval;
            Color = GenerateColor();
            LicenseNumber = GenerateLicenseNumer();
        }
        private static string GenerateLicenseNumer() {
            char[] chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            string ret = "";
            return ret + (chars[Random.Next(chars.Length)] + chars[Random.Next(chars.Length)] + chars[Random.Next(chars.Length)]);
        }
        private static string GenerateColor() {
            string[] colors = { "Röd", "Svart", "Gul", "Blå", "Vit", "Metallic", "Grön", "Rosa" };
            return colors[Random.Next(colors.Length)];
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