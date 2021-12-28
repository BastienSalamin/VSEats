using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class RestaurantsController : Controller
    {
        private IRestaurantsManager RestaurantsManager { get; }

        public RestaurantsController(IRestaurantsManager restaurantsManager)
        {
            RestaurantsManager = restaurantsManager;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
