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

        public LivreursController(ILivreursManager livreursManager)
        {
            LivreursManager = livreursManager;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
