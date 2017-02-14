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
    public class CommentController : Controller
    {
        
        // private readonly MessageFactory messageFactory;
        private readonly CommentFactory commentFactory;

        public CommentController(CommentFactory comment) {
     
          commentFactory = comment;
            }



            [HttpPost]
        [Route("addcomments/{msgid}")]
        public IActionResult addcomments(Comment newcomment,int msgid)
        {   int user_id=(int)HttpContext.Session.GetInt32("user_id");
            TryValidateModel(newcomment);
            if(HttpContext.Session !=null){
                       
            if(ModelState.IsValid){
       
            commentFactory.Add(newcomment,user_id,msgid);            
            return RedirectToAction("messages","Message");
            }
            }
            
                ViewBag.Errors=ModelState.Values;
                return View("dashboard","Message");
              }
          }
          }