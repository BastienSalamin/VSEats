using System;
using DAL;
using DTO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class CategoriesPlatsManager : ICategoriesPlatsManager
    {

        // Création de deux références privées
        private ICategoriesPlatsDB CategoriesPlatsDb { get; }

        // Création du constructeur pour instancier la DAL
        public CategoriesPlatsManager(ICategoriesPlatsDB categoriesPlatsDb)
        {
            CategoriesPlatsDb = categoriesPlatsDb;
        }

        //liste des méthodes utilisateurs

        //les getters
        public List<CategoriesPlats> GetCategoriesPlats()
        {
            return CategoriesPlatsDb.GetCategoriesPlats();
        }
    }
}
