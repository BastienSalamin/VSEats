﻿using DTO;
using System.Collections.Generic;

namespace DAL
{
    public interface ILivreursDB
    {
        List<Livreurs> GetLivreurs();
        int UpdateDisponibilite(int idLivreur, bool disponible);
    }
}