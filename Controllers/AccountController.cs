using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Data.SqlClient;
using MC_Forum.Models;

namespace MC_Forum.Controllers
{
    public class AccountController : Controller
    {
        private readonly MyConfig _options;
        public AccountController(IOptions<MyConfig> optionsAccessor)
        {
            _options = optionsAccessor.Value;
        }
        public IActionResult Index()
        {
            using (SqlConnection connection = new SqlConnection(_options.ConnectionString))
            {
                string query = $"SELECT * FROM UserAccounts;";
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                List<UserAccount> list = new List<UserAccount>();
                try
                {
                    da.SelectCommand = new SqlCommand(query, connection);
                    da.Fill(ds, "Table");
                    dt = ds.Tables["Table"];
                    foreach (DataRow row in dt.Rows)
                    {
                        UserAccount user = new UserAccount();
                        user.ID = (int) row[0];
                        user.FirstName = (string) row[1];
                        user.LastName = (string) row[2];
                        user.Email = (string) row[3];
                        user.Username = (string) row[4];
                        list.Add(user);
                    }
                    return View(list);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"There was an error retrieving user accounts: \n {e.GetBaseException()} \n {e.StackTrace}");
                }
            }
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserAccount account)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(_options.ConnectionString))
                {
                    string query = $"INSERT INTO UserAccounts (FirstName, LastName, Email, Username, UserPassword" +
                    $"ConfirmPassword) values ('{account.FirstName}', '{account.LastName}', '{account.Email}', '{account.Username}'" +
                    $"'{account.UserPassword}', '{account.ConfirmPassword}');";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error trying to register user: \n " + e.StackTrace);
                        }
                    }
                }
                ModelState.Clear();
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserAccount user)
        {
            using (SqlConnection connection = new SqlConnection(_options.ConnectionString))
            {
                string query = $"SELECT UserID, Username FROM UserAccounts WHERE Username='{user.Username}' and UserPassword='{user.UserPassword}';";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            IDataRecord record = (IDataRecord)reader;

                            // Requires using Microsoft.AspNetCore.Http;
                            HttpContext.Session.SetString("Username", (string)record[1]);
                            HttpContext.Session.SetInt32("UserID", (int)record[0]);

                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            Console.WriteLine("User was unable to login.");
                            return View();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("There was an error trying to login user. \n"
                            + e.GetBaseException() + ":\n" + e.StackTrace);
                    }
                }
            }
            return View();
        }

        public IActionResult Profile()
        {
            // Has no content yet.
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}