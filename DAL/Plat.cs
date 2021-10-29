using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Plat
    {
        public int IdPlat { get; set; }
        public string Nom { get; set; }
        public float Prix { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return "IdPlat: " + IdPlat +
                " Nom du plat: " + Nom +
                " Prix du plat: " + Prix +
                " Description: " + Description;
        }
    }
}
