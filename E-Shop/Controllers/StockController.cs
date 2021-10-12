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
  public class StockController : Controller
  {
    private readonly UsersContext _context;
    public StockController(UsersContext context)
    {
      _context = context;
    }
    [HttpGet]
    public async Task<IActionResult> GetStocks()
    {
      return Ok(await Task.Run(() => _context.Stock.ToArray()));
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetStockById(int id)
    {
      Stock stock = await _context.Stock.FindAsync(id);
      if (stock == null)
      {
        return NotFound();
      }
      else
      {
        return Ok(stock);
      }
    }

    [HttpPost]
    public async Task<IActionResult> PostStocks([FromBody] Stock model)
    {
      _context.Stock.Add(model);

      await _context.SaveChangesAsync();

      return Ok(model);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateStocks([FromBody] Stock model)
    {
      _context.Stock.Update(model);

      await _context.SaveChangesAsync();

      return Ok(model);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteStock(int id)
    {
      Stock stock = _context.Stock.Find(id);

      if (stock != null)
      {
        _context.Stock.Remove(stock);

        await _context.SaveChangesAsync();

        return Ok(stock);
      }
      else
      {
        return NotFound();
      }
    }
  }
}
