using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Core_MyApp.CustomFilters
{
    public class MyLoggingFilter : ActionFilterAttribute, IActionFilter
    {

        private void LogRequest(string currentState, RouteData route)
        {
            string controller = route.Values["controller"].ToString();
            string action = route.Values["action"].ToString();

            string log = $"Current Execution is {currentState} in Controller {controller} in action {action}";
            Debug.WriteLine(log);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            LogRequest("Action Executing", context.RouteData);
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            LogRequest("Action Executed", context.RouteData);
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            LogRequest("Result Executing", context.RouteData);
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            LogRequest("Result Executing", context.RouteData);
        }
    }
}
