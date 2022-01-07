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
        private ICommandesManager CommandesManager { get; }
        private IPlatsManager PlatsManager { get; set; }

        public CommandesController(ICommandesManager commandesManager, IPlatsManager platsManager)
        {
            CommandesManager = commandesManager;
            PlatsManager = platsManager;
        }

        public IActionResult Index()
        {
            List<PanierVM> panier = new List<PanierVM>();

            var plats = PlatsManager.GetPlats();

            foreach (var plat in plats)
            {
                var id = plat.IdPlat.ToString();
                var idc = HttpContext.Request.Cookies["IdPlat" + id];

                if (idc != null)
                {
                    PanierVM element = new PanierVM();
                    element.IdPlat = plat.IdPlat;
                    element.Nom = plat.Nom;
                    element.Prix = plat.Prix;
                    element.Quantite = 1;
                    element.HeureLivraison = DateTime.Now;
                    panier.Add(element);

                }
            }

            return View(panier);
        }

        public IActionResult Details(int id)
        {
            var idUser = HttpContext.Request.Cookies["IdUtilisateur"];

            if (idUser != null)
            {   
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
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
    }
}
