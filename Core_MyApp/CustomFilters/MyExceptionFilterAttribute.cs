using Core_MyApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
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
        private readonly IModelMetadataProvider _modelMetata;
        private readonly CompanyContext _ctx;

        public MyExceptionFilterAttribute(IModelMetadataProvider modelMetata, CompanyContext ctx)
        {
            _modelMetata = modelMetata;
            _ctx = ctx;
        }

        public void OnException(ExceptionContext context)
        {
            

            // 1. Handle Exception, so that the request processing will stop
            // the logic for handling post exception mechanism will start execute
            // like catch block
            context.ExceptionHandled = true;
            // 2. Read the  excpetion
            string message = context.Exception.Message;
            // 3. Set the Result so that it will be responded
            // a. Define a ViewResult
            var viewResult = new ViewResult();
            // b. Define Custom Error View
            viewResult.ViewName = "CustomError";
            // c. Define ViewDataDictionary with custom keys
            var viewDictionary = new ViewDataDictionary(_modelMetata, context.ModelState);
            viewDictionary["ControllerName"] = context.RouteData.Values["controller"].ToString();
            viewDictionary["ActionName"] = context.RouteData.Values["action"].ToString();
            viewDictionary["ErrorMessage"] = message;
            // d. pass the ViewDataDictionary to the ViewData property
            viewResult.ViewData = viewDictionary;
            // e. define the ViewResult
            context.Result = viewResult;
        }
    }
}
