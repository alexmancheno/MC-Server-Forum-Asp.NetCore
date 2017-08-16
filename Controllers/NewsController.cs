using System;
using System.Collections.Generic;
using System.Data;
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
            if (HttpContext.Session.GetString("Username") != null)
            {
                ViewData["Username"] = HttpContext.Session.GetString("Username");
            }

            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            List<Post> list = new List<Post>();
            using (SqlConnection connection = new SqlConnection(_options.ConnectionString))
            {
                string query = "Select * FROM Posts WHERE PostTopic=0";
                da.SelectCommand = new SqlCommand(query, connection);
                da.Fill(ds, "Table");
                dt = ds.Tables["Table"];
                Console.WriteLine($"Size of: {dt.Rows.Count}");
                foreach (DataRow row in dt.Rows)
                {
                    Post post = new Post();
                    post.ID = (int)row[0];
                    post.PostTitle = (string)row[1];
                    post.PostBody = (string)row[2];
                    post.PostTopic = (int)row[3];
                    // Missing DatePosted field!!!
                    post.UserID = (int)row[4];
                    list.Add(post);
                    // Console.WriteLine($"{row[0]}, {row[1]}, {row[2]}, {row[3]}, {row[4]}");
                }
            }
            return View(list);
        }

        public IActionResult SubmitPost()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewData["Username"] = HttpContext.Session.GetString("Username");   
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
                            connection.Open();
                            command.ExecuteNonQuery();
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