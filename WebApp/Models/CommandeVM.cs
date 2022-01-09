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
        [Display(Name = "Commande livrée ?")]
        public bool CommandeLivree { get; set; }
        [Display(Name = "Prix total")]
        public double PrixTotal { get; set; }
        [Display(Name = "Date de livraison")]
        public DateTime HeureLivraison { get; set; }
        [Display(Name = "Temps estimé de la livraison depuis la création de la commande")]
        public int TempsLivraison { get; set; }
        public int IdUtilisateur { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Adresse { get; set; }
        [Display(Name = "Numéro de téléphone")]
        public string NumTelephone { get; set; }
        public IEnumerable<ItemVM> ListPlats { get; set; }

    }
}
