using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeWork_5_1_2019.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HomeWork_5_1_2019.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private IHostingEnvironment _environment;
        private string _connectionstring;

        public UserController(IHostingEnvironment environment,
            IConfiguration configuration)
        {
            _environment = environment;
            _connectionstring = configuration.GetConnectionString("ConStr");
        }
        public IActionResult MyQuestions()
        {
            var DB = new PQRepository(_connectionstring);
            return View(DB.GetUserQuestions(DB.GetUseridByEmail(User.Identity.Name)));
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Ask()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ask(Question Question, string Tagstring)
        {
            Question.DatePosted = DateTime.Now;
            var DB = new PQRepository(_connectionstring);
            Question.Id = DB.GetUseridByEmail(User.Identity.Name);
            DB.AddQuestion(Question, Tagstring.Split(",").ToList());
            return Redirect("/User/MyQuestions"); 
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync().Wait();
            return Redirect("/");
        }
    }
}