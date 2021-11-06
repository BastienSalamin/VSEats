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
        private ILocalitesDB localitesDb { get; }


        // Création du constructeur pour instancier la DAL
        public UtilisateursManager(IConfiguration configuration)
        {
            utilisateursDb = new UtilisateursDB(configuration);
            
        }


        //liste des méthodes utilisateurs


        public int subscribe(int npa, string nom, string prenom, string login, string motDePasse, string adresse, string numTelephone)
        {

            var idLocalite = localitesDb.GetLocalite(npa);

            return utilisateursDb.AddUtilisateur( idLocalite,  nom,  prenom,  login,  motDePasse,  adresse,  numTelephone);
        }

        public Boolean canConnect(string email, string motDePasse)
        {

            Boolean canConnect;

            var utilisateur = utilisateursDb.GetUtilisateurs(email, motDePasse);

            if (utilisateur.Login == email)
            {

                if(utilisateur.MotDePasse == motDePasse)
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
            return utilisateursDb.GetUtilisateurs();
        }

        public Utilisateurs GetUtilisateurs(string email, string motDePasse)
        {

            return utilisateursDb.GetUtilisateurs(email, motDePasse);
        }

        public Utilisateurs GetUserId(int idUtilisateur)
        {
            

            return utilisateursDb.GetUtilisateurs(idUtilisateur);
        }
    }
}
