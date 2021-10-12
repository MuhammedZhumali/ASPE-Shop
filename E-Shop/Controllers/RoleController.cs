using E_Shop.Models;
using E_Shop.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Controllers
{
  [Route("api/[controller]")]
  public class RoleController : Controller
  {
    private readonly UsersContext _context;
    public RoleController(UsersContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetRoles()
    {
      return Ok(await Task.Run(() => _context.Roles.ToArray()));
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetRoleById(int id)
    {
      Role role = await _context.Roles.FindAsync(id);
      if (role == null)
      {
        return NotFound();
      }
      else
      {
        return Ok(role);
      }
    }

    [HttpPost]
    public async Task<IActionResult> PostRoles([FromBody] Role model)
    {
      _context.Roles.Add(model);

      await _context.SaveChangesAsync();

      return Ok(model);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateRoles([FromBody] Role model)
    {
      _context.Roles.Update(model);

      await _context.SaveChangesAsync();

      return Ok(model);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteRole(int id)
    {
      Role role = _context.Roles.Find(id);

      if (role != null)
      {
        _context.Roles.Remove(role);

        await _context.SaveChangesAsync();

        return Ok(role);
      }
      else
      {
        return NotFound();
      }
    }
  }
}
