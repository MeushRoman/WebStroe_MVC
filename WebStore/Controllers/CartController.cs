using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebStore.Context;
using WebStore.Infrastructure.Handlers;
using WebStore.Models;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext _dbContext;
        private OrderHandler _orderHandler;
        private Cart _cart;
        public CartController(
            ApplicationDbContext dbContext,
            OrderHandler orderHandler,
            Cart cart
            )
        {
            _dbContext = dbContext;
            _orderHandler = orderHandler;
            _cart = cart;
        }

        public IActionResult List()
        {
            return View(new CartViewModel() 
            {
                Cart = _cart 
            });
        }

        [HttpGet]
        public IActionResult OrderRegistration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> OrderRegistration(OrderViewModel model, string SessionId)
        {
            if (!ModelState.IsValid) return View();
            model.CartLines = _cart.Lines;
            
            var res = await _orderHandler.Handle(model);                      

            return View();
        }
        [HttpGet]
        public IActionResult RemoveFromCart(int Id)
        {
            var product = _dbContext.Products
                .FirstOrDefault(p => p.Id == Id);

            if (product != null)
            {
                _cart.RemoveLine(product);
            }
            

            return RedirectToAction("List");
        }
    }
}