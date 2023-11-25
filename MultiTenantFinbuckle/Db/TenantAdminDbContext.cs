using Finbuckle.MultiTenant.Stores;
using Microsoft.EntityFrameworkCore;
using MulltiTenantFinbuckle.Models;

namespace MulltiTenantFinbuckle.Db;

public class TenantAdminDbContext : EFCoreStoreDbContext<MultiTenantInfo>
{
    public TenantAdminDbContext(DbContextOptions<TenantAdminDbContext> options) : base(options)
    {
    }
}