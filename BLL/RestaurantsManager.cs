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
    public class RestaurantsManager
    {
        // Création de références privées
        private IRestaurantsDB RestaurantsDb { get; }

        //Création du constructeur pour instancier la DAL
        public RestaurantsManager(IConfiguration configuration)
        {
            RestaurantsDb = new RestaurantsDB(configuration);
        }

        //liste des méthodes

        //les getters

        public List<Restaurants> GetRestaurants()
        {
            return RestaurantsDb.GetRestaurants();
        }
    }
}
