using DTO;
using System.Collections.Generic;

namespace DAL
{
    public interface IUtilisateursDB
    {
        int AddUtilisateur(int idLocalite, string nom, string prenom, string login, string motDePasse, string adresse, string numTelephone);
        List<Utilisateurs> GetUtilisateurs();
        int UpdateUtilisateur(int idUtilisateur, int idLocalite, string nom, string prenom, string login, string motDePasse, string adresse, string numTelephone);
    }
}