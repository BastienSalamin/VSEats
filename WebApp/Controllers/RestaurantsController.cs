using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using DTO;

namespace WebApp.Controllers
{
    public class RestaurantsController : Controller
    {
        private IRestaurantsManager RestaurantsManager { get; }
        private IUtilisateursManager UtilisateursManager { get; }
        private IPlatsManager PlatsManager { get; }
        private ICommandesManager CommandesManager { get; }
        private ICommandesPlatsManager CommandesPlatsManager { get; }

        public RestaurantsController(IRestaurantsManager restaurantsManager, IUtilisateursManager utilisateursManager, IPlatsManager platsManager, ICommandesManager commandesManager, ICommandesPlatsManager commandesPlatsManager)
        {
            RestaurantsManager = restaurantsManager;
            UtilisateursManager = utilisateursManager;
            PlatsManager = platsManager;
            CommandesManager = commandesManager;
            CommandesPlatsManager = commandesPlatsManager;
        }

        public IActionResult Index()
        {
            var id = HttpContext.Request.Cookies["IdUtilisateur"];

            if (id != null)
            {
                int idUser = Int32.Parse(id);

                var util = UtilisateursManager.GetUserId(idUser);

                var idLocalite = util.IdLocalite;

                var restaurants = RestaurantsManager.GetRestaurants(idLocalite);

                if (restaurants != null)
                {
                    return View(restaurants);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public IActionResult Details(int id)
        {
            var idUser = HttpContext.Request.Cookies["IdUtilisateur"];

            if (idUser != null)
            {
                var plats = PlatsManager.GetPlats(id);

                if (plats != null)
                {
                    return View(plats);
                }
                else
                {
                    return RedirectToAction("Index", "Restaurants");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public IActionResult Commander(int id)
        {
            var idUser = HttpContext.Request.Cookies["IdUtilisateur"];

            if (idUser != null)
            {
                var plats = PlatsManager.GetPlats();
                int idRestaurant = 0;

                foreach (var plat in plats)
                {
                    if (plat.IdPlat == id)
                    {
                        idRestaurant = plat.IdRestaurant;
                    }
                }

                /*Ajout du plat dans le panier*/
                HttpContext.Response.Cookies.Append("IdPlat" + id.ToString(), "1");


                return RedirectToAction("Details", new { id = idRestaurant });
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public IActionResult Terminer()
        {
            return RedirectToAction("Index", "Commandes");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Commander(CommandeVM debutCommande)
        {
            if (ModelState.IsValid)
            {
                var idCommande = HttpContext.Request.Cookies["IdCommande"];

                if (idCommande != null)
                {
                    return RedirectToAction("Index", "Commandes");
                }
                else
                {
                    var prixTotal = debutCommande.Prix * debutCommande.Quantite;
                    CommandesManager.Order(debutCommande.IdUtilisateur, 1, prixTotal, debutCommande.HeureLivraison); // Comment faire pour le livreur ?
                    var idNewCommande = CommandesManager.GetIdCommande(debutCommande.IdUtilisateur, prixTotal, debutCommande.HeureLivraison);
                    CommandesPlatsManager.AddQuantite(idNewCommande, debutCommande.IdPlat, debutCommande.Quantite);
                    HttpContext.Response.Cookies.Append("IdCommande", idNewCommande.ToString());

                    return RedirectToAction("Index", "Commandes");
                }
            }

            return View(debutCommande);
        }
    }
}