namespace ParkingDeluxe {
    internal class InputModule {

        internal static string GetString() {
            while (true) {
                string? read = Console.ReadLine();
                if (read is not null) {
                    return read;
                }
            }
        }

        internal static int GetInt() {
            while (true) {
                string? read = Console.ReadLine();
                if (read is not null && Int32.TryParse(read, out int number)) {
                    return number;
                }
            }
        }

        internal static bool GetBool() {
            while (true) {
                string? read = Console.ReadLine();
                if (read is not null && bool.TryParse(read, out bool value)) {
                    return value;
                }
            }
        }

        internal static int GetIntInRange(int lower, int upper) {
            int number = int.MaxValue;
            while (number < lower || number > upper) {
                number = GetInt();
            }
            return number;
        }

        internal static string GetBrand() {
            throw new NotImplementedException();
        }

        internal static string GetColor() {
            throw new NotImplementedException();
        }
    }
}
