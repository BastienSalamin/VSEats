using DTO;
using System.Collections.Generic;

namespace BLL
{
    public interface ILivreursManager
    {
        List<Livreurs> GetLivreurs();
        Livreurs GetLivreurs(string login, string motDePasse);
        void UpdateDisponibilite(int livreur, bool disponible);
    }
}