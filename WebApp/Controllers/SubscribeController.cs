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

        public IActionResult Index(SubscribeVM subscribeVM)
        {
            if (ModelState.IsValid)
            {
                UtilisateursManager.Subscribe();
            }
        }
    }
}
