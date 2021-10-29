using DAL;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ProjetAccess
{
    class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        static void Main(string[] args)
        {
            var platsDB = new PlatsDB(Configuration);

            var plats = platsDB.GetPlats();

            if(plats != null)
            {
                foreach (var m in plats)
                {
                    Console.WriteLine(m.ToString());
                    Console.WriteLine("Salut ça va ?");
                }
            }
            else
            {
                Console.WriteLine("Table vide :(");
            }

        }
    }
}
