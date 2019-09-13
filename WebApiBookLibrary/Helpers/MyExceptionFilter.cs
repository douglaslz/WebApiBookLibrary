using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiBookLibrary.Helpers
{
    public class MyExceptionFilter :ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            
        }
    }
}
