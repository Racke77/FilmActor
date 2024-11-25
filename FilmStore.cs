using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmActor
{
    internal class FilmStore
    {
        public SakilaDbContext DbContext;
        public Menu Menu;
        public FilmStore()
        {
            DbContext = new SakilaDbContext();
            Menu = new Menu();
        }
        public void Storage()
        {
            while (true)
            {
                int chosenOption = Menu.MenuSelectionNormal();

                switch (chosenOption)
                {
                    case 0: //Display all actors
                        Console.Clear();
                        DbContext.DisplayAllActorsQuery(); //update query
                        DbContext.OpenConnection(); //open
                        var answer = DbContext.CreateCommand().ExecuteReader();
                        TwoColumnAnswer(answer);
                        DbContext.CloseConnection(); //close
                        Console.ReadLine();
                        break;
                    case 1: //Find films by actor
                        Console.Clear();
                        SearchForActor(); //update query
                        DbContext.OpenConnection(); //open
                        answer = DbContext.CreateCommand().ExecuteReader();
                        SingleColumnAnswer(answer);
                        DbContext.CloseConnection(); //close
                        Menu.StartingMenu();
                        Console.ReadLine();
                        break;
                    case 2: //Display all films
                        Console.Clear();
                        DbContext.DisplayAllFilmTitlesQuery();//update query
                        DbContext.OpenConnection(); //open
                        answer = DbContext.CreateCommand().ExecuteReader();
                        SingleColumnAnswer(answer);
                        DbContext.CloseConnection(); //close
                        Console.ReadLine();
                        break;
                    case 3: //Exit program
                        Environment.Exit(1);
                        break;
                }
            }
        }
        public void SearchForActor() //for inputting name and updating dbContext query
        {
            Menu.FindActorByXOptions();
            switch (Menu.MenuSelectionNormal())
            {
                case 0: //full name
                    string firstName = Input.InputName("first");
                    string lastName = Input.InputName("last");
                    DbContext.FindActorByFullNameQuery(firstName, lastName);
                    break;
                case 1: //first name                    
                    firstName = Input.InputName("first");
                    DbContext.FindActorByFirstNameQuery(firstName);
                    break;
                case 2: //last name
                    lastName = Input.InputName("last");
                    DbContext.FindActorByLastNameQuery(lastName);
                    break;
            }
        }
        public void SingleColumnAnswer(SqlDataReader answer)
        {
            if (answer.HasRows) //does it exist?
            {
                Menu.Printer.PrintSingleColumn(answer);
            }
            else
            {
                Menu.Printer.PrintFailureToFind();
            }
        }
        public void TwoColumnAnswer(SqlDataReader answer)
        {
            if (answer.HasRows) //does it exist?
            {
                Menu.Printer.PrintTwoColumns(answer);
            }
            else
            {
                Menu.Printer.PrintFailureToFind();
            }
        }
    }
}
