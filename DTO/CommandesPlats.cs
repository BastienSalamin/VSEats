using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CommandesPlats
    {
        public int IdCommande { get; set; }
        public int IdPlat { get; set; }
        public int Quantite { get; set; }

        public override string ToString()
        {
            return "IdCommande: " + IdCommande +
                "IdPlat: " + IdPlat +
                "Quantite: " + Quantite;
        }

    }
}
