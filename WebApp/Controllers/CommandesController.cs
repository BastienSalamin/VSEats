using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class CommandesController : Controller
    {
        private ICommandesManager CommandesManager { get; }

        public CommandesController(ICommandesManager commandesManager)
        {
            CommandesManager = commandesManager;
        }

        public IActionResult Index()
        {
            var idUser = HttpContext.Request.Cookies["IdUtilisateur"];

            if (idUser != null)
            {
                var id = Int32.Parse(idUser);
                var commandes = CommandesManager.GetCommandes(id);
                if(commandes != null)
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
