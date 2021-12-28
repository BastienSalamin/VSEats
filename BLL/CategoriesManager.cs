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
    public class CategoriesManager : ICategoriesManager
    {
        // Création de références privées
        private ICategoriesDB CategoriesDb { get; }

        // Création du constructeur pour instancier la DAL
        public CategoriesManager(ICategoriesDB categoriesDb)
        {
            CategoriesDb = categoriesDb;

        }

        //liste des méthodes utilisateurs

        //les getters
        public List<Categories> GetCategories()
        {
            return CategoriesDb.GetCategorie();
        }
    }
}
