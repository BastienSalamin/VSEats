﻿using BLL;
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
        public ILocalitesManager LocalitesManager { get; set; }

        public SubscribeController(IUtilisateursManager utilisateursManager, ILocalitesManager localitesManager)
        {
            UtilisateursManager = utilisateursManager;
            LocalitesManager = localitesManager;
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
                    if (user.Login.Contains(subscribeVM.Login))
                    {
                        ModelState.AddModelError("", "Cet e-mail est déjà utilisé !");
                        return View(subscribeVM);
                    }
                }

                UtilisateursManager.Subscribe(subscribeVM.Npa, subscribeVM.Nom, subscribeVM.Prenom, subscribeVM.Login, subscribeVM.MotDePasse, subscribeVM.Adresse, subscribeVM.NumTelephone);
                return RedirectToAction("Index", "Home");

            }
            else
            {
                ModelState.AddModelError("", "Erreur lors de l'insertion !");
            }

            return View(subscribeVM);
        }

        //pour Edit les données
        public IActionResult Edit(int id)
        {
            var util = UtilisateursManager.GetUserId(id);
            var localites = LocalitesManager.GetLocalites();
            int npa = 0;

            foreach (var localite in localites)
            {
                if(localite.IdLocalite == util.IdLocalite)
                {
                    npa = localite.NPA;
                }
            }

            SubscribeVM membre = new SubscribeVM();
            membre.Npa = npa;
            membre.Nom = util.Nom;
            membre.Prenom = util.Prenom;
            membre.Login = util.Login;
            membre.MotDePasse = util.MotDePasse;
            membre.Adresse = util.Adresse;
            membre.NumTelephone = util.NumTelephone;

            return View(membre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SubscribeVM subscribeVM)
        {
            if (ModelState.IsValid)
            {
                UtilisateursManager.Update(subscribeVM.Npa,subscribeVM.Nom,subscribeVM.Prenom,subscribeVM.Login,subscribeVM.MotDePasse,subscribeVM.Adresse,subscribeVM.NumTelephone);
                return RedirectToAction("Index","Home");
            }

            return View(subscribeVM);
        }

    }
}