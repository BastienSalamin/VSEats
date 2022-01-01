using DTO;
using System.Collections.Generic;

namespace BLL
{
    public interface ICommandesPlatsManager
    {
        int AddQuantite(int idCommande, int idPlat, int quantite);
        List<CommandesPlats> GetCommandesPlats();
    }
}