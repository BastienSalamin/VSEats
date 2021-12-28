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
    public class LocalitesManager : ILocalitesManager
    {
        // Création de références privées
        private ILocalitesDB LocalitesDb { get; }

        // Création du constructeur pour instancier la DAL
        public LocalitesManager(ILocalitesDB localitesDb)
        {

            LocalitesDb = localitesDb;
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
