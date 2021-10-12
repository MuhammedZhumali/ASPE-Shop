using E_Shop.Models;
using E_Shop.Models.Entities;
using E_Shop.Models.Entities.OrderFolder;
using Microsoft.AspNetCore.Mvc;
using Questionnaire.Models.Entities.OrderFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Controllers.OrderControllerFolder
{
  [Route("api/[controller]")]
  public class OrderStockController : Controller
  {
    private readonly UsersContext _context;
    public OrderStockController(UsersContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrderStocks()
    {
      return Ok(await Task.Run(() => _context.OrderStocks.ToArray()));
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetOrderStocksById(int id)
    {
      OrderStock orderStock = await _context.OrderStocks.FindAsync(id);
      if (orderStock== null)
      {
        return NotFound();
      }
      else
      {
        return Ok(orderStock);
      }
    }

    [HttpPost]
    public async Task<IActionResult> PostOrderStocks([FromBody] OrderStock model)
    {
      _context.OrderStocks.Add(model);

      await _context.SaveChangesAsync();

      return Ok(model);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateOrderStocks([FromBody] OrderStock model)
    {
      _context.OrderStocks.Update(model);

      await _context.SaveChangesAsync();

      return Ok(model);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteOrderStock(int id)
    {
      OrderStock orderStock = _context.OrderStocks.Find(id);

      if (orderStock != null)
      {
        _context.OrderStocks.Remove(orderStock);

        await _context.SaveChangesAsync();

        return Ok(orderStock);
      }
      else
      {
        return NotFound();
      }
    }
  }

}
