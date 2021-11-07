using DTO;
using System.Collections.Generic;

namespace DAL
{
    public interface IRestaurantsDB
    {
        List<Restaurants> GetRestaurants();
    }
}