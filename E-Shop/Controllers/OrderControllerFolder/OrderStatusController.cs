using E_Shop.Models;
using E_Shop.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Questionnaire.Models.Entities.OrderFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Controllers.OrderControllerFolder
{
  [Route("api/[controller]")]
  public class OrderStatusController : Controller
  {
    private readonly UsersContext _context;
    public OrderStatusController(UsersContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrderStatuses()
    {
      return Ok(await Task.Run(() => _context.OrderStatuses.ToArray()));
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetOrderStatusById(int id)
    {
      OrderStatus orderStatus = await _context.OrderStatuses.FindAsync(id);
      if (orderStatus == null)
      {
        return NotFound();
      }
      else
      {
        return Ok(orderStatus);
      }
    }

    [HttpPost]
    public async Task<IActionResult> PostOrderStatuses([FromBody] OrderStatus model)
    {
      _context.OrderStatuses.Add(model);

      await _context.SaveChangesAsync();

      return Ok(model);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateOrderStatuses([FromBody] OrderStatus model)
    {
      _context.OrderStatuses.Update(model);

      await _context.SaveChangesAsync();

      return Ok(model);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteOrderStatus(int id)
    {
      OrderStatus orderStatus = _context.OrderStatuses.Find(id);

      if (orderStatus != null)
      {
        _context.OrderStatuses.Remove(orderStatus);

        await _context.SaveChangesAsync();

        return Ok(orderStatus);
      }
      else
      {
        return NotFound();
      }
    }
  }

}
