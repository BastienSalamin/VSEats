using DTO;
using System.Collections.Generic;

namespace BLL
{
    public interface IUtilisateursManager
    {
        bool CanConnect(string email, string motDePasse);
        Utilisateurs GetUserId(int idUtilisateur);
        List<Utilisateurs> GetUtilisateurs();
        Utilisateurs GetUtilisateurs(string email, string motDePasse);
        void Subscribe(int npa, string nom, string prenom, string login, string motDePasse, string adresse, string numTelephone);
        int Update(int npa, string nom, string prenom, string login, string motDePasse, string adresse, string numTelephone);
    }
}