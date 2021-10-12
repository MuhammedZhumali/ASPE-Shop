using E_Shop.Models;
using E_Shop.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Controllers.OrderControllerFolder
{
  [Route("api/[controller]")]
  public class OrderController : Controller
  {
    private readonly UsersContext _context;
    public OrderController(UsersContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
      return Ok(await Task.Run(() => _context.Orders.ToArray()));
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
      Order order = await _context.Orders.FindAsync(id);
      if (order == null)
      {
        return NotFound();
      }
      else
      {
        return Ok(order);
      }
    }

    [HttpPost]
    public async Task<IActionResult> PostOrders([FromBody] Order model)
    {
      _context.Orders.Add(model);

      await _context.SaveChangesAsync();

      return Ok(model);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateOrders([FromBody] Order model)
    {
      _context.Orders.Update(model);

      await _context.SaveChangesAsync();

      return Ok(model);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
      Order order = _context.Orders.Find(id);

      if (order != null)
      {
        _context.Orders.Remove(order);

        await _context.SaveChangesAsync();

        return Ok(order);
      }
      else
      {
        return NotFound();
      }
    }
  }

}
