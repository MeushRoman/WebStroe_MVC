using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using WebStore.Context;
using WebStore.Infrastructure.Abstract;
using WebStore.Infrastructure.Helpers;
using WebStore.Models;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Handlers
{
    public class SignUpHandler : IHandler<SignUpViewModel>
    {
        private ApplicationDbContext _dbContext;
        public SignUpHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async  Task<HandlerResponse> Handle(SignUpViewModel model)
        {
            var res = await _dbContext.Users.FirstOrDefaultAsync(p => p.PhoneNumber == model.PhoneNumber);

            if (!(res is null)) 
                return new HandlerResponse() {
                    StatusCode = 200,
                    Discription = "", 
                    Route = new KeyValuePair<string, string>("Accoun", "SignUp") 
                } ;

            var user = new User()
            {
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
                Password = model.Password,
                RoleId = 2
            };

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return new HandlerResponse()
            {
                StatusCode = 201,
                Discription = "Created",
                Route = new KeyValuePair<string, string>("Accoun", "SignIn")
            };
        }
    }
}
