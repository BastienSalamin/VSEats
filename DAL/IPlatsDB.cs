using DTO;
using System.Collections.Generic;

namespace DAL
{
    public interface IPlatsDB
    {
        int GetPlatID(string nom, int idRestaurant);
        List<Plats> GetPlats();
        float GetPrixPlat(int idPlat);
    }
}