using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Plats
    {
        public int IdPlat { get; set; }
        public int IdRestaurant { get; set; }
        public string Nom { get; set; }
        public float Prix { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return "IdPlat: " + IdPlat +
                "IdRestaurant: " + IdRestaurant +
                " Nom du plat: " + Nom +
                " Prix du plat: " + Prix +
                " Description: " + Description;
        }
    }
}
