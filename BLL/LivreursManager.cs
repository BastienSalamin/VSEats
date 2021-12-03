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
    public class LivreursManager : ILivreursManager
    {
        // Création de références privées
        private ILivreursDB LivreursDb { get; }

        // Création du constructeur pour instancier la DAL
        public LivreursManager(ILivreursDB livreursDb)
        {
            LivreursDb = livreursDb;
        }

        //liste des méthodes utilisateurs

        public void UpdateDisponibilite(int livreur, bool disponible)
        {
            LivreursDb.UpdateDisponibilite(livreur, disponible);
        }

        //les getters
        public List<Livreurs> GetLivreurs()
        {
            return LivreursDb.GetLivreurs();
        }
    }
}
