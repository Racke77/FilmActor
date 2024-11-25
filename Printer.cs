using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmActor
{
    internal class Printer
    {
        #region Menu
        public void PrintMenuNormal(Menu menu)
        {
            foreach (string menuOption in menu.MenuList)
            {
                if (menu.MenuList.IndexOf(menuOption) == menu.MenuSelect)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.WriteLine(menuOption);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
        #endregion
        #region PrintingDb
        public void PrintSingleColumn(SqlDataReader answer)
        {
            while (answer.Read()) //read each row until it has no more
            {
                Console.WriteLine(answer[0]); //display column
            }
        }
        public void PrintTwoColumns(SqlDataReader answer)
        {
            while (answer.Read()) //read each row until it has no more
            {
                Console.WriteLine($"{answer[0]} {answer[1]}"); //display columns
            }
        }
        #endregion
        public void PrintFailureToFind()
        {
            Console.WriteLine("This actor has no films.");
        }
    }
}
