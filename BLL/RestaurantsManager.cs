using System;
using DTO;
using DAL;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class RestaurantsManager : IRestaurantsManager
    {
        // Création des références privées
        private IRestaurantsDB RestaurantsDb { get; }

        // Création du constructeur pour instancier la DAL
        public RestaurantsManager(IRestaurantsDB restaurantsDb)
        {
            RestaurantsDb = restaurantsDb;
        }

        // Liste des méthodes

        // Les getters
        public List<Restaurants> GetRestaurants()
        {
            return RestaurantsDb.GetRestaurants();
        }

        public List<Restaurants> GetRestaurants(int idLocalite)
        {
            return RestaurantsDb.GetRestaurants(idLocalite);
        }
    }
}
