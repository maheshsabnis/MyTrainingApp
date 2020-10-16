using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_MyApp.CustomFilters
{
    /// <summary>
    /// The CLass that will be used to handle Exception in MVC Controllers
    /// </summary>
    public class MyExceptionFilterAttribute : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
