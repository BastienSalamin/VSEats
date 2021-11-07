﻿using System;
using DAL;
using DTO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class CommandesManager
    {
        // Création de deux références privées
        private ICommandesDB CommandesDb { get; }
        private IUtilisateursDB UtilisateursDb { get; }
        private ILivreursDB LivreursDb { get; }
        private IPlatsDB PlatsDb { get; }
        private ICommandesPlatsDB CommandesPlatsDb{get;}


        // Création du constructeur pour instancier la DAL
        public CommandesManager(IConfiguration configuration)
        {
            CommandesDb = new CommandesDB(configuration);
            UtilisateursDb = new UtilisateursDB(configuration);
            LivreursDb = new LivreursDB(configuration);
            PlatsDb = new PlatsDB(configuration);
            CommandesPlatsDb = new CommandesPlatsDB(configuration);
        }

        //liste des méthodes utilisateurs


        //Add

        //Plusieurs var demandées, faut-il les calculer ici?
        //dont: le prixTotal à calculer,
        //
        //Remarque: l'utilisateur choisi la DateTime à laquelle il veut se faire livrer
        public void Order(int idUtilisateur, int idLivreur, float prixTotal, DateTime date)
        {
   
            bool commandeLivree = false;

            int tempsLivraison = date - DateTime.Now;

            CommandesDb.AddCommande(idUtilisateur, idLivreur, commandeLivree, prixTotal, date, tempsLivraison);
        }

        //update

        public void updateDelivery(int idCommande)
        {

            commandesDb.UpdateCommandeLivree(idCommande);
        }


        //les getters

        public List<Commandes> GetCommandes()
        {
            return commandesDb.GetCommandes();
        }

    }
}
