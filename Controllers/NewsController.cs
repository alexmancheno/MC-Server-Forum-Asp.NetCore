using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MC_Forum.Models;
using Microsoft.Extensions.Options;

namespace MC_Forum.Controllers
{
    public class NewsController : Controller
    {
    
        private readonly MyConfig _options;
        public NewsController(IOptions<MyConfig> optionsAccessor) 
        {
            _options = optionsAccessor.Value;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SubmitPost()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        public IActionResult SubmitPost(Post post)
        {
            if (HttpContext.Session.GetInt32("UserID") != null)
            { 
                using (SqlConnection connection = new SqlConnection(_options.ConnectionString))
                {
                    string query = $"INSERT INTO Posts (PostTitle, PostBody, PostTopic, UserID) values ('{post.PostTitle}', '{post.PostBody}', 0, {HttpContext.Session.GetInt32("UserID")})";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        try 
                        {
                            Console.WriteLine(query);
                            connection.Open();
                            command.ExecuteNonQuery();
                            Console.WriteLine("Success submitting post!");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"There was an error inserting into Posts: \n {e.GetBaseException()} \n {e.StackTrace}");
                        }
                    }
                }
                return RedirectToAction("Index", "News");
            }
            return RedirectToAction("Login", "Account");
        }
    }
}