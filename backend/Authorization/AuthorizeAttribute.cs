namespace ASPNET_API.Authorization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ASPNET_API.Domain.Entities;
using ASPNET_API.Application.DTOs;
using ASPNET_API.Infrastructure.Data;

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
        if (allowAnonymous)
            return;

        // authorization
        var user = (User)context.HttpContext.Items["User"];
        try
        {
            var _context = context.HttpContext.RequestServices.GetService<DonationWebApp_v2Context>();
            var userRoles = _context.Roles
            .Where(r => r.UserRoles.Any(ur => ur.UserId == user.UserId))
            .ToList();
            var userDTO = toDTO(user, userRoles);
            if (user == null || (_roles.Any() && !_roles.Any(item => userDTO.Roles.Contains(item))))
            {
                // not logged in or role not authorized
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
        catch(Exception ex)
        {
            //context.Result = new RedirectResult("/Error/Error401", false);
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }

    }

    public UserDTO toDTO(User user, List<Role> roles)
    {
        var userDTO = new UserDTO();
        userDTO.UserId = user.UserId;
        userDTO.UserName = user.UserName;
        userDTO.FirstName = user.FirstName;
        userDTO.LastName = user.LastName;

        List<RoleEnum> rolesEnum = new List<RoleEnum>();
        foreach(Role role in roles)
        {
            switch(role.RoleId)
            {
				case 1: rolesEnum.Add(RoleEnum.Anonymous); break;
				case 2: rolesEnum.Add(RoleEnum.Admin); break;
                case 3: rolesEnum.Add(RoleEnum.Lecturer); break;
                case 4: rolesEnum.Add(RoleEnum.Student); break;
				case 5: rolesEnum.Add(RoleEnum.Staff); break;
				default: rolesEnum.Add(RoleEnum.Anonymous); break;
            }
        }
        userDTO.Roles = rolesEnum;
        return userDTO;
    }
}