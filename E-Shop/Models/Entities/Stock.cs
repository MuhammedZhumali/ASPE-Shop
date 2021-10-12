using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Models.Entities
{
  public class Stock
  {

    public int Id { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
  }
}
