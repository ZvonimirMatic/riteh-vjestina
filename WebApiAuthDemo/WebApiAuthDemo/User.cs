using Microsoft.AspNetCore.Identity;

namespace WebApiAuthDemo;

public class User : IdentityUser
{
    public DateTime? Birthday { get; set; }
}
