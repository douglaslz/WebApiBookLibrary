using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiBookLibrary.Helpers
{
    //This is a filter that is applied in two moments, Execiting or Excecuted
    //Write in the logger OnActionExecuting or OnActionExecuted
    public class MyActionFilter : IActionFilter
    {
        private readonly ILogger<MyActionFilter> logger;
        public MyActionFilter(ILogger<MyActionFilter> logger)
        {
            this.logger = logger;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
            logger.LogError("OnActionExecuted");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            logger.LogError("OnActionExecuting");
        }
    }
}
