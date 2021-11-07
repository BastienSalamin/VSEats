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
    public class PlatsManager
    {
        // Création de références privées
        private IPlatsDB PlatsDb { get; }

        // Création du constructeur pour instancier la DAL
        public PlatsManager(IConfiguration configuration)
        {
            PlatsDb = new PlatsDB(configuration);
            
        }

        //liste des méthodes

        //les getters
        public int GetPlatID(string nom, int idRestaurant)
        {
            return PlatsDb.GetPlatID(nom, idRestaurant);
        }

        public double GetPrixPlat(int idPlat)
        {
            return PlatsDb.GetPrixPlat(idPlat);
        }

        public List<Plats> GetPlats()
        {
            return PlatsDb.GetPlats();
        }
    }
}
