using System;

namespace TechnicalTestScaffoldDeveloper.Helpers
{
    public static class ConsoleHelper
    {
        public static void WriteSpacingLines(int num)
        {
            for (int i = 0; i < num; i++)
            {
                Console.WriteLine("");
            }
        }

        public static void OverwriteLastLine(string value)
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write(" ");
            }
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(value);
        }
    }
}
