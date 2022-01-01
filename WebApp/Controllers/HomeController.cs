using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IUtilisateursManager UtilisateursManager { get; }

        public HomeController(ILogger<HomeController> logger, IUtilisateursManager utilisateursManager)
        {
            _logger = logger;
            UtilisateursManager = utilisateursManager;
        }

        public IActionResult Index()
        {
            var id = HttpContext.Request.Cookies["IdUtilisateur"];

            if(id != null)
            {
                int idUser = Int32.Parse(id);

                var users = UtilisateursManager.GetUtilisateurs();

                foreach (var user in users)
                {
                    if (user.IdUtilisateur == idUser)
                    {
                        return View(user);
                    }
                }

                return View();
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
