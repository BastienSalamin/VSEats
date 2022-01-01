using BLL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class LoginController : Controller
    {

        private IUtilisateursManager UtilisateursManager { get; }

        public LoginController(IUtilisateursManager utilisateursManager)
        {
            UtilisateursManager = utilisateursManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LoginVM loginWm)
        {
            if (ModelState.IsValid)
            {
                var connexion = UtilisateursManager.CanConnect(loginWm.Email,loginWm.MotDePasse);

                if (connexion == true)
                {
                    return RedirectToAction("Index", "Restaurants");
                }
                else
                {
                    ModelState.AddModelError("", "Woops! Email or Password is wrong!");
                }

            }

            return View(loginWm);
        }
    }
}
