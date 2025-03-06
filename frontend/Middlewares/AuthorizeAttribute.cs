namespace ASPNET_MVC.Middlewares;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using ASPNET_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using ASPNET_MVC.Models.Entity;
using System.Text.Json;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly IList<RoleEnum> _roles;

    public AuthorizeAttribute(params RoleEnum[] roles)
    {
        //_roles = roles ?? new Role[] { };
        _roles = roles ?? new RoleEnum[] { };
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // skip authorization if action is decorated with [AllowAnonymous] attribute
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous) return;

        try
        {
            var userLoggedIn = context.HttpContext.Items["UserLoggedIn"] as UserLoggedIn;
            List<RoleEnum> rolesEnum = new List<RoleEnum>();
            foreach (int roleId in userLoggedIn.Roles)
            {
                switch (roleId)
                {
					case 1: rolesEnum.Add(RoleEnum.Anonymous); break;
					case 2: rolesEnum.Add(RoleEnum.Admin); break;
					case 3: rolesEnum.Add(RoleEnum.Lecturer); break;
					case 4: rolesEnum.Add(RoleEnum.Student); break;
					case 5: rolesEnum.Add(RoleEnum.Staff); break;
					default: rolesEnum.Add(RoleEnum.Anonymous); break;
                }
            }
            if (userLoggedIn == null || (_roles.Any() && !_roles.Any(item => rolesEnum.Contains(item))))
            {
                // not logged in or role not authorized
                context.Result = new RedirectResult("/Error/Error401", false);
            }
        }
        catch(Exception ex)
        {
            context.Result = new RedirectResult("/Error/Error401", false);
        }

    }

}