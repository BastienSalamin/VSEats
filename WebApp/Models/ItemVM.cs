using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ItemVM
    {
        public int IdPlat { get; set; }
        public int IdRestaurant { get; set; }
        [Display(Name = "Nom du plat")]
        public string Nom { get; set; }
        public double Prix { get; set; }
        public string Description { get; set; }
        [Required]
        [Display(Name = "Quantité")]
        public int Quantite { get; set; }
        public int IdLocalite { get; set; }
        [Display(Name = "Nom du restaurant")]
        public string NomRestaurant { get; set; }
        public string Adresse { get; set; }
        public int NPA { get; set; }
        public string Ville { get; set; }

    }
}
