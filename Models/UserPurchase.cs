using System.Collections.Generic;
using System;

// I think in the end this won't be useful. I can just log the completed sale
// on its own table without creating a join to product
// will I need multiple product links? 
namespace Market.Models
{
  public class UserPurchase
  {
    public int UserPurchaseId { get; set; }
    public int PurchaseId { get; set; }
    public Product Product { get; set; }
    public User User { get; set; }
  }
}