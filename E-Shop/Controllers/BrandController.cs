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
  public class BrandController : Controller
  {
    private readonly UsersContext _context;
    public BrandController(UsersContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetBrands()
    {
      return Ok(await Task.Run(() => _context.Brands.ToArray()));
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetBrandById(int id)
    {
      Brand brand = await _context.Brands.FindAsync(id);
      if (brand == null)
      {
        return NotFound();
      }
      else
      {
        return Ok(brand);
      }
    }

    [HttpPost]
    public async Task<IActionResult> PostBrands([FromBody] Brand model)
    {
      _context.Brands.Add(model);

      await _context.SaveChangesAsync();

      return Ok(model);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBrands([FromBody] Brand model)
    {
      _context.Brands.Update(model);

      await _context.SaveChangesAsync();

      return Ok(model);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteBrand(int id)
    {
      Brand brand = _context.Brands.Find(id);

      if (brand != null)
      {
        _context.Brands.Remove(brand);

        await _context.SaveChangesAsync();

        return Ok(brand);
      }
      else
      {
        return NotFound();
      }
    }
  }
}
