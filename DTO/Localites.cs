using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Localites
    {
        public int IdLocalite { get; set; }
        public int NPA { get; set; }
        public string Ville { get; set; }

        public override string ToString()
        {
            return "IdLocalite: " + IdLocalite +
                "NPA: " + NPA +
                "Ville: " + Ville;
        }

    }
}
