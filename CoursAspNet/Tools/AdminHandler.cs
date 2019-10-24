using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursAspNet.Tools
{
    //public class AdminHandler : IAuthorizationHandler
    //{
    //    public Task HandleAsync(AuthorizationHandlerContext context)
    //    {
    //        AdminRequirement requirement = (AdminRequirement)context.Requirements.ToList()[0];
    //        return Task.CompletedTask;
    //    }
    //}

    public class AdminHandler : AuthorizationHandler<AdminRequirement>
    {
        private IHttpContextAccessor accessor;

        public AdminHandler(IHttpContextAccessor _accessor)
        {
            accessor = _accessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
        {
            string cookieValue = accessor.HttpContext.Request.Cookies["user"];
            if(cookieValue == requirement.TypeProfil)
            {
                context.Succeed(requirement);
            }
            else
            {
                //Resource est de type object à caster en AuthorizationFilterContext pour pouvoir appliquer des redirections
                var result = context.Resource as AuthorizationFilterContext;
                result.Result = new RedirectToActionResult("Index", "Client", null);
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
