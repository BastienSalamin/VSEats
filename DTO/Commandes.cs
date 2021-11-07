using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Commandes
    {
        public int IdCommande { get; set; }
        public int IdUtilisateur { get; set; }
        public int IdLivreur { get; set; }
        public bool CommandeLivree { get; set; }
        public double PrixTotal { get; set; }
        public DateTime Date { get; set; }
        public int TempsLivraison { get; set; } /*temps d'attente en min*/

        public override string ToString()
        {
            return "IdCommande: " + IdCommande +
                " IdUtilisateur: " + IdUtilisateur +
                " IdLivreur: " + IdLivreur +
                " Commande Livree ? : " + CommandeLivree +
                " Prix Total: " + PrixTotal +
                " Date: " + Date +
                " Temps de livraison : " + TempsLivraison + " minutes";
        }

    }
}
