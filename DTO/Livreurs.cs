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
        public string NumTelephone{get;set;}
        public Boolean Disponible { get; set; }

        public override string ToString()
        {
            return "IdLivreur: " + IdLivreur +
                "IdLocalite: " + IdLocalite +
                "Nom: " + Nom +
                "Prenom: " + Prenom +
                "NumTelephone: " + NumTelephone +
                "Disponible: " + Disponible;
                
        }

    }
}
