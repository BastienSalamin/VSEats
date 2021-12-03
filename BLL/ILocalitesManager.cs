using DTO;
using System.Collections.Generic;

namespace BLL
{
    public interface ILocalitesManager
    {
        int GetLocalite(int npa);
        List<Localites> GetLocalites();
    }
}