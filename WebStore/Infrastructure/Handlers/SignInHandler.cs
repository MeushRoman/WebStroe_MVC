using Microsoft.AspNetCore.Authentication.Cookies;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebStore.Context;
using WebStore.Infrastructure.Abstract;
using WebStore.Infrastructure.Helpers;
using WebStore.Infrastructure.Services;
using WebStore.Models;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Handlers
{
    public class SignInHandler : IHandler<SignInViewModel>
    {
        private ApplicationDbContext _dbContext;
        SessionManagementService _sessionManagementService;
        public SignInHandler(ApplicationDbContext dbContext, SessionManagementService sessionManagementService)
        {
            _sessionManagementService = sessionManagementService;
            _dbContext = dbContext;
        }

        public async Task<HandlerResponse> Handle(SignInViewModel model)
        {
            var user = await _dbContext.Users.Include(p=>p.Role).FirstOrDefaultAsync(p => p.PhoneNumber == model.PhoneNumber && p.Password == model.Password);

            if (user is null) 
                return new HandlerResponse() 
                { 
                    StatusCode = 404, 
                    Discription = "Not Found", 
                    Route = new KeyValuePair<string, string>("Accoun", "SignIn") 
                };

            var sessionId = _sessionManagementService.AddSession(user.Id);

            return new HandlerResponse()
            {   
                StatusCode = 200,
                SessionId = sessionId,
                UserId = user.Id,
                UserName = user.Name,
                Discription = "Ok",
                Route = new KeyValuePair<string, string>("Account", "MyCabinet")
            };
        }
    }
}
