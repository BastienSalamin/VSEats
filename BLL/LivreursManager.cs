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
        // Création des références privées
        private ILivreursDB LivreursDb { get; }

        // Création du constructeur pour instancier la DAL
        public LivreursManager(ILivreursDB livreursDb)
        {
            LivreursDb = livreursDb;
        }

        // Liste des méthodes utilisateurs
        public void UpdateDisponibilite(int livreur, bool disponible)
        {
            LivreursDb.UpdateDisponibilite(livreur, disponible);
        }

        public int AddCommande(int idLivreur)
        {
            var livreurs = LivreursDb.GetLivreurs();

            int nbCommandesLivreur = 5;

            foreach (var livreur in livreurs)
            {
                if (livreur.IdLivreur == idLivreur)
                {
                    nbCommandesLivreur = livreur.NbCommande;
                }
            }

            if (nbCommandesLivreur == 5)
            {
                return -1;
            }
            else
            {
                return LivreursDb.AddCommande(idLivreur);
            }
        }

        public int RemoveCommande(int idLivreur)
        {
            var livreurs = LivreursDb.GetLivreurs();

            int nbCommandesLivreur = 0;

            foreach (var livreur in livreurs)
            {
                if (livreur.IdLivreur == idLivreur)
                {
                    nbCommandesLivreur = livreur.NbCommande;
                }
            }

            if (nbCommandesLivreur == 0)
            {
                return -1;
            }
            else
            {
                return LivreursDb.RemoveCommande(idLivreur);
            }
        }

        // Les Getters
        public List<Livreurs> GetLivreurs()
        {
            return LivreursDb.GetLivreurs();
        }

        public Livreurs GetLivreurs(string login, string motDePasse)
        {
            return LivreursDb.GetLivreurs(login, motDePasse);
        }

        public Livreurs GetLivreurs(int idLivreur)
        {
            return LivreursDb.GetLivreurs(idLivreur);
        }
    }
}
