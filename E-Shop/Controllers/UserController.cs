using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using E_Shop.Models.Entities;
using E_Shop.Models;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace E_Shop.Controllers
{
  [Route("api/[controller]")]
  public class UsersController : Controller
  {
    IConfigurationRoot configuration = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .Build();

    private readonly UsersContext _context;
    public UsersController(UsersContext context)
    {
      _context = context;
    }
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
      return Ok(await Task.Run(() => _context.Users.ToArray()));
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
      User user = await _context.Users.FindAsync(id);
      if (user == null)
      {
        return NotFound();
      }
      else
      {
        return Ok(user);
      }
    }

    [HttpPost]
    public async Task<IActionResult> PostUsers([FromBody] User model)
    {
      if (model.Password != null)
        model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

      _context.Users.Add(model);

      await _context.SaveChangesAsync();

      return Ok(model);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUsers([FromBody] User model)
    {
      if (model.Password == null)
      {
        User user = _context.Users.Find(model.Id);
        model.Password = user.Password;
      }

      _context.Users.Update(model);

      await _context.SaveChangesAsync();

      return Ok(model);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
      User user = _context.Users.Find(id);
      if (user != null)
      {
        _context.Users.Remove(user);

        await _context.SaveChangesAsync();

        return Ok(user);
      }
      else
      {
        return NotFound();
      }
    }
  }
}

