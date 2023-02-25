using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Market.Models
{
  public class User
  {
    public int UserId { get; set; }
    public string Name { get; set; }
    public List<UserPurchase> JoinEntities { get; }
    public ApplicationUser UserAccount { get; set; }
  }
}