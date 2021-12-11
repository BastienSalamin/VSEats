using System.Collections.Generic;
using DTO;

namespace DAL
{
    public interface ILocalitesDB
    {
        int GetLocalite(int npa);
        List<Localites> GetLocalites();
    }
}