using DTO;
using System.Collections.Generic;

namespace BLL
{
    public interface IRestaurantsManager
    {
        List<Restaurants> GetRestaurants();
    }
}