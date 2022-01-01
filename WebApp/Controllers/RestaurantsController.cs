﻿using BLL;
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

            if(id != null)
            {
                int idUser = Int32.Parse(id);

                var util = UtilisateursManager.GetUserId(idUser);

                var idLocalite = util.IdLocalite;

                var restaurants = RestaurantsManager.GetRestaurants(idLocalite);

                if(restaurants != null)
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

                if(plats != null)
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
                /*var idCommande = HttpContext.Request.Cookies["IdCommande"];

                if (idCommande != null)
                {
                    return RedirectToAction("Index", "Restaurants");
                }
                else
                {
                    return RedirectToAction("Index", "Restaurants");
                }*/

                return View(debutCommande);
            }

            return View(debutCommande);
        }
    }
}
