using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Revues
    {
        public int IdRevue { get; set; }
        public int IdUtilisateur{get;set;}
        public int IdRestaurant { get; set; }
        public int Etoiles { get; set; }
        public string Commentaire { get; set; }
        public DateTime Date { get; set; }

        public override string ToString()
        {
            return "IdRevue: " + IdRevue +
                " IdUtilisateur: " + IdUtilisateur +
                " IdRestaurant: " + IdRestaurant +
                " Etoiles: " + Etoiles +
                " Commentaire: " + Commentaire +
                " Date: " + Date;
        }

    }
}
