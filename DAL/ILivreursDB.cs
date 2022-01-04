using DTO;
using System.Collections.Generic;

namespace DAL
{
    public interface ILivreursDB
    {
        int AddCommande(int idLivreur);
        List<Livreurs> GetLivreurs();
        Livreurs GetLivreurs(int idLivreur);
        Livreurs GetLivreurs(string login, string motDePasse);
        int RemoveCommande(int idLivreur);
        int UpdateDisponibilite(int idLivreur, bool disponible);
    }
}