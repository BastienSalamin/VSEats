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
        private ICommandesPlatsManager CommandesPlatsManager { get; set; }
        private ICommandesManager CommandesManager { get; set; }
        private IPlatsManager PlatsManager { get; set; }
        private IRestaurantsManager RestaurantsManager { get; set; }
        private ILocalitesManager LocalitesManager { get; set; }

        public CommandesController(ICommandesManager commandesManager, IPlatsManager platsManager, ICommandesPlatsManager commandesPlatsManager, IRestaurantsManager restaurantsManager,ILocalitesManager localitesManager)
        {
            LocalitesManager = localitesManager;
            RestaurantsManager = restaurantsManager;
            CommandesPlatsManager = commandesPlatsManager;
            CommandesManager = commandesManager;
            PlatsManager = platsManager;
        }

        public IActionResult Index()
        {
            List<ItemVM> items= new List<ItemVM>();

            var plats = PlatsManager.GetPlats();

            foreach (var plat in plats)
            {
                var id = plat.IdPlat.ToString();
                var idc = HttpContext.Request.Cookies["IdPlat" + id];

                if (idc != null)
                {
                    ItemVM element = new ItemVM();
                    element.IdRestaurant = plat.IdRestaurant;
                    element.Description = plat.Description;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(PanierVM panier)
        {
            if (ModelState.IsValid)
            {

                var idUtilisateur = Int32.Parse(HttpContext.Request.Cookies["IdUtilisateur"]);
                var idLivreur = 1;
                var dateTime = panier.HeureLivraison;
                double prixTotal = 0;

                foreach (var item in panier.Items)
                {
  
                    var prix = item.Prix;
                    var quantite = item.Quantite;
                    prixTotal = prixTotal +( prix * quantite);

                }

                
                
                CommandesManager.Order(idUtilisateur, idLivreur, prixTotal, dateTime);

                var idCommande = CommandesManager.GetIdCommande(idUtilisateur, prixTotal, dateTime);

                foreach(var item in panier.Items){

                    var idPlat = item.IdPlat;
                    var quantite = item.Quantite;

                    CommandesPlatsManager.AddQuantite(idCommande, idPlat, quantite);

                }

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

        public IActionResult Detail(int id)
        {

            var idUser = HttpContext.Request.Cookies["IdUtilisateur"];

            if (idUser != null)
            {

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

                return View(commande);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
