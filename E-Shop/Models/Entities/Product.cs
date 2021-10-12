using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Models.Entities
{
  public class Product
  {
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public string Image { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public int BrandId { get; set; }
    public Brand Brand { get; set; }
    public ICollection<Stock> Stock { get; set; }
  }
}
