using Microsoft.AspNetCore.Identity;

namespace Market.Models
{
  public class ApplicationUser : IdentityUser
  {
    public string Name { get; set; }
  }
}