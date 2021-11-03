using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Utilisateurs
    {
        public int IdUtilisateur { get; set; }
        public int IdLocalite { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Login { get; set; } /*correspond au mail*/
        public string MotDePasse { get; set; }
        public string Adresse { get; set; }
        public string NumTelephone { get; set; }

        public override string ToString()
        {
            return "IdUtilisateur: " + IdUtilisateur +
                "IdLocalite: " + IdLocalite +
                "Nom: " + Nom +
                "Prenom: " + Prenom +
                "Login: " + Login +
                "MotDePass: " + MotDePasse +
                "Adresse: " + Adresse +
                "NumTelephone: " + NumTelephone;
        }

    }
}
