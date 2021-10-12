using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Models.Entities
{
  public class CartProduct
  {
    [Key]
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public Product Product { get; set; }
    public int StockId { get; set; }
    public Stock Stock { get; set; }
    public int Qty { get; set; }
    public decimal Value { get; set; }
  }
}
