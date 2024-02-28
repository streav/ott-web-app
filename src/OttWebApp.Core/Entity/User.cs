using Microsoft.AspNetCore.Identity;

namespace OttWebApp.Core.Entity;

public class User : IdentityUser<int>
{
    public int? SubscriberId { get; set; }
}