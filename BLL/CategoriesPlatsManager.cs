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
    public class CategoriesPlatsManager
    {

        // Création de deux références privées
        private ICategoriesPlatsDB CategoriesPlatsDb { get; }

        // Création du constructeur pour instancier la DAL
        public CategoriesPlatsManager(IConfiguration configuration)
        {
            CategoriesPlatsDb = new CategoriesPlatsDB(configuration);
        }

        //liste des méthodes utilisateurs

        //les getters

        public List<CategoriesPlats> GetCategoriesPlats()
        {
            return CategoriesPlatsDb.GetCategoriesPlats();
        }
    }
}
