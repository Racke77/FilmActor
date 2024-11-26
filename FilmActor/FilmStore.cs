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
        public int FoundActor;
        public List<string> Actors;
        public List<string> ActorsId;
        public FilmStore()
        {
            DbContext = new SakilaDbContext();
            Menu = new Menu();
            Actors = new List<string>();
            ActorsId = new List<string>();
        }
        #region Main Program
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
                        OpenCloseExecutePrintDouble();
                        Console.ReadLine();
                        break;
                    case 1: //Find films by actor
                        Console.Clear();
                        SearchForActor(); //update query
                        Menu.StartingMenu(); //return to starting-menu
                        Console.ReadLine();
                        break;
                    case 2: //Display all films
                        Console.Clear();
                        DbContext.DisplayAllFilmTitlesQuery();//update query
                        OpenCloseExecutePrintSingle();
                        Console.ReadLine();
                        break;
                    case 3: //Exit program
                        Environment.Exit(1);
                        break;
                }
            }
        }
        #endregion
        #region Choose how to search
        public void SearchForActor() //for inputting name and updating dbContext query
        {
            Menu.FindActorByXOptions();
            switch (Menu.MenuSelectionNormal())
            {
                case 0: //full name
                    string firstName = Input.InputName("first");
                    string lastName = Input.InputName("last");
                    FoundManyActors(firstName, lastName);
                    break;
                case 1: //first name                    
                    firstName = Input.InputName("first"); //ask for input
                    FoundManyActors(firstName, "");
                    break;
                case 2: //last name
                    lastName = Input.InputName("last");
                    FoundManyActors("", lastName);
                    break;
            }
        }
        #endregion
        #region Found Many Actors Checker
        public void FoundManyActors(string firstName, string lastName)
        {
            DbContext.FoundManyActorsQuery(firstName, lastName); //update query
            DbContext.OpenConnection(); //open
            var answer = DbContext.CreateCommand().ExecuteReader();
            FoundManyMenuPreparation(answer);
            DbContext.CloseConnection(); //close
            if (FoundActor > 1)
            {
                Menu.MenuUpdate(Actors);
                DbContext.DisplayFilmByActorIdQuery(ActorsId[Menu.MenuSelectionNormal()]); //select from menu and update query with selected id
                OpenCloseExecutePrintSingle();
            }
            else //if only a single actor is found
            {
                if (lastName == "")
                {
                    DbContext.FindActorByFirstNameQuery(firstName); //call for films query-update
                }
                else if (firstName == "")
                {
                    DbContext.FindActorByLastNameQuery(lastName);
                }
                else
                {
                    DbContext.FindActorByFullNameQuery(firstName, lastName);
                }
                OpenCloseExecutePrintSingle();
            }
        }
        public void FoundManyMenuPreparation(SqlDataReader answer)
        {
            FoundActor = 0;
            if (Actors != null)
            {
                Actors.Clear();
                ActorsId.Clear();
            }
            while (answer.Read()) //read each row until it has no more
            {
                Actors.Add($"{answer[1]} {answer[2]}"); //create name-list for menu
                ActorsId.Add($"{answer[0]}"); //create id-list for menu
                FoundActor++;
            }
        }
        #endregion
        #region SingleColumnAnswer
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
        public void OpenCloseExecutePrintSingle()
        {
            Console.Clear();
            DbContext.OpenConnection(); //open
            var answer = DbContext.CreateCommand().ExecuteReader();
            SingleColumnAnswer(answer);
            DbContext.CloseConnection(); //close
        }
        #endregion
        #region DoubleColumnAnswer
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
        public void OpenCloseExecutePrintDouble()
        {
            Console.Clear();
            DbContext.OpenConnection(); //open
            var answer = DbContext.CreateCommand().ExecuteReader();
            TwoColumnAnswer(answer);
            DbContext.CloseConnection();
        }
        #endregion

    }
}
