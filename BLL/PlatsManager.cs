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
    public class PlatsManager : IPlatsManager
    {
        // Création des références privées
        private IPlatsDB PlatsDb { get; }

        // Création du constructeur pour instancier la DAL
        public PlatsManager(IPlatsDB platsDb)
        {
            PlatsDb = platsDb;
        }

        // Liste des méthodes

        // Les Getters
        public int GetPlatID(string nom, int idRestaurant)
        {
            return PlatsDb.GetPlatID(nom, idRestaurant);
        }

        public string GetNomPlat(int idPlat)
        {
            return PlatsDb.GetNomPlat(idPlat);
        }

        public double GetPrixPlat(int idPlat)
        {
            return PlatsDb.GetPrixPlat(idPlat);
        }

        public List<Plats> GetPlats()
        {
            return PlatsDb.GetPlats();
        }

        public List<Plats> GetPlats(int idRestaurant)
        {
            return PlatsDb.GetPlats(idRestaurant);
        }
    }
}
