using Microsoft.AspNetCore.Identity;

namespace PostalZipService.Services.Identity
{
    // Note: The advantage of inheriting from framework classes is that you can add your own 
    // custom properties(e.g.Age in my case) to user entity.Also by inheriting database context, you could modify the database schema, if required.
    public class AppIdentityUser : IdentityUser
    {
       
    }
}
