using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Users.Domain.Users;

namespace Users.Infrastructure.Identity;

public class UsersIdentityDbContext : IdentityDbContext<User>
{
    public UsersIdentityDbContext(DbContextOptions<UsersIdentityDbContext> options)
        : base(options)
    {

    }
}
