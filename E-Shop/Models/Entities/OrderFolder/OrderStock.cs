﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Models.Entities.OrderFolder
{
  public class OrderStock
  {
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public int StockId { get; set; }
    public Stock Stock { get; set; }
    public int Qty { get; set; }
  }
}
