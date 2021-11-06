using System.Collections.Generic;

namespace DTO
{
    public interface ILocalitesDB
    {
        int GetLocalite(int npa);
        List<Localites> GetLocalites();
    }
}