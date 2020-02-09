using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Infrastructure.Helpers
{
    public class HandlerResponse
    {
        public int StatusCode { get; set; }
        public string Discription { get; set; }
        public string SessionId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public KeyValuePair<string, string> Route { get; set; }
    }
}
