using DTO;
using System.Collections.Generic;

namespace DAL
{
    public interface IRevuesDB
    {
        int AddRevue(int idUtilisateur, int idRestaurant, int etoiles, string commentaire);
        List<Revues> GetRevues();
    }
}