using DTO;
using System.Collections.Generic;

namespace DAL
{
    public interface IUtilisateursDB
    {
        int AddUtilisateur(int idLocalite, string nom, string prenom, string login, string motDePasse, string adresse, string numTelephone);
        int GetIdUtilisateurs(string login, string motDePasse);
        List<Utilisateurs> GetUtilisateurs();
        Utilisateurs GetUtilisateurs(int idUtilisateur);
        Utilisateurs GetUtilisateurs(string login, string motDePasse);
        int UpdateUtilisateur(int idUtilisateur, int idLocalite, string nom, string prenom, string login, string motDePasse, string adresse, string numTelephone);
    }
}