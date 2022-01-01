using DTO;
using System.Collections.Generic;

namespace DAL
{
    public interface ICommandesPlatsDB
    {
        int AddQuantite(int idCommande, int idPlat, int quantite);
        List<CommandesPlats> GetCommandesPlats();
        int UpdateQuantite(int idCommande, int idPlat);
    }
}