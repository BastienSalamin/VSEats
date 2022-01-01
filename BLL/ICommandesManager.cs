using DTO;
using System;
using System.Collections.Generic;

namespace BLL
{
    public interface ICommandesManager
    {
        List<Commandes> GetCommandes();
        List<Commandes> GetCommandes(int idUser);
        int GetIdCommande(int idUtilisateur, double prixTotal, DateTime date);
        void Order(int idUtilisateur, int idLivreur, double prixTotal, DateTime date);
        void updateDelivery(int idCommande);
    }
}