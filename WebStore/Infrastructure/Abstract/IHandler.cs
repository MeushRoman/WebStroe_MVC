using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Infrastructure.Helpers;

namespace WebStore.Infrastructure.Abstract
{
    public interface  IHandler<T>
    {
        Task<HandlerResponse> Handle(T obj);
    }
}
