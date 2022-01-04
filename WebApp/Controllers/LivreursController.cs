using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class LivreursController : Controller
    {
        private ILivreursManager LivreursManager { get; }
        private ICommandesManager CommandesManager { get; }
        private IUtilisateursManager UtilisateursManager { get; }

        public LivreursController(ILivreursManager livreursManager, ICommandesManager commandesManager, IUtilisateursManager utilisateursManager)
        {
            LivreursManager = livreursManager;
            CommandesManager = commandesManager;
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
                var commande = CommandesManager.GetCommande(idCommande);

                var user = UtilisateursManager.GetUserId(commande.IdUtilisateur);

                return View(user);
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
