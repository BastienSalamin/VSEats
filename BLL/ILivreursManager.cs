using DTO;
using System.Collections.Generic;

namespace BLL
{
    public interface ILivreursManager
    {
        int AddCommande(int idLivreur);
        List<Livreurs> GetLivreurs();
        Livreurs GetLivreurs(int idLivreur);
        Livreurs GetLivreurs(string login, string motDePasse);
        int RemoveCommande(int idLivreur);
        void UpdateDisponibilite(int livreur, bool disponible);
    }
}