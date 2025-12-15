using Microsoft.EntityFrameworkCore;

namespace WorkLedger.Data;

public class WorkLedgerDbContext : DbContext
{
    public WorkLedgerDbContext(DbContextOptions<WorkLedgerDbContext> options)
        : base(options)
    {
    }

    public DbSet<WorkItem> WorkItems => Set<WorkItem>();
}
