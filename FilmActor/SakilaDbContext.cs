using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmActor
{
    internal class SakilaDbContext
    {
        private SqlConnection Connection = new SqlConnection(
            "Data Source = (localdb)\\MSSQLLocalDB; " +
            "Initial Catalog = Sakila; Integrated Security = True; " +
            "Connect Timeout = 30; Encrypt = False; Trust Server Certificate = False; " +
            "Application Intent = ReadWrite; Multi Subnet Failover = False");
        private string CommandString = "";

        #region Queries
        public void FindActorByFullNameQuery(string firstName, string lastName)
        {
            string commandString =
                "select film.title from film_actor " +
                "inner join actor on film_actor.actor_id = actor.actor_id " +
                "inner join film on film_actor.film_id = film.film_id " +
                $"where first_name like '{firstName}' and last_name like '{lastName}'";
            CommandString = commandString;
        }
        public void FindActorByFirstNameQuery(string firstName)
        {
            string commandString =
                "select film.title from film_actor " +
                "inner join actor on film_actor.actor_id = actor.actor_id " +
                "inner join film on film_actor.film_id = film.film_id " +
                $"where first_name like '{firstName}'";
            CommandString = commandString;
        }
        public void FindActorByLastNameQuery(string lastName)
        {
            string commandString =
                "select film.title from film_actor " +
                "inner join actor on film_actor.actor_id = actor.actor_id " +
                "inner join film on film_actor.film_id = film.film_id " +
                $"where last_name like '{lastName}'";
            CommandString = commandString;
        }
        public void DisplayAllActorsQuery()
        {
            string commandString = 
                "select first_name, last_name from actor";
            CommandString = commandString;
        }
        public void DisplayAllFilmTitlesQuery()
        {
            string commandString =
                "select title from film";
            CommandString = commandString;
        }
        #endregion

        public SqlCommand CreateCommand()
        {
            var command = new SqlCommand(CommandString, Connection);
            return command;
        }
        public void OpenConnection()
        {
            Connection.Open();
        }
        public void CloseConnection()
        {

            Connection.Close();
        }

    }
}
