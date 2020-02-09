using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Context;
using WebStore.Models;

namespace WebStore.Infrastructure.Services
{
    public class SessionManagementService
    {
        private readonly Dictionary<string, int> _activeSessionUserMappings;
        //private ApplicationDbContext _dbContext;

        public SessionManagementService()
        {
            _activeSessionUserMappings = new Dictionary<string, int>();
           // _dbContext = dbContext;
        }

        public string AddSession(int userId)
        {
            string sessionId = Guid.NewGuid().ToString();
            _activeSessionUserMappings[sessionId] = userId;

            return sessionId;
        }

        public int? GetUserIdBySession(string sessionId)
        {
            if (_activeSessionUserMappings.ContainsKey(sessionId))
                return _activeSessionUserMappings[sessionId];

            else return null;
        }        

        public string GetUserRoleNameBySession(string sessionId, ApplicationDbContext _dbContext)
        {
            int? userId = GetUserIdBySession(sessionId);

            if (userId == null) return null;

            var res = _dbContext.Users.Include(p => p.Role).FirstOrDefault(p => p.Id == p.Id).Role.Name;

            return res;
        }
    }
}
