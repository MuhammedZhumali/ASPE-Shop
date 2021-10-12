using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using E_Shop.Models;
using E_Shop.Models.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using E_Shop.Helpers;

namespace E_Shop.Controllers.Auth
{
  public class AccountController : Controller
  {
    private UsersContext db;
    public AccountController(UsersContext context)
    {
      db = context;
    }

    [HttpPost]
    [Route("login")]
    [EnableCors]
    public IActionResult Login([FromBody] User model)
    {
      User user = db.Users.FirstOrDefault(x => x.Email == model.Email);
      var identity = GetIdentity(model.Email, model.Password);
      if (identity == null)
      {
        return BadRequest(new { errorText = "Invalid email or password." });
      }
      if (model.Password != null)
        model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

      var now = DateTime.UtcNow;
      // создаем JWT-токен
      var jwt = new JwtSecurityToken(
              issuer: AuthOptions.ISSUER,
              audience: AuthOptions.AUDIENCE,
              notBefore: now,
              claims: identity.Claims,
              expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
              signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
      var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

      var response = new
      {
        access_token = encodedJwt,
        email = identity.Name,
        id = db.Users.FirstOrDefault(x => x.Email == user.Email).Id,
        firstName = db.Users.FirstOrDefault(x => x.FirstName == user.FirstName).FirstName,
        RoleId = db.Users.FirstOrDefault(x => x.RoleId == user.RoleId).RoleId
      };

      return Json(response);
    }

    private ClaimsIdentity GetIdentity(string email, string password)
    {
      var user = db.Users.FirstOrDefault(x => x.Email == email);
      if (BCrypt.Net.BCrypt.Verify(password, user.Password))
      {
        var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, db.Roles.FirstOrDefault(x => x.Id == user.RoleId).Name)
                };
        ClaimsIdentity claimsIdentity =
        new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);

        return claimsIdentity;
      }
      return null;
    }
    [HttpPost]
    [Route("register")]
    [EnableCors]
    public async Task<IActionResult> Register([FromBody] User model)
    {
      if (model == null)
      {
        return BadRequest(new { errorText = "No data has been sent" });
      }

      if (!db.Users.Select(x => x.Email).ToList().Contains(model.Email))
      {
        if (model.Password != null)
          model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);


        await db.Users.AddAsync(model);
        await db.SaveChangesAsync();
      }
      return Ok(model);
    }
  }
}
