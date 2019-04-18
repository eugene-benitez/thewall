using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using The_Wall.Models;
using Microsoft.AspNetCore.Identity;

namespace The_Wall.Controllers
{
    public class HomeController : Controller
    {

        private MyContext dbContext;

        // here we can "inject" our context service into the constructor
        public HomeController(MyContext context)
        {
            dbContext = context;
        }


        public IActionResult Index()
        {
            DateTime CurrentTime = DateTime.Now;
            ViewBag.Now = CurrentTime;

            return View();
        }


        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetInt32("LoggedUser") == null)
            {
                return RedirectToAction("Login");
            }

            HttpContext.Session.GetInt32("LoggedUser");
            int? id = HttpContext.Session.GetInt32("LoggedUser");
            @ViewBag.UserId = (int)id;

            List<Messages> AllMessages = dbContext.Messages
            .Include(c => c.Message_Creator)
            .ToList();

            Users LoggedUser = dbContext.Users
            .FirstOrDefault(i => i.UserID == id);

            List<Comments> AllComments = dbContext.Comments
            .Include(c => c.Comment_Creator)
            .ToList();

            @ViewBag.User = LoggedUser;
            @ViewBag.AllMessages = AllMessages;
            @ViewBag.AllComments = AllComments;
            return View();
        }

        [HttpGet("commentpartial")]
        public IActionResult CommentPartial()
        {
            HttpContext.Session.GetInt32("LoggedUser");
            int? id = HttpContext.Session.GetInt32("LoggedUser");
            @ViewBag.UserId = (int)id;

            List<Comments> AllComments = dbContext.Comments
            .Include(c => c.Comment_Creator)
            .ToList();
            @ViewBag.AllComments = AllComments;

            List<Messages> AllMessages = dbContext.Messages
            .Include(c => c.Message_Creator)
            .ToList();

            @ViewBag.AllMessages = AllMessages;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }




        //!!!!!!!!!!!  POSTS
        //!!!!!!!!!!!  POSTS
        //!!!!!!!!!!!  POSTS
        //!!!!!!!!!!!  POSTS
        //!!!!!!!!!!!  POSTS

        [HttpPost]
        [Route("submit")]
        public IActionResult Submit(Users NewUser)
        {
            if (ModelState.IsValid)
            {
                if (dbContext.Users.Any(u => u.Email == NewUser.Email))
                {

                    ModelState.AddModelError("Email", "Email already in use!");

                    return View("Index");
                }

                // Initializing a PasswordHasher object, providing our User class as its
                PasswordHasher<Users> Hasher = new PasswordHasher<Users>();
                NewUser.Password = Hasher.HashPassword(NewUser, NewUser.Password);
                //Save your user object to the database
                dbContext.Add(NewUser);
                // OR dbContext.Users.Add(newUser);
                dbContext.SaveChanges();
                // Other code
                return RedirectToAction("Login");
            }

            DateTime CurrentTime = DateTime.Now;
            ViewBag.Now = CurrentTime;
            return View("Index");
        }

        [HttpPost("log")]
        public IActionResult Log(LoginUser submission)
        {
            var userInDb = dbContext.Users.FirstOrDefault(u => u.Email == submission.Email);
            if (ModelState.IsValid)
            {
                // If inital ModelState is valid, query for a user with provided email
                // If no user exists with provided email
                if (userInDb == null)
                {
                    // Add an error to ModelState and return to View!
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("login");
                }

                // Initialize hasher object
                var hasher = new PasswordHasher<LoginUser>();

                // varify provided password against hash stored in db
                var result = hasher.VerifyHashedPassword(submission, userInDb.Password, submission.Password);

                // result can be compared to 0 for failure
                if (result == 0)
                {
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("Login");
                }
            }
            HttpContext.Session.SetInt32("LoggedUser", userInDb.UserID);
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        [Route("createmessage")]
        public IActionResult CreateMessage(Messages newMessage)
        {
            newMessage.UserID = (int)HttpContext.Session.GetInt32("LoggedUser");
            dbContext.Add(newMessage);

            dbContext.SaveChanges();

            return RedirectToAction("Dashboard");
        }


        [HttpPost]
        [Route("createcomment")]
        public IActionResult CreateComment(Comments newComment)
        {

            dbContext.Add(newComment);

            dbContext.SaveChanges();

            return RedirectToAction("Dashboard");
        }

    }
}
