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
    public class UtilisateursManager
    {
        // Création de deux références privées
        private IUtilisateursDB utilisateursDb { get; }


        // Création du constructeur pour instancier la DAL
        public UtilisateursManager(IConfiguration configuration)
        {
            utilisateursDb = new UtilisateursDB(configuration);
            
        }


        //liste des méthodes utilisateurs

        public List<Utilisateurs> GetUtilisateurs()
        {
            return utilisateursDb.GetUtilisateurs();
        }
    }
}
