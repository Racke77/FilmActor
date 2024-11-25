using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmActor
{
    internal class Input
    {
        public static string InputName(string desiredName)
        {
            Console.Clear();
            while (true)
            {
                Console.WriteLine($"Please input their {desiredName} name:");
                Console.CursorVisible = true;
                string? inputString = Console.ReadLine();

                if (inputString != null && inputString != "")
                {
                    bool containsInt = inputString.Any(char.IsDigit);
                    if (!containsInt)
                    {
                        return inputString;
                    }
                    else
                    {
                        Console.WriteLine("Please do not include numbers.");
                    }
                }
                else
                {
                    Console.WriteLine("It needs to be an actual name.");
                }
            }
        }
    }
}
