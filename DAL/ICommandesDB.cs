using DTO;
using System;
using System.Collections.Generic;

namespace DAL
{
    public interface ICommandesDB
    {
        int AddCommande(int idUtilisateur, int idLivreur, float prixTotal, int tempsLivraison, DateTime date);
        List<Commandes> GetCommandes();
        int UpdateCommandeLivree(int idCommande);
    }
}