using DTO;
using System.Collections.Generic;

namespace DAL
{
    interface IRestaurantsDB
    {
        List<Restaurants> GetRestaurants();
    }
}