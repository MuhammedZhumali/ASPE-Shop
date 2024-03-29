﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Models.Entities
{
  public class Role
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<User> Users { get; set; }
  }
}
