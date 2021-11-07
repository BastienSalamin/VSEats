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
    class LocalitesManager
    {
        // Création de références privées
        private ILocalitesDB LocalitesDb { get; }

        // Création du constructeur pour instancier la DAL
        public LocalitesManager(IConfiguration configuration)
        {

            LocalitesDb = new LocalitesDB(configuration);
        }

        //liste des méthodes utilisateurs

        //les getters

        public int GetLocalite(int npa)
        {
            return LocalitesDb.GetLocalite(npa);
        }

        public List<Localites> GetLocalites()
        {
            return LocalitesDb.GetLocalites();
        }

    }
}
