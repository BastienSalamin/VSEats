using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class PanierVM
    {

        public IList<ItemVM> Items { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm}")]
        [DataType(DataType.Time)]
        [Display(Name = "Heure de livraison")]
        public DateTime HeureLivraison { get; set; }

    }

}
