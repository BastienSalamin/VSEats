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
                    // Création du cookie utilisateur
                    var user = UtilisateursManager.GetUtilisateurs(loginWm.Email, loginWm.MotDePasse);
                    HttpContext.Response.Cookies.Append("IdUtilisateur", user.IdUtilisateur.ToString());

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Oups ! L'e-mail ou le mot de passe est faux !");
                }

            }

            return View(loginWm);
        }

        public IActionResult Unlog()
        {
            HttpContext.Response.Cookies.Delete("IdUtilisateur");

            return RedirectToAction("Index", "Home");
        }
    }
}
