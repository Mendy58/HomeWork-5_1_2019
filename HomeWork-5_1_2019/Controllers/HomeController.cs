using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeWork_5_1_2019.Models;
using HomeWork_5_1_2019.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace HomeWork_5_1_2019.Controllers
{
    public class HomeController : Controller
    {
        private IHostingEnvironment _environment;
        private string _connectionstring;

        public HomeController(IHostingEnvironment environment,
            IConfiguration configuration)
        {
            _environment = environment;
            _connectionstring = configuration.GetConnectionString("ConStr");
        }

        public IActionResult Index()
        {
            var DB = new PQRepository(_connectionstring);
            return View(DB.GetQuestions());
        }
        [HttpPost][Authorize]
        public IActionResult AnswerQuestion(Answer answer)
        {
            var DB = new PQRepository(_connectionstring);
            answer.DatePosted = DateTime.Now;
            answer.Userid = DB.GetUseridByEmail(User.Identity.Name); 
            DB.AddAnswer(answer);
            return Redirect($"/Home/Question?id={answer.Questionid}");
        }
        public IActionResult Question(int id)
        {
            var DB = new PQRepository(_connectionstring);
            return View(DB.GetQuestionWithAnswers(id));
        }       
        public ActionResult GetQuestions()
        {
            var DB = new PQRepository(_connectionstring);
            return View(DB.GetQuestions());
        }
        public ActionResult Login(string message)
        {
            return View(message);
        }
        [HttpPost]
        public ActionResult LoginUser(string email, string password)
        {
            var mgr = new PQRepository(_connectionstring);
            var user = mgr.AuthUser(email, password);
            if (user == null)
            {
                TempData["message"] = "Invalid login, Try again you Irish drunk!";
                return Redirect("/Home/login");
            }

            var claims = new List<Claim>
                {
                    new Claim("user", email)
                };
            HttpContext.SignInAsync(new ClaimsPrincipal(
                new ClaimsIdentity(claims, "Cookies", "user", "role"))).Wait();
            return RedirectToAction("MyQuestions","User");
        }
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(User user, string password)
        {
            user.PasswordHash = password;
            var mgr = new PQRepository(_connectionstring);
            mgr.AddUser(user);
            //FormsAuthentication.SetAuthCookie(user.email, true);
            var claims = new List<Claim>
                {
                    new Claim("user", user.email)
                };
            HttpContext.SignInAsync(new ClaimsPrincipal(
                new ClaimsIdentity(claims, "Cookies", "user", "role"))).Wait();
            return RedirectToAction("Ask", "User");
        }



    }
}
