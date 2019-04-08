using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeddingPlanner.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace WeddingPlanner.Controllers
{
    public class HomeController : Controller
    {
        private weddingContext dbContext;
        public HomeController(weddingContext context)
        {
            dbContext = context;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            return View("Login");
        }

        [HttpPost("makeUser")]
        public IActionResult createUser(User newUser)
        {
            if(ModelState.IsValid)
            {              
                if ( dbContext.users.Any(u => u.email == newUser.email)){
                    return View("Login");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newUser.password = Hasher.HashPassword(newUser, newUser.password);
                System.Console.WriteLine(newUser.password.ToString());
                System.Console.WriteLine("--------------------------------------------------------------------------");
                dbContext.users.Add(newUser);
                dbContext.SaveChanges();
                HttpContext.Session.SetInt32("id", newUser.userid);
                System.Console.WriteLine(newUser.userid);
                return RedirectToAction("Dashboard");
                // System.Console.WriteLine("*******************************************************************");
            }
            else{
                return View("Login", newUser);
            }
                 
                

        }
        [HttpGet("login")]
        public IActionResult Login(){
            return View("Login");
        }
        [HttpPost("loginaction")]
        public IActionResult LoginAction(LoginUser userSubmission)
        {            
            if(ModelState.IsValid)
            {
                var userInDb = dbContext.users.FirstOrDefault(u => u.email == userSubmission.email);
                if(userInDb == null)
                {             
                    return View("Login");
                }
                var hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(userSubmission, userInDb.password, userSubmission.password);
                if(result == 0)
                {
                    return View("Login");
                }
                System.Console.WriteLine(userInDb.userid);
                HttpContext.Session.SetInt32("id", userInDb.userid);
                return RedirectToAction("Dashboard");
            } 
            return View("Login"); 
            
        }

        [HttpGet("Dashboard")]
        public IActionResult Dashboard(){
            if(HttpContext.Session.GetInt32("id") == null){
                return RedirectToAction("Login");
                
            }
            var wedding = dbContext.weddings.Include(p => p.bride).Include(p => p.groom).Include(p=>p.Guests).ThenInclude( p=>p.User ).ToList();
            System.Console.WriteLine("----------------------------------------------------------------------------");
            System.Console.WriteLine("----------------------------------------------------------------------------");
            System.Console.WriteLine("----------------------------------------------------------------------------");
            System.Console.WriteLine("----------------------------------------------------------------------------");
            System.Console.WriteLine("----------------------------------------------------------------------------");
            System.Console.WriteLine("----------------------------------------------------------------------------");
            System.Console.WriteLine(wedding[0].bride.firstName);
            System.Console.WriteLine(wedding[0].groom.firstName);
            ViewBag.wedding = wedding;
            ViewBag.id =HttpContext.Session.GetInt32("id");
            ViewBag.guests = dbContext.guests.ToList();
            return View();
            
        }
        [HttpGet("createWedding")]
          public IActionResult createWedding(){
            if(HttpContext.Session.GetInt32("id") == null){
                return RedirectToAction("Login");
                
            }
            var user = dbContext.users.ToList();
         
            ViewBag.user = user;
            
            return View();
        }
        [HttpGet("logout")]
        public IActionResult logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        [HttpPost("createWedding")]
          public IActionResult createWedding(Wedding currentWedding){
            if(HttpContext.Session.GetInt32("id") == null){
                return RedirectToAction("Login");
                
            }
            dbContext.weddings.Add(currentWedding);
            dbContext.SaveChanges();
            
            return RedirectToAction("Dashboard");
        }
        [HttpGet("infoWedding/{id}")]
        public IActionResult infoWedding(int id)
        {
            // var info = dbContext.weddings.Where(p=>p.weddingid = id).Include(p => p.bride).Include(p => p.groom).Include(p=>p.Guests).ThenInclude(p=>p.User);
            // ViewBag.info = info;
            return View("Info");
        }
        [HttpGet("delete/{id}")]
        public IActionResult deleteGuest(int id){

            return RedirectToAction("Dashboard");
        }            
        [HttpGet("remove/{id}")]
        public IActionResult removeGuest(int id){
            int sessionId = (int)HttpContext.Session.GetInt32("id");
 
            Guest guest = dbContext.guests.SingleOrDefault(e=>(e.weddingid ==id) && (e.userid == sessionId));
            dbContext.guests.Remove(guest);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }    
        [HttpGet("create/{id}")]
        public IActionResult createGuest(int id){
            int sessionId = (int)HttpContext.Session.GetInt32("id");
            Guest guest = new Guest{weddingid=id,userid =sessionId};
            dbContext.guests.Add(guest);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }        
    }
}
