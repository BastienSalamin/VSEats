using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CategoriesPlats
    {
        public int IdPlat { get; set; }
        public int IdCategorie { get; set; }

        public override string ToString()
        {
            return "IdPlat: " + IdPlat +
                " IdCategorie: " + IdCategorie;
                 
        }

    }
}
