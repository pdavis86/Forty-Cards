using System;
using System.Collections.Generic;

namespace TechnicalTestScaffoldDeveloper
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Write(Constants.Intro);

            string mode;
            do
            {
                mode = Console.ReadLine();
                switch (mode)
                {
                    case "1":
                    case "2":
                    case "3":
                        break;

                    default:
                        mode = null;
                        Console.WriteLine("Invalid selection, please try again:");
                        break;
                }
            } while (string.IsNullOrWhiteSpace(mode));

            Helpers.ConsoleHelper.WriteSpacingLines(2);

            switch (mode)
            {
                case "1":
                    GameLogic.RunQuestion1();
                    break;

                case "2":
                    GameLogic.RunQuestion2();
                    break;

                case "3":
                    GameLogic.RunQuestion3();
                    break;

                default:
                    throw new Exception($"Unexpected game mode {mode}");
            }

            Helpers.ConsoleHelper.WriteSpacingLines(2);

            Console.Write(Constants.Outro);
            Console.ReadKey(true);
        }


    }
}
