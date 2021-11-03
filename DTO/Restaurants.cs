using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Restaurants
    {
        public int IdRestaurant { get; set; }
        public int IdLocalite { get; set; }
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public DateTime DateOuverture{get;set;}
    }
}
