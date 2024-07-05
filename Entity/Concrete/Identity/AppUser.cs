using Entity.Abstract;
using Microsoft.AspNetCore.Identity;

namespace Entity.Concrete.Identity
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
