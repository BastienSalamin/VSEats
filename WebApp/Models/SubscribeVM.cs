using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class SubscribeVM
    {
        [Required]
        [Display(Name = "NPA de votre ville")]
        public int Npa { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Login { get; set; }
        [Required]
        [Display(Name = "Mot de passe")]
        public string MotDePasse { get; set; }
        [Required]
        public string Adresse { get; set; }
        [Required]
        [StringLength(10)]
        [Display(Name = "Numéro de téléphone")]
        public string NumTelephone { get; set; }
    }
}
