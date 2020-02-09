using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStore.Context;
using WebStore.Infrastructure.Handlers;
using WebStore.Infrastructure.Services;
using WebStore.Models;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationDbContext _dbContext;
        private readonly SessionManagementService _sessionManagementService;
        private SignUpHandler _signUp;
        private SignInHandler _signIn;

        
        public AccountController(ApplicationDbContext dbContext,
            SessionManagementService sessionManagementService,
            SignUpHandler signUp,
            SignInHandler signIn)
        {
            _dbContext = dbContext;
            _signUp = signUp;
            _signIn = signIn;
            _sessionManagementService = sessionManagementService;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInViewModel signInModel)
        {
            if (!ModelState.IsValid) return View();

            var res = await _signIn.Handle(signInModel);
            
            if(res.StatusCode != 200) return View();

            CookieOptions cookie = new CookieOptions();

            cookie.Expires = DateTime.Now.AddDays(3);

            Response.Cookies.Append("Session", res.SessionId, cookie);
            Response.Cookies.Append("UserName", res.UserName, cookie);


               //var user = await _dbContext.Users.Include(p => p.Role).FirstOrDefaultAsync(p => p.PhoneNumber == signInModel.PhoneNumber);
                //var session = _sessionManagementService.AddSession(user.Id);

                //HttpContext.Session.SetString("SessionId", session);
                //HttpContext.Session.SetString("UserName", user.Name);
                //ViewBag.UserName = user.Name;

                //ViewBag.User = user.Name;
            

            return RedirectToRoute(new { controller = res.Route.Key, action = res.Route.Value });
        } 

        [HttpGet]
        public IActionResult SignUp()
        {          

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUpViewModel signUp)
        {
            if (!ModelState.IsValid) return View();

            var res = await _signUp.Handle(signUp);

            if(res.StatusCode != 200) return View();
            
            return RedirectToRoute(new { controller = res.Route.Key, action = res.Route.Value });
        }

        [HttpGet]
        public IActionResult MyCabinet()
        {
            var sessionId = Request.Cookies["Session"];

            if (String.IsNullOrEmpty(sessionId)) return RedirectToRoute(new { controller = "Account", action = "SignIn" });

            int? userId = _sessionManagementService.GetUserIdBySession(sessionId);

            if (userId is null) return RedirectToRoute(new { controller = "Account", action = "SignIn" });

            var user = _dbContext.Users.Include(p => p.UserOrders).FirstOrDefault(p => p.Id == userId);
            var orders = _dbContext.UserOrders.Include(p => p.Order).Where(p => p.UserId == user.Id).ToList();
           

            ViewBag.User = user;
            ViewBag.Orders = orders;
                                
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> MyCabinet(string sessionId)
        //{   

        //    return View();
        //}
    }
}