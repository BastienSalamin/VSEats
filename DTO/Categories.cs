using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Categories
    {
        public int IdCategorie { get; set; }
        public string Type { get; set; }
        public string Marque { get; set; }

        public override string ToString()
        {
            return "IdCategorie: " + IdCategorie +
                " Type: " + Type +
                " Marque: " + Marque;
        }

    }
}
