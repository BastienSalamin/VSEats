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
    public class CommandesPlatsManager : ICommandesPlatsManager
    {
        // Création de références privées
        private ICommandesPlatsDB CommandesPlatsDb { get; }

        // Création du constructeur pour instancier la DAL
        public CommandesPlatsManager(ICommandesPlatsDB commandesPlatsDb)
        {
            CommandesPlatsDb = commandesPlatsDb;

        }

        //liste des méthodes utilisateurs
        public int AddQuantite(int idCommande, int idPlat, int quantite)
        {
            return CommandesPlatsDb.AddQuantite(idCommande, idPlat, quantite);
        }

        //les getters
        public List<CommandesPlats> GetCommandesPlats()
        {
            return CommandesPlatsDb.GetCommandesPlats();
        }
    }
}
