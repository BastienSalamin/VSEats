using DTO;
using System.Collections.Generic;

namespace DAL
{
    public interface IPlatsDB
    {
        List<Plats> GetPlats();
    }
}