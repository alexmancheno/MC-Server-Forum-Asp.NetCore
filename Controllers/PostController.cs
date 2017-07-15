using MC_Forum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MC_Forum.Controllers
{
    public class PostController : Controller
    {
        private readonly MyConfig _options;
        const string SessionUsername = "_Username";
        const string SessionUserID = "_UserID";
        public PostController(IOptions<MyConfig> optionsAccessor) 
        {
            _options = optionsAccessor.Value;
        }
        public IActionResult Index()
        {
            // This view still needs a lot work.
            return View();
        }

        public IActionResult SubmitPost()
        {
            // This view is getting there.
            return View(); 
        }

        [HttpPost]
        public ActionResult SubmitPost(Post post)
        {
            return View();
        }
    }
}