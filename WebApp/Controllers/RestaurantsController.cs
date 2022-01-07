using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

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
                    List<PlatsVM> platsQuantite = new List<PlatsVM>();

                    foreach (var plat in plats)
                    {
                        PlatsVM platQuantite = new PlatsVM();
                        platQuantite.IdPlat = plat.IdPlat;
                        platQuantite.IdRestaurant = plat.IdRestaurant;
                        platQuantite.Nom = plat.Nom;
                        platQuantite.Prix = plat.Prix;
                        platQuantite.Description = plat.Description;
                        platQuantite.Quantite = 0;
                        platsQuantite.Add(platQuantite);
                    }

                    return View(platsQuantite);
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
                var idUtilisateur = Int32.Parse(idUser);
                var prixPlat = PlatsManager.GetPrixPlat(id);
                CommandeVM debutCommande = new CommandeVM();
                debutCommande.IdUtilisateur = idUtilisateur;
                debutCommande.IdPlat = id;
                debutCommande.Nom = PlatsManager.GetNomPlat(id);
                debutCommande.Prix = prixPlat;
                debutCommande.Quantite = 1;
                debutCommande.HeureLivraison = DateTime.Now;

                return View(debutCommande);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(List<PlatsVM> platsVMs)
        {
            if (ModelState.IsValid)
            { 
                foreach (var plat in platsVMs)
                {
                    if (plat.Quantite > 0)
                    {
                        HttpContext.Response.Cookies.Append("IdPlat", plat.IdPlat.ToString());
                        HttpContext.Response.Cookies.Append("Quantite", plat.Quantite.ToString());
                        
                    }
                    
                }
                return RedirectToAction("Commander", "Restaurants");
            }
            return View(platsVMs);
        }
    }
}