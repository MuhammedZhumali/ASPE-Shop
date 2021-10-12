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
  public class CartProductController : Controller
  {
    private readonly UsersContext _context;
    public CartProductController(UsersContext context)
    {
      _context = context;
    }
    [HttpGet]
    public async Task<IActionResult> GetCartProducts()
    {
      return Ok(await Task.Run(() => _context.CartProducts.ToArray()));
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetCartProductById(int id)
    {
      CartProduct cartProduct = await _context.CartProducts.FindAsync(id);
      if (cartProduct == null)
      {
        return NotFound();
      }
      else
      {
        return Ok(cartProduct);
      }
    }

    [HttpPost]
    public async Task<IActionResult> PostCartProducts([FromBody] CartProduct model)
    {
      _context.CartProducts.Add(model);

      await _context.SaveChangesAsync();

      return Ok(model);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCartProducts([FromBody] CartProduct model)
    {
      _context.CartProducts.Update(model);

      await _context.SaveChangesAsync();

      return Ok(model);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteCartProduct(int id)
    {
      CartProduct cartProduct = _context.CartProducts.Find(id);

      if (cartProduct != null)
      {
        _context.CartProducts.Remove(cartProduct);

        await _context.SaveChangesAsync();

        return Ok(cartProduct);
      }
      else
      {
        return NotFound();
      }
    }
  }

}
