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
        // Création de références privées
        private IRestaurantsDB RestaurantsDb { get; }

        //Création du constructeur pour instancier la DAL
        public RestaurantsManager(IRestaurantsDB restaurantsDb)
        {
            RestaurantsDb = restaurantsDb;
        }

        //liste des méthodes

        //les getters

        public List<Restaurants> GetRestaurants()
        {
            return RestaurantsDb.GetRestaurants();
        }
    }
}
