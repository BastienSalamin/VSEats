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
        private ILivreursManager LivreursManager { get; }
        private ILocalitesManager LocalitesManager { get; }
        private IPlatsManager PlatsManager { get; }

        public LoginController(IUtilisateursManager utilisateursManager, ILivreursManager livreursManager, ILocalitesManager localitesManager, IPlatsManager platsManager)
        {
            PlatsManager = platsManager;
            UtilisateursManager = utilisateursManager;
            LivreursManager = livreursManager;
            LocalitesManager = localitesManager;
        }

        public IActionResult Index()
        {
            // Empêcher qu'un livreur connecté puisse se connecter en tant qu'utilisateur
            var id = HttpContext.Request.Cookies["IdLivreur"];

            if (id != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
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
            // Supprimer le cookie correspondant à l'utilisateur connecté
            HttpContext.Response.Cookies.Delete("IdUtilisateur");

            // Supprimer les cookies correspondants aux plats ajoutés au panier pour vider ce dernier
            var plats = PlatsManager.GetPlats();

            foreach(var plat in plats)
            {
                var id = plat.IdPlat.ToString();
                var idc = HttpContext.Request.Cookies["IdPlat" + id];

                if(idc != null)
                {
                    HttpContext.Response.Cookies.Delete("IdPlat" + id);
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult LivreurIndex()
        {
            // Si le livreur est déjà connecté, le rediriger directement à la page d'index des livreurs
            var id = HttpContext.Request.Cookies["IdLivreur"];

            if(id != null)
            {
                return RedirectToAction("Index", "Livreurs");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LivreurIndex(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var livreur = LivreursManager.GetLivreurs(loginVM.Email, loginVM.MotDePasse);

                if(livreur != null)
                {
                    HttpContext.Response.Cookies.Append("IdLivreur", livreur.IdLivreur.ToString());
                    HttpContext.Response.Cookies.Append("NomLivreur", livreur.Nom);
                    HttpContext.Response.Cookies.Append("PrenomLivreur", livreur.Prenom);

                    var localites = LocalitesManager.GetLocalites();
                    int npa = 0;
                    string ville = "";

                    foreach (var localite in localites)
                    {
                        if (localite.IdLocalite == livreur.IdLocalite)
                        {
                            npa = localite.NPA;
                            ville = localite.Ville;
                        }
                    }

                    HttpContext.Response.Cookies.Append("LocaliteNPA", npa.ToString());
                    HttpContext.Response.Cookies.Append("LocaliteVille", ville);

                    return RedirectToAction("Index", "Livreurs");
                }
                else
                {
                    ModelState.AddModelError("", "Oups ! L'e-mail ou le mot de passe est faux !");
                }
            }
            return View(loginVM);
        }

        public IActionResult UnlogLivreur()
        {
            HttpContext.Response.Cookies.Delete("IdLivreur");
            HttpContext.Response.Cookies.Delete("NomLivreur");
            HttpContext.Response.Cookies.Delete("PrenomLivreur");
            HttpContext.Response.Cookies.Delete("LocaliteNPA");
            HttpContext.Response.Cookies.Delete("LocaliteVille");

            return RedirectToAction("Index", "Home");
        }
    }
}
