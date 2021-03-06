using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Livreurs
    {
        public int IdLivreur { get; set; }
        public int IdLocalite { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Login { get; set; } /*Correspond à l'e-mail*/
        public string MotDePasse { get; set; }
        public string NumTelephone{get;set;}
        public int NbCommande { get; set; }
        public Boolean Disponible { get; set; }

        public override string ToString()
        {
            return "IdLivreur: " + IdLivreur +
                " IdLocalite: " + IdLocalite +
                " Nom: " + Nom +
                " Prenom: " + Prenom +
                " NumTelephone: " + NumTelephone +
                " Nombre de commandes : " + NbCommande +
                " Disponible: " + Disponible;
                
        }

    }
}
