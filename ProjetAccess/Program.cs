using DTO;
using BLL;
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
            // Pour reset la clé primaire dans les tables SQL : DBCC CHECKIDENT ('[TestTable]', RESEED, 0);


            // Méthodes de UtilisateurManager
            var userManager = new UtilisateursManager(Configuration);
            /*
            userManager.Subscribe(3960, "Salamin", "Bastien", "bastiensalamin@gmail.com", "123456", "Rte de Mura 3", "0793337658");

            var users = userManager.GetUtilisateurs();
            
            foreach (var user in users)
            {
                Console.WriteLine(user.ToString());
            }
            */
            var connexion = userManager.CanConnect("bastiensalamin@gmail.com", "123456");

            Console.WriteLine("Connexion réussie ? " + connexion);

            var userTest1 = userManager.GetUtilisateurs("bastiensalamin@gmail.com", "123456");

            Console.WriteLine(userTest1.ToString());

            var userTest2 = userManager.GetUserId(1);

            Console.WriteLine(userTest2.ToString());


            // Méthodes de CategoriesManager
            var categoriesManager = new CategoriesManager(Configuration);

            var categories = categoriesManager.GetCategories();
            
            foreach (var categorie in categories)
            {
                Console.WriteLine(categorie.ToString());
            }


            // Méthodes de CategoriesPlatsManager
            var categoriesPlatsManager = new CategoriesPlatsManager(Configuration);

            var categoriesPlats = categoriesPlatsManager.GetCategoriesPlats();

            foreach (var categoriesPlat in categoriesPlats)
            {
                Console.WriteLine(categoriesPlat.ToString());
            }


            // Méthodes de CommandesPlatsManager
            var commandesPlatsManager = new CommandesPlatsManager(Configuration);

            var commandesPlats = commandesPlatsManager.GetCommandesPlats();

            foreach (var commandesPlat in commandesPlats)
            {
                Console.WriteLine(commandesPlat.ToString());
            }


            // Méthodes de LivreursManager
            var livreursManager = new LivreursManager(Configuration);

            livreursManager.UpdateDisponibilite(1, true);

            var livreurs = livreursManager.GetLivreurs();

            foreach (var livreur in livreurs)
            {
                Console.WriteLine(livreur.ToString());
            }


            // Méthodes de LocalitesManager
            var localitesManager = new LocalitesManager(Configuration);

            var idLocalite = localitesManager.GetLocalite(3968);

            Console.WriteLine("IdLocalite : " + idLocalite);

            var localites = localitesManager.GetLocalites();

            foreach (var localite in localites)
            {
                Console.WriteLine(localite.ToString());
            }


            // Méthodes de PlatsManager
            var platsManager = new PlatsManager(Configuration);

            var prixPlat = platsManager.GetPrixPlat(1);

            Console.WriteLine("Prix du plat ayant l'IdPlat 1 : " + prixPlat);

            var idPlat = platsManager.GetPlatID("Menu Cheeseburger Royal", 1);

            Console.WriteLine("Id du plat 'Menu Cheeseburger Royal' du restaurant n°1 : " + idPlat);

            var plats = platsManager.GetPlats();

            foreach (var plat in plats)
            {
                Console.WriteLine(plat.ToString());
            }


            // Méthodes de RestaurantsManager
            var restaurantsManager = new RestaurantsManager(Configuration);

            var restaurants = restaurantsManager.GetRestaurants();

            foreach (var restaurant in restaurants)
            {
                Console.WriteLine(restaurant.ToString());
            }


            // Méthodes de RevuesManager
            var revuesManager = new RevuesManager(Configuration);
            /*
            revuesManager.AddRevue(1, 1, 5, "J'adore les cheeseburgers !");
            */
            var revues = revuesManager.GetRevues();

            foreach (var revue in revues)
            {
                Console.WriteLine(revue.ToString());
            }


            // Méthodes de CommandesManager
            var commandesManager = new CommandesManager(Configuration);
            /*
            var date = new DateTime(2021, 11, 7, 16, 0, 0);

            commandesManager.Order(1, 1, 27, date);
            */
            commandesManager.updateDelivery(1);

            var commandes = commandesManager.GetCommandes();

            foreach (var commande in commandes)
            {
                Console.WriteLine(commande.ToString());
            }

        }
    }
}
