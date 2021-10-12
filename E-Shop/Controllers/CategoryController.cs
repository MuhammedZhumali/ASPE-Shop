using E_Shop.Models;
using E_Shop.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Controllers
{
  [Route("api/[controller]")]
  public class CategoryController : Controller
  {
    private readonly UsersContext _context;
    public CategoryController(UsersContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
      return Ok(await Task.Run(() => _context.Categories.ToArray()));
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
      Category category = await _context.Categories.FindAsync(id);
      if (category == null)
      {
        return NotFound();
      }
      else
      {
        return Ok(category);
      }
    }

    [HttpPost]
    public async Task<IActionResult> PostCategories([FromBody] Category model)
    {
      _context.Categories.Add(model);

      await _context.SaveChangesAsync();

      return Ok(model);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCategories([FromBody] Category model)
    {
      _context.Categories.Update(model);

      await _context.SaveChangesAsync();

      return Ok(model);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
      Category category = _context.Categories.Include(b => b.Brands).FirstOrDefault(b=>b.Id==id);

      if (category != null)
      {
        _context.Categories.Remove(category);

        await _context.SaveChangesAsync();

        return Ok(category);
      }
      else
      {
        return NotFound();
      }
    }
  }
}
