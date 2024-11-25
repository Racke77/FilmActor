using Microsoft.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using System.Numerics;
namespace FilmActor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FilmStore store = new FilmStore();
            store.Storage();
        }
    }
}
