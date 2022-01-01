using DTO;
using System.Collections.Generic;

namespace DAL
{
    public interface IPlatsDB
    {
        string GetNomPlat(int idPlat);
        int GetPlatID(string nom, int idRestaurant);
        List<Plats> GetPlats();
        List<Plats> GetPlats(int idRestaurant);
        double GetPrixPlat(int idPlat);
    }
}