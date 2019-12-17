using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Store.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult Index()
        {
            // Index, Update, Delete gibi islemler jQuery ile API`ye atilan istek ile saglanmaktadir. bu javascript dosyasina; wwwroot/js/Category.js den ulasabilirsiniz.
            return View();
        }

        [HttpGet]
        public ActionResult Insert()
        {
            return View();
        }
    }
}