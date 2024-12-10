using Core.Entities;
using Core.enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
namespace Core.Helpers;

public interface IUserHelper
{
    User GetUserFromToken();
}
public class UserHelper(IHttpContextAccessor accessor) : IUserHelper
{
    private IEnumerable<Claim> Claims { get => accessor.HttpContext.User.Identities.FirstOrDefault()?.Claims.Select(x => new Claim(x.Type.Split("/").LastOrDefault(), x.Value)); }

    public User GetUserFromToken()
    {

        var dto = new User
        {
            UserName = GetValue("userName"),
            Id = long.Parse(GetValue("userId")),
            Role = (EUser_Role)int.Parse(GetValue("role"))
        };

        return dto;
    }


    private string GetValue(string type)
        => Claims.FirstOrDefault(x => x.Type.Equals(type, StringComparison.CurrentCultureIgnoreCase))?.Value ?? "";
}