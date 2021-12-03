using DTO;
using System;
using System.Collections.Generic;

namespace BLL
{
    public interface ICommandesManager
    {
        List<Commandes> GetCommandes();
        void Order(int idUtilisateur, int idLivreur, double prixTotal, DateTime date);
        void updateDelivery(int idCommande);
    }
}