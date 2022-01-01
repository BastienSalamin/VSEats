using DTO;
using System.Collections.Generic;

namespace DAL
{
    public interface ICommandesPlatsDB
    {
        int AddQuantite(int idCommande, int idPlat, int quantite);
        List<CommandesPlats> GetCommandesPlats();
    }
}