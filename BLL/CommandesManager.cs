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
    public class CommandesManager : ICommandesManager
    {
        // Création de deux références privées
        private ICommandesDB CommandesDb { get; }
        private IUtilisateursDB UtilisateursDb { get; }
        private ILivreursDB LivreursDb { get; }
        private IPlatsDB PlatsDb { get; }
        private ICommandesPlatsDB CommandesPlatsDb { get; }


        // Création du constructeur pour instancier la DAL
        public CommandesManager(ICommandesDB commandesDb, IUtilisateursDB utilisateursDb, ILivreursDB livreursDb, IPlatsDB platsDb, ICommandesPlatsDB commandesPlatsDb)
        {
            CommandesDb = commandesDb;
            UtilisateursDb = utilisateursDb;
            LivreursDb = livreursDb;
            PlatsDb = platsDb;
            CommandesPlatsDb = commandesPlatsDb;
        }

        //liste des méthodes utilisateurs


        //Add

        //Plusieurs var demandées, faut-il les calculer ici?
        //dont: le prixTotal à calculer,
        //
        //Remarque: l'utilisateur choisi la DateTime à laquelle il veut se faire livrer
        public void Order(int idUtilisateur, int idLivreur, double prixTotal, DateTime date)
        {
            TimeSpan t = date - DateTime.Now;

            int tempsLivraison = (int)t.TotalMinutes;

            CommandesDb.AddCommande(idUtilisateur, idLivreur, prixTotal, tempsLivraison, date);
        }

        //update
        public void updateDelivery(int idCommande)
        {

            CommandesDb.UpdateCommandeLivree(idCommande);
        }


        //les getters
        public List<Commandes> GetCommandes()
        {
            return CommandesDb.GetCommandes();
        }

        public List<Commandes> GetCommandes(int idUser)
        {
            return CommandesDb.GetCommandes(idUser);
        }

        public int GetIdCommande(int idUtilisateur, double prixTotal, DateTime date)
        {
            return CommandesDb.GetIdCommande(idUtilisateur, prixTotal, date);
        }

    }
}
