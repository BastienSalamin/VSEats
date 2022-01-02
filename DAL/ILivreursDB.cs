using DTO;
using System.Collections.Generic;

namespace DAL
{
    public interface ILivreursDB
    {
        List<Livreurs> GetLivreurs();
        Livreurs GetLivreurs(string login, string motDePasse);
        int UpdateDisponibilite(int idLivreur, bool disponible);
    }
}