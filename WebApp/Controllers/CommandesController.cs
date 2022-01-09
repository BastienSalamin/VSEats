using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class CommandesController : Controller
    {
        private ICommandesPlatsManager CommandesPlatsManager { get; }
        private ICommandesManager CommandesManager { get; }
        private IPlatsManager PlatsManager { get; }
        private IRestaurantsManager RestaurantsManager { get; }
        private ILocalitesManager LocalitesManager { get; }
        private IUtilisateursManager UtilisateursManager { get; }
        private ILivreursManager LivreursManager { get; }

        public CommandesController(ICommandesManager commandesManager, IPlatsManager platsManager, ICommandesPlatsManager commandesPlatsManager, IRestaurantsManager restaurantsManager, ILocalitesManager localitesManager, ILivreursManager livreursManager, IUtilisateursManager utilisateursManager)
        {
            LocalitesManager = localitesManager;
            RestaurantsManager = restaurantsManager;
            CommandesPlatsManager = commandesPlatsManager;
            CommandesManager = commandesManager;
            PlatsManager = platsManager;
            LivreursManager = livreursManager;
            UtilisateursManager = utilisateursManager;
        }

        public IActionResult Index()
        {
            var idUser = HttpContext.Request.Cookies["IdUtilisateur"];

            if (idUser != null)
            {
                List<ItemVM> items = new List<ItemVM>();

                var plats = PlatsManager.GetPlats();

                foreach (var plat in plats)
                {
                    var id = plat.IdPlat.ToString();
                    var idc = HttpContext.Request.Cookies["IdPlat" + id];

                    if (idc != null)
                    {
                        ItemVM element = new ItemVM();
                        element.IdPlat = plat.IdPlat;
                        element.Nom = plat.Nom;
                        element.Prix = plat.Prix;
                        element.Quantite = Int32.Parse(idc);
                        items.Add(element);
                    }
                }

                PanierVM panier = new PanierVM()
                {
                    Items = items,
                    HeureLivraison = DateTime.Now
                };

                return View(panier);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
                
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(PanierVM panier)
        {
            if (ModelState.IsValid)
            {
                var idUtilisateur = Int32.Parse(HttpContext.Request.Cookies["IdUtilisateur"]);

                // Bloquer le passage de la commande si les minutes sont négatives (== l'heure de livraison est inférieure à maintenant)
                var dateTime = panier.HeureLivraison;
                TimeSpan t = panier.HeureLivraison - DateTime.Now;

                int tempsLivraison = (int)t.TotalMinutes;

                if (tempsLivraison < 0)
                {
                    return View(panier);
                }

                // Contrôle du livreur
                var idLivreur = 0;

                var utilisateur = UtilisateursManager.GetUserId(idUtilisateur);

                var livreurs = LivreursManager.GetLivreurs();
                
                foreach(var livreur in livreurs)
                {
                    if(livreur.IdLocalite == utilisateur.IdLocalite)
                    {
                        if(livreur.Disponible == true)
                        {
                            if (livreur.NbCommande == 4)
                            {
                                LivreursManager.UpdateDisponibilite(idLivreur, false);
                            }

                            LivreursManager.AddCommande(livreur.IdLivreur);

                            idLivreur = livreur.IdLivreur;
                            break;
                        }
                    }
                }

                // Attribution d'un livreur par défaut si l'idLivreur est resté à 0
                if (idLivreur == 0)
                {
                    idLivreur = 1;
                }

                // Création de la commande dans la table dédiée
                double prixTotal = 0;

                foreach (var item in panier.Items)
                {
                    var prix = item.Prix;
                    var quantite = item.Quantite;
                    prixTotal = prixTotal +(prix * quantite);
                }

                CommandesManager.Order(idUtilisateur, idLivreur, prixTotal, dateTime);

                // Ajout de la quantité et de l'idplat dans la table CommandesPlats à l'aide de l'idCommande de la commande tout juste créée
                var idCommande = CommandesManager.GetIdCommande(idUtilisateur, prixTotal, dateTime);

                foreach(var item in panier.Items)
                {
                    var idPlat = item.IdPlat;
                    var quantite = item.Quantite;

                    CommandesPlatsManager.AddQuantite(idCommande, idPlat, quantite);
                }

                // Supprimer les cookies concernant les plats pour vider le panier
                var plats = PlatsManager.GetPlats();

                foreach (var plat in plats)
                {
                    var id = plat.IdPlat.ToString();
                    var idc = HttpContext.Request.Cookies["IdPlat" + id];

                    if (idc != null)
                    {
                        HttpContext.Response.Cookies.Delete("IdPlat" + id);
                    }
                }

                return RedirectToAction("Historique");
            }
            else
            {
                return View(panier);
            }
        }

        public IActionResult Historique()
        {
            var idUser = HttpContext.Request.Cookies["IdUtilisateur"];

            if (idUser != null)
            {
                var id = Int32.Parse(idUser);
                var commandes = CommandesManager.GetCommandes(id);
                if (commandes != null)
                {
                    return View(commandes);
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
                // Ajout des éléments concernant strictement la commande
                CommandeVM commande = new CommandeVM();
                List<ItemVM> listItemVm = new List<ItemVM>();

                var commandeDb = CommandesManager.GetCommande(id);

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
                    if(commandePlat.IdCommande == commande.IdCommande)
                    {
                        var plats = PlatsManager.GetPlats();
                        foreach(var plat in plats)
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
                    foreach(var restaurant in restaurants)
                    {
                        if(item.IdRestaurant == restaurant.IdRestaurant)
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

                return View(commande);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public IActionResult Delete(int id)
        {
            var idUser = HttpContext.Request.Cookies["IdUtilisateur"];

            if (idUser != null)
            {
                var commande = CommandesManager.GetCommande(id);

                // Contrôle pour que la date voulue de livraison soit supérieure à 3 heures
                TimeSpan t = commande.Date - DateTime.Now;

                int tempsLivraison = (int)t.TotalMinutes;

                if (tempsLivraison > 180)
                {
                    if(commande.IdLivreur != 1)
                    {
                        LivreursManager.RemoveCommande(commande.IdLivreur);

                        var livreur = LivreursManager.GetLivreurs(commande.IdLivreur);

                        if (livreur.NbCommande < 5)
                        {
                            LivreursManager.UpdateDisponibilite(commande.IdLivreur, true);
                        }
                    }

                    CommandesManager.DeleteCommande(id);

                    return RedirectToAction("Historique");
                }
                else
                {
                    return RedirectToAction("Details", new { id = id });
                }
                    
                
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
