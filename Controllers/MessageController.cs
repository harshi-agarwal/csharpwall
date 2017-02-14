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
    public class MessageController : Controller
    {
        
        private readonly MessageFactory messageFactory;
        private readonly CommentFactory commentFactory;

        public MessageController(MessageFactory message,CommentFactory comment) {
     
     messageFactory = message;
     commentFactory = comment;
            }


[HttpGet]
[Route("messages")]
            public IActionResult messages(){
                ViewBag.user=HttpContext.Session.GetString("name");
                ViewBag.messages= messageFactory.FindAll();
                ViewBag.comments= commentFactory.FindAll();
                return View("dashboard");
            }

            [HttpPost]
        [Route("addmessages")]
        public IActionResult addmessages(Message newmessage)
        {   int user_id=(int)HttpContext.Session.GetInt32("user_id");
            TryValidateModel(newmessage);
            if(HttpContext.Session !=null)
            {
              if(ModelState.IsValid)
              {
                messageFactory.Add(newmessage,user_id);            
                return RedirectToAction("messages");
              }
            }
            ViewBag.Errors=ModelState.Values;
            return View("dashboard");
              }
          }
          }