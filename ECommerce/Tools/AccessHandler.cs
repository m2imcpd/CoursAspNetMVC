using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Tools
{
    public class AccessHandler : AuthorizationHandler<AccessRequirement>
    {
        private ILoginService loginService;

        public AccessHandler(ILoginService _loginService)
        {
            loginService = _loginService;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AccessRequirement requirement)
        {
            if(!(loginService.TestConnection() && requirement.TypeProfil <= loginService.GetUserProfil()))
            {
                var res = context.Resource as AuthorizationFilterContext;
                res.Result = new RedirectToActionResult("Index", "User", null);
            }
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
