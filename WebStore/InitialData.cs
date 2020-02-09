using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Context;
using WebStore.Models;

namespace WebStore
{
    public static class InitialData
    {
        public static void Initialize(ApplicationDbContext dbContext)
        {
            if (!dbContext.Products.Any())
            {
                dbContext.Roles.AddRange(
                    new Role() { Name = "Administrator" },
                    new Role() { Name = "FunnyUser" });

                dbContext.SaveChanges();

                dbContext.Users.AddRange(new User() { Name = "User1", Password = "12345", PhoneNumber = "87227766554", RoleId = 1 },
                                         new User() { Name = "User2", Password = "12345", PhoneNumber = "87345324341", RoleId = 2 },
                                         new User() { Name = "User3", Password = "12345", PhoneNumber = "87346323444", RoleId = 2 },
                                         new User() { Name = "User4", Password = "12345", PhoneNumber = "87454723254", RoleId = 2 },
                                         new User() { Name = "User5", Password = "12345", PhoneNumber = "87457453554", RoleId = 1 }
                                         );
                dbContext.SaveChanges();

                dbContext.Stores.Add(new Store() { Name = "BestStore" });
                dbContext.SaveChanges();

                dbContext.Categories.AddRange(new Category() { Name = "Category 1" },
                                              new Category() { Name = "Category 2" },
                                              new Category() { Name = "Category 3" }
                                              );
                dbContext.SaveChanges();

                dbContext.Products.AddRange(
                       new Product() { Name = "Product 1", Discription = "test discription 1", Price = 1230.0, CategoryId = 1, StoreId = 1 },
                       new Product() { Name = "Product 2", Discription = "test discription 2", Price = 1230.0, CategoryId = 1, StoreId = 1 },
                       new Product() { Name = "Product 3", Discription = "test discription 3", Price = 1230.0, CategoryId = 1, StoreId = 1 },
                       new Product() { Name = "Product 4", Discription = "test discription 4", Price = 1230.0, CategoryId = 2, StoreId = 1 },
                       new Product() { Name = "Product 5", Discription = "test discription 5", Price = 1230.0, CategoryId = 2, StoreId = 1 },
                       new Product() { Name = "Product 6", Discription = "test discription 6", Price = 1230.0, CategoryId = 3, StoreId = 1 },
                       new Product() { Name = "Product 7", Discription = "test discription 7", Price = 1230.0, CategoryId = 3, StoreId = 1 }
                     );
                dbContext.SaveChanges();

                dbContext.OrderStatuses.AddRange(
                    new OrderStatus { Name = "Delivered" },
                    new OrderStatus { Name = "Shipped" },
                    new OrderStatus { Name = "Ready to ship" });
                dbContext.SaveChanges();

                dbContext.Orders.AddRange(
                    new Order() { Adress = "testAdres1", OrderDate = DateTime.Now, StatusId = 1 },
                    new Order() { Adress = "testAdres2", OrderDate = DateTime.Now, StatusId = 2 },
                    new Order() { Adress = "testAdres3", OrderDate = DateTime.Now, StatusId = 3 },
                    new Order() { Adress = "testAdres4", OrderDate = DateTime.Now, StatusId = 1 },
                    new Order() { Adress = "testAdres5", OrderDate = DateTime.Now, StatusId = 2 },
                    new Order() { Adress = "testAdres5", OrderDate = DateTime.Now, StatusId = 3 },
                    new Order() { Adress = "testAdres5", OrderDate = DateTime.Now, StatusId = 1 }
                    );
                dbContext.SaveChanges();

                dbContext.UserOrders.AddRange(
                    new UserOrder() { OrderId = 1, UserId = 1},
                    new UserOrder() { OrderId = 1, UserId = 1},
                    new UserOrder() { OrderId = 2, UserId = 3},
                    new UserOrder() { OrderId = 3, UserId = 2}
                    );
                dbContext.SaveChanges();
            }
        }

    }
}
