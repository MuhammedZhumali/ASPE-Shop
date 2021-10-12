using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Models.Entities
{
  public class Category
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Brand> Brands { get; set; }
    public List<Product> Products { get; set; }
  }
}
