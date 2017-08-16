using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MC_Forum.Models;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace MC_Forum.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyConfig _options;
        const string SessionKeyUsername = "_Username";
        const string SessionKeyUserID = "_UserID";

        public HomeController(IOptions<MyConfig> optionsAccessor) 
        {
            _options = optionsAccessor.Value;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                ViewData["Username"] = HttpContext.Session.GetString("Username");
            } 
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            if (HttpContext.Session.GetString("Username") != null)
            {
                ViewData["Username"] = HttpContext.Session.GetString("Username");
            } 
            return View();
        }

        public IActionResult Contact()
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                ViewData["Username"] = HttpContext.Session.GetString("Username");
            } 
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
