using DTO;
using System.Collections.Generic;

namespace BLL
{
    public interface ICategoriesManager
    {
        List<Categories> GetCategories();
    }
}