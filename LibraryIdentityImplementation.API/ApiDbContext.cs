using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace LibraryIdentityImplementation.API
{
    public class ApiDbContext : IdentityDbContext<IdentityUser>
    {

        public ApiDbContext(DbContextOptions<ApiDbContext> options)
   : base(options) { }

    }



}

