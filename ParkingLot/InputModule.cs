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
        internal static Command GetCommand() {
            ConsoleKey key = Console.ReadKey(true).Key;
            return KeyToCommand(key);
        }
        internal static Command KeyToCommand(ConsoleKey key) => key switch {
            ConsoleKey.P => Command.Park,
            ConsoleKey.U => Command.Unpark,
            ConsoleKey.Q => Command.Quit,
            ConsoleKey.A => Command.ToggleInput,
            _ => Command.DoNothing,
        };
    }
}
