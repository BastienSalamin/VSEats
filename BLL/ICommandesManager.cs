using DTO;
using System;
using System.Collections.Generic;

namespace BLL
{
    public interface ICommandesManager
    {
        int DeleteCommande(int idCommande);
        Commandes GetCommande(int idCommande);
        List<Commandes> GetCommandes();
        List<Commandes> GetCommandes(int idUser);
        List<Commandes> GetCommandesLocales(int idLivreur);
        int GetIdCommande(int idUtilisateur, double prixTotal, DateTime date);
        void Order(int idUtilisateur, int idLivreur, double prixTotal, DateTime date);
        int UpdateCommandeLivreur(int idLivreur, int idCommande);
        void UpdateDelivery(int idCommande);
    }
}