usingusing System.Collections.Generic;
using System;
using Newtonsoft.Json;
using Market.Models;

namespace Market.Models
{
  public class Product
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
  }
}