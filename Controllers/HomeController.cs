using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using theWall.Models;
using theWall.Factory;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
namespace theWall.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly UserFactory userFactory;
        public HomeController(UserFactory user ) {
     userFactory = user;
     
            }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.Errors = "";
            return View("index");
        }
             
        [HttpPost]
        [Route("method")]
        public IActionResult method(User newuser)
        {   
            String Email=newuser.email;
            var user1=userFactory.FindByemail(Email);       
            if(ModelState.IsValid){
                if(user1==null)
                {
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newuser.password = Hasher.HashPassword(newuser, newuser.password);
                userFactory.Add(newuser);
                return RedirectToAction("index"); 
                }
                ViewBag.thing="already exist";
            }                         
                
            
            TryValidateModel(newuser);
            ViewBag.Errors = ModelState.Values;
            return View("index");
        }
        [HttpPost]
        [Route("login")]
        public IActionResult login(string email,string password)
        {   
            String Email=email;
            string passwordtocheck=password;
            
            if(Email!=null && passwordtocheck!=null){
                var user1=userFactory.FindByemail(Email);
                
            if(user1!=null)
                {
                var Hasher = new PasswordHasher<User>();
                if(0 != Hasher.VerifyHashedPassword(user1, user1.password, passwordtocheck))
                {
                HttpContext.Session.SetInt32("user_id",(int)user1.id);
                HttpContext.Session.SetString("name",user1.f_name);
                ViewBag.user=user1.f_name;
                return RedirectToAction("messages","Message"); 
                }
                
                ViewBag.thing1="password and email donot match";
                Console.WriteLine(ViewBag.thing1);  
                return View("Index");      
            }                         
            }                    
              ViewBag.thing1="email or password donot exist";
                  return View("Index");
            
           
            }
            
            [HttpGet]
            [Route("logout")]
            public IActionResult logout(){
                HttpContext.Session.Clear();
                
                return RedirectToAction("Index");
            }
            

    }
}
