using Microsoft.EntityFrameworkCore;

namespace WorkLedger.Data;

public class EfWorkItemRepository : IWorkItemRepository
{
    private readonly WorkLedgerDbContext _context;

    public EfWorkItemRepository(WorkLedgerDbContext context)
    {
        _context = context;
    }

    public IEnumerable<WorkItem> GetAll() => _context.WorkItems.AsNoTracking().ToList();

    public WorkItem? Get(int id) => _context.WorkItems.Find(id);

    public void Add(WorkItem item)
    {
        _context.WorkItems.Add(item);
        _context.SaveChanges();
    }

    public void Update(WorkItem item)
    {
        _context.WorkItems.Update(item);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var existing = _context.WorkItems.Find(id);
        if (existing != null)
        {
            _context.WorkItems.Remove(existing);
            _context.SaveChanges();
        }
    }
}
