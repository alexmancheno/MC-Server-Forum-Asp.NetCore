using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MC_Forum.Models;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace MC_Forum.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyConfig _options;
        const string SessionUsername = "_Username";
        const string SessionUserID = "_UserID";

        public HomeController(IOptions<MyConfig> optionsAccessor) 
        {
            _options = optionsAccessor.Value;
        }
        public IActionResult Index()
        {
            // using (SqlConnection connection = new SqlConnection(@"Data Source=173.56.89.207,1433;Initial Catalog=MC_Forum;User ID=sa;Password=Supersecretpassword12"))
            // {
            //     using (SqlCommand command = new SqlCommand("Select * from Alex", connection))
            //     {
            //         Console.WriteLine("Hello");
            //         connection.Open();
            //         Console.WriteLine("Connection opened!");
            //         command.ExecuteReader();
            //         Console.WriteLine("Command executed!");
            //     }
            // }
            Console.WriteLine("connection string: " + _options.ConnectionString);
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
