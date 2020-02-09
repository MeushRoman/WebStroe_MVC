using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Context;
using WebStore.Infrastructure.Abstract;
using WebStore.Infrastructure.Helpers;
using WebStore.Models;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Handlers
{
    public class OrderHandler : IHandler<OrderViewModel>
    {
        private ApplicationDbContext _dbContext;
        public OrderHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<HandlerResponse> Handle(OrderViewModel obj)
        {
            var newOrder = new Order()
            {
                Adress = obj.Address,
                OrderDate = DateTime.Now,
                DeliveryDate = obj.DeliveryDate
            };

            await _dbContext.Orders.AddAsync(newOrder);
            await _dbContext.SaveChangesAsync();

            var user = _dbContext.Users.FirstOrDefault(p => p.PhoneNumber == obj.Phone);

            if(user is null)
            {
                user = new User
                {
                    Name = obj.Name,
                    PhoneNumber = obj.Phone
                };
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
            }

            var userOrder = new UserOrder
            {
                Order = newOrder,
                UserId = user.Id
            };
            await _dbContext.UserOrders.AddAsync(userOrder);
            await _dbContext.SaveChangesAsync();

            foreach (var item in obj.CartLines)
            {
                await _dbContext.OrderProducts.AddAsync(new OrderProducts()
                {
                    Order = newOrder,
                    ProductId = item.Product.Id,
                    CountProduct = item.Quantity
                });
            }

            await _dbContext.SaveChangesAsync();

            return new HandlerResponse()
            {
                StatusCode = 201,
                Discription = "Created",
                Route = new KeyValuePair<string, string>("Home", "Index")
            };
        }
    }
}
