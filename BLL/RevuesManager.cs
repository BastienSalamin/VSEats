﻿using System;
using DTO;
using DAL;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class RevuesManager : IRevuesManager
    {
        // Création de références privées
        private IRevuesDB RevuesDb { get; }

        // Création du constructeur pour instancier la DAL
        public RevuesManager(IRevuesDB revuesDb)
        {
            RevuesDb = revuesDb;
        }

        //liste des méthodes
        public void AddRevue(int idUtilisateur, int idRestaurant, int etoiles, string commentaire)
        {
            RevuesDb.AddRevue(idUtilisateur, idRestaurant, etoiles, commentaire);
        }

        //les getters
        public List<Revues> GetRevues()
        {
            return RevuesDb.GetRevues();
        }
    }
}