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
    public class CommandesPlatsManager
    {
        // Création de références privées
        private ICommandesPlatsDB CommandesPlatsDb { get; }

        // Création du constructeur pour instancier la DAL
        public CommandesPlatsManager(IConfiguration configuration)
        {
            CommandesPlatsDb = new CommandesPlatsDB(configuration);
            
        }

        //liste des méthodes utilisateurs

        //les getters
        public List<CommandesPlats> GetCommandesPlats()
        {
            return CommandesPlatsDb.GetCommandesPlats();
        }
    }
}
