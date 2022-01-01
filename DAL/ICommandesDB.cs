﻿using DTO;
using System;
using System.Collections.Generic;

namespace DAL
{
    public interface ICommandesDB
    {
        int AddCommande(int idUtilisateur, int idLivreur, double prixTotal, int tempsLivraison, DateTime date);
        List<Commandes> GetCommandes();
        List<Commandes> GetCommandes(int idUser);
        int GetIdCommande(int idUtilisateur, double prixTotal, DateTime date);
        int UpdateCommandeLivree(int idCommande);
    }
}