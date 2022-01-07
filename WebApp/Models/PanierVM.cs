using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class PanierVM
    {
        public int IdPlat { get; set; }
        public string Nom { get; set; }
        public double Prix { get; set; }
        [Required]
        [Display(Name = "Quantité")]
        public int Quantite { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm}")]
        [DataType(DataType.Time)]
        [Display(Name = "Heure de livraison")]
        public DateTime HeureLivraison { get; set; }
    }
}
