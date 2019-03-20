using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Attributes
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }

            CheckPostForIdempotentHeader(context);
        }

        private void CheckPostForIdempotentHeader(ActionExecutingContext context)
        {
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            string actionName = controllerActionDescriptor?.ActionName;
            if (actionName == "Post")
            {
                Guid guidOutput;
                var idempotentKey = context.HttpContext.Request.Headers["idempotentKey"].ToString();
                var isValid = Guid.TryParse(idempotentKey, out guidOutput);
                
                if (!isValid)
                {
                    context.Result = new BadRequestObjectResult("Missing or invalid Itempotent key header");
                }
            }
        }
    }
}