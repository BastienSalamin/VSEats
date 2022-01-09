using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class LivreursController : Controller
    {
        private ILivreursManager LivreursManager { get; }
        private ICommandesManager CommandesManager { get; }
        private ICommandesPlatsManager CommandesPlatsManager { get; }
        private IPlatsManager PlatsManager { get; }
        private IRestaurantsManager RestaurantsManager { get; }
        private ILocalitesManager LocalitesManager { get; }
        private IUtilisateursManager UtilisateursManager { get; }

        public LivreursController(ILivreursManager livreursManager, ICommandesManager commandesManager, IUtilisateursManager utilisateursManager, ICommandesPlatsManager commandesPlatsManager, IPlatsManager platsManager, IRestaurantsManager restaurantsManager, ILocalitesManager localitesManager)
        {
            LivreursManager = livreursManager;
            CommandesManager = commandesManager;
            CommandesPlatsManager = commandesPlatsManager;
            PlatsManager = platsManager;
            RestaurantsManager = restaurantsManager;
            LocalitesManager = localitesManager;
            UtilisateursManager = utilisateursManager;
        }

        public IActionResult Index()
        {
            var id = HttpContext.Request.Cookies["IdLivreur"];

            if (id != null)
            {
                var idLivreur = Int32.Parse(id);

                var commandes = CommandesManager.GetCommandesLocales(idLivreur);

                if(commandes != null)
                {
                    return View(commandes);
                }
                else
                {
                    return RedirectToAction("UnlogLivreur", "Login");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Take(int idCommande)
        {
            var id = HttpContext.Request.Cookies["IdLivreur"];

            if (id != null)
            {
                var idLivreur = Int32.Parse(id);

                var commande = CommandesManager.GetCommande(idCommande);

                var livreur1 = LivreursManager.GetLivreurs(idLivreur);

                if(livreur1.Disponible == false)
                {
                    return RedirectToAction("Index");
                }

                if (commande.IdLivreur == 1)
                {
                    CommandesManager.UpdateCommandeLivreur(idLivreur, idCommande);

                    LivreursManager.AddCommande(idLivreur);

                    var livreur2 = LivreursManager.GetLivreurs(idLivreur);

                    if (livreur2.NbCommande == 5)
                    {
                        LivreursManager.UpdateDisponibilite(idLivreur, false);
                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Details(int idCommande)
        {
            var id = HttpContext.Request.Cookies["IdLivreur"];

            if (id != null)
            {
                // Ajout des éléments concernant strictement la commande (sans infos utilisateur)
                CommandeVM commande = new CommandeVM();
                List<ItemVM> listItemVm = new List<ItemVM>();

                var commandeDb = CommandesManager.GetCommande(idCommande);

                commande.IdCommande = commandeDb.IdCommande;
                commande.IdUtilisateur = commandeDb.IdUtilisateur;
                commande.IdLivreur = commandeDb.IdLivreur;
                commande.CommandeLivree = commandeDb.CommandeLivree;
                commande.PrixTotal = commandeDb.PrixTotal;
                commande.HeureLivraison = commandeDb.Date;
                commande.TempsLivraison = commandeDb.TempsLivraison;

                // Ajout des éléments concernant les plats commandés
                var commandePlats = CommandesPlatsManager.GetCommandesPlats();

                foreach (var commandePlat in commandePlats)
                {
                    if (commandePlat.IdCommande == commande.IdCommande)
                    {
                        var plats = PlatsManager.GetPlats();
                        foreach (var plat in plats)
                        {
                            if (plat.IdPlat == commandePlat.IdPlat)
                            {
                                ItemVM itemVM = new ItemVM();

                                itemVM.IdPlat = plat.IdPlat;
                                itemVM.IdRestaurant = plat.IdRestaurant;
                                itemVM.Nom = plat.Nom;
                                itemVM.Prix = plat.Prix;
                                itemVM.Description = plat.Description;
                                itemVM.Quantite = commandePlat.Quantite;

                                listItemVm.Add(itemVM);

                            }
                        }
                    }
                }

                commande.ListPlats = listItemVm;

                // Ajout des informations concernant les restaurants
                var restaurants = RestaurantsManager.GetRestaurants();

                foreach (var item in commande.ListPlats)
                {
                    foreach (var restaurant in restaurants)
                    {
                        if (item.IdRestaurant == restaurant.IdRestaurant)
                        {
                            item.IdLocalite = restaurant.IdLocalite;
                            item.NomRestaurant = restaurant.Nom;
                            item.Adresse = restaurant.Adresse;
                        }
                    }
                }

                // Ajout des informations concernant la localité du restaurant
                var localites = LocalitesManager.GetLocalites();

                foreach (var item in commande.ListPlats)
                {
                    foreach (var localite in localites)
                    {
                        if (item.IdLocalite == localite.IdLocalite)
                        {
                            item.NPA = localite.NPA;
                            item.Ville = localite.Ville;
                        }
                    }
                }

                // Ajout des informations concernant l'utilisateur
                var user = UtilisateursManager.GetUserId(commande.IdUtilisateur);

                commande.Nom = user.Nom;
                commande.Prenom = user.Prenom;
                commande.Adresse = user.Adresse;
                commande.NumTelephone = user.NumTelephone;

                return View(commande);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Deliver(int idCommande)
        {
            var id = HttpContext.Request.Cookies["IdLivreur"];

            if (id != null)
            {
                var idLivreur = Int32.Parse(id);

                var commande = CommandesManager.GetCommande(idCommande);

                if (commande.IdLivreur == idLivreur && idLivreur != 1)
                {
                    CommandesManager.UpdateDelivery(idCommande);

                    LivreursManager.RemoveCommande(idLivreur);

                    var livreur = LivreursManager.GetLivreurs(idLivreur);

                    if (livreur.NbCommande < 5)
                    {
                        LivreursManager.UpdateDisponibilite(idLivreur, true);
                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
