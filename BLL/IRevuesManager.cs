using DTO;
using System.Collections.Generic;

namespace BLL
{
    public interface IRevuesManager
    {
        void AddRevue(int idUtilisateur, int idRestaurant, int etoiles, string commentaire);
        List<Revues> GetRevues();
    }
}