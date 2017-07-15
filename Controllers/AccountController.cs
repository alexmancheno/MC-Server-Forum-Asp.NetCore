using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using MC_Forum.Models;

namespace MC_Forum.Controllers 
{
    public class AccountController : Controller 
    {
        private readonly MyConfig _options;
        const string SessionUsername = "_Username";
        const string SessionUserID = "_UserID";

        public AccountController(IOptions<MyConfig> optionsAccessor) 
        {
            _options = optionsAccessor.Value;
        }
        public IActionResult Index() 
        {

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
                    string query = String.Format("INSERT INTO UserAccounts (FirstName, LastName, Email, Username," +
                        "UserPassword, ConfirmPassword) values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", 
                        account.FirstName, account.LastName, account.Email, account.Username, account.UserPassword, account.ConfirmPassword);
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
                // Login 
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserAccount user)
        {
            using (SqlConnection connection = new SqlConnection(_options.ConnectionString))
            {
                string query = String.Format("SELECT * FROM UserAccounts WHERE Username='{0}' and UserPassword='{1}'", 
                    user.Username, user.UserPassword);
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try 
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            IDataRecord record = (IDataRecord) reader;
                            Console.WriteLine(String.Format("Account: {0}", record[1]));
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
            // Still needs to log out properly.
            return RedirectToAction("Index", "Home");
        }
    }
}