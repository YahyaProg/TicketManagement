using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace Core.CustomAttribute;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AllowRolesAttribute() : AuthorizeAttribute, IAuthorizationFilter
{
    private readonly string[] _roles;

    public AllowRolesAttribute(string[] roles) : this() 
    {
        _roles = roles;
    }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (_roles.Length > 0)
        {
            var helper = context.HttpContext.RequestServices.GetService<IUserHelper>();
            var user = helper.GetUserFromToken();
            if (user is null)
            {
                context.Result = new ForbidResult();
                return;
            }

            // Check if the user's role is in the allowed roles
            if (_roles.FirstOrDefault(x => x == user.Role.ToString()) is null) // Case-insensitive comparison
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}
