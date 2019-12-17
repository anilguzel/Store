using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Store.WebUI.ViewModels;

namespace Store.WebUI.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            // Index, Update, Delete gibi islemler jQuery ile API`ye atilan istek ile saglanmaktadir. bu javascript dosyasina; wwwroot/js/Product.js den ulasabilirsiniz.

            return View();
        }
        public ActionResult Insert()
        {
            return View();
        }
    }
}