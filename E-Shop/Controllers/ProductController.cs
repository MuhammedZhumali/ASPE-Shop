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
  public class ProductController : Controller
  {
    private readonly UsersContext _context;
    public ProductController(UsersContext context)
    {
      _context = context;
    }
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
      return Ok(await Task.Run(() => _context.Products.ToArray()));
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
      Product product = await _context.Products.FindAsync(id);
      if (product == null)
      {
        return NotFound();
      }
      else
      {
        return Ok(product);
      }
    }

    [HttpPost]
    public async Task<IActionResult> PostProducts([FromBody] Product model)
    {
      _context.Products.Add(model);

      await _context.SaveChangesAsync();

      return Ok(model);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProducts([FromBody] Product model)
    {
      _context.Products.Update(model);

      await _context.SaveChangesAsync();

      return Ok(model);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
      Product product = _context.Products.Find(id);

      if (product != null)
      {
        _context.Products.Remove(product);

        await _context.SaveChangesAsync();

        return Ok(product);
      }
      else
      {
        return NotFound();
      }
    }
  }
}
