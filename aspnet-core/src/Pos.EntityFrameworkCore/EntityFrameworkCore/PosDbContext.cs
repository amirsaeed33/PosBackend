using Abp.Zero.EntityFrameworkCore;
using Pos.Authorization.Roles;
using Pos.Authorization.Users;
using Pos.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace Pos.EntityFrameworkCore;

public class PosDbContext : AbpZeroDbContext<Tenant, Role, User, PosDbContext>
{
    /* Define a DbSet for each entity of the application */

    public PosDbContext(DbContextOptions<PosDbContext> options)
        : base(options)
    {
    }
}
