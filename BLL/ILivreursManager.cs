using DTO;
using System.Collections.Generic;

namespace BLL
{
    public interface ILivreursManager
    {
        List<Livreurs> GetLivreurs();
        void UpdateDisponibilite(int livreur, bool disponible);
    }
}