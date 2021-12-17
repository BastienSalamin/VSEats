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
        public int Npa { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
        [Required]
        [EmailAddress]
        public string Login { get; set; }
        [Required]
        public string MotDePasse { get; set; }
        [Required]
        public string Adresse { get; set; }
        [Required]
        [StringLength(10)]
        public string NumTelephone { get; set; }
    }
}
