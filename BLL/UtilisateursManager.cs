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
    public class UtilisateursManager
    {
        // Création de références privées
        private IUtilisateursDB UtilisateursDb { get; }
        private ILocalitesDB LocalitesDb { get; }


        // Création du constructeur pour instancier la DAL
        public UtilisateursManager(IConfiguration configuration)
        {
            UtilisateursDb = new UtilisateursDB(configuration);
            LocalitesDb = new LocalitesDB(configuration);
        }


        //liste des méthodes utilisateurs


        public void Subscribe(int npa, string nom, string prenom, string login, string motDePasse, string adresse, string numTelephone)
        {

            var idLocalite = LocalitesDb.GetLocalite(npa);

            UtilisateursDb.AddUtilisateur( idLocalite,  nom,  prenom,  login,  motDePasse,  adresse,  numTelephone);
        }

        public Boolean CanConnect(string email, string motDePasse)
        {

            Boolean canConnect;

            var utilisateur = UtilisateursDb.GetUtilisateurs(email, motDePasse);

            if (utilisateur.Login.Contains(email))
            {

                if(utilisateur.MotDePasse.Contains(motDePasse))
                {
                    return canConnect = true;
                }
                else
                {
                    return canConnect = false;
                }

            }
            else
            {
                return canConnect = false;
            }
        }

        //les getters
        public List<Utilisateurs> GetUtilisateurs()
        {
            return UtilisateursDb.GetUtilisateurs();
        }

        public Utilisateurs GetUtilisateurs(string email, string motDePasse)
        {

            return UtilisateursDb.GetUtilisateurs(email, motDePasse);
        }

        public Utilisateurs GetUserId(int idUtilisateur)
        {
            

            return UtilisateursDb.GetUtilisateurs(idUtilisateur);
        }
    }
}
