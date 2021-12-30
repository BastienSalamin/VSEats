﻿using DTO;
using System.Collections.Generic;

namespace BLL
{
    public interface IPlatsManager
    {
        int GetPlatID(string nom, int idRestaurant);
        List<Plats> GetPlats();
        List<Plats> GetPlats(int idRestaurant);
        double GetPrixPlat(int idPlat);
    }
}