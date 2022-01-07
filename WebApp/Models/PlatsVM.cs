using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class PlatsVM
    {
        public int IdPlat { get; set; }
        public int IdRestaurant { get; set; }
        public string Nom { get; set; }
        public double Prix { get; set; }
        public string Description { get; set; }
        public int Quantite { get; set; }
    }
}
