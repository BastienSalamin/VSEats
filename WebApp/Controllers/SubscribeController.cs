using BLL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class SubscribeController : Controller
    {
        private IUtilisateursManager UtilisateursManager { get; }

        public SubscribeController(IUtilisateursManager utilisateursManager)
        {
            UtilisateursManager = utilisateursManager;
        }
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(SubscribeVM subscribeVM)
        {
            if (ModelState.IsValid)
            {
                // vérifier qu'il n'y ait pas d'utilisateurs avec le même login
                var listeUsers = UtilisateursManager.GetUtilisateurs();

                foreach (var user in listeUsers)
                {
                    if(user.Login.Contains(subscribeVM.Login))
                    {
                        ModelState.AddModelError("", "Cet e-mail est déjà utilisé !");
                        return View(subscribeVM);
                    }
                }
                
                UtilisateursManager.Subscribe(subscribeVM.Npa, subscribeVM.Nom, subscribeVM.Prenom, subscribeVM.Login, subscribeVM.MotDePasse, subscribeVM.Adresse, subscribeVM.NumTelephone);
                return RedirectToAction("Home", "Index");
                
            }
            else
            {
                ModelState.AddModelError("", "Erreur lors de l'insertion !");
            }

            return View(subscribeVM);
        }
    }
}
