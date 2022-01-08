using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class CommandeVM
    {

        public int IdCommande { get; set; }
        public int IdLivreur { get; set; }
        public bool CommandeLivree { get; set; }
        public double PrixTotal { get; set; }
        public int TempsLivraison { get; set; }
        public int IdUtilisateur { get; set; }
        public DateTime HeureLivraison { get; set; }

        public List<ItemVM> ListPlats { get; set; }

    }
}
