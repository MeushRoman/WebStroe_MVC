using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebStore.Context;
using WebStore.Infrastructure.Handlers;
using WebStore.Infrastructure.Services;
using WebStore.Models;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly SessionManagementService _sessionManagementService;
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext _dbContext;
        private Cart _cart;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext, SessionManagementService sessionManagementService, Cart cart)
        {
            _dbContext = dbContext;
            _logger = logger;
            _sessionManagementService = sessionManagementService;
           _cart = cart;
        }

        public IActionResult Index()
        {
            ViewBag.Cart = _cart;
            ViewBag.Categories = _dbContext.Categories.ToList();


            ViewBag.Products = _dbContext.Products.ToList();
            return View();
        }

        [HttpGet]
        public IActionResult Buy(int? id)
        {            
            var product = _dbContext.Products.FirstOrDefault(p => p.Id == id);
                        
            _cart.AddItem(product, 1);

            if (id == null) return RedirectToAction("Index");
            ViewBag.ProductId = id;
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public IActionResult Privacy()
        {
            var session = HttpContext.Session.GetString("SessionId");
            if (!(session is null))
            {

                var userId = _sessionManagementService.GetUserIdBySession(session);
                var userRole = _dbContext.Users.Include(p => p.Role).FirstOrDefault(p => p.Id == userId).Role.Name;

                if (userRole == "Administrator")
                {
                    return View();
                }
               
            }
            return Redirect("NotFound");// View("NotFound");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
