using System.Collections.Generic;
using System.Linq;

namespace WorkLedger;

public class InMemoryWorkItemRepository : IWorkItemRepository
{
    private readonly List<WorkItem> _items = new();
    private int _nextId = 1;

    public IEnumerable<WorkItem> GetAll() => _items;

    public WorkItem? Get(int id) => _items.FirstOrDefault(i => i.Id == id);

    public void Add(WorkItem item)
    {
        item.Id = _nextId++;
        _items.Add(item);
    }

    public void Update(WorkItem item)
    {
        var existing = Get(item.Id);
        if (existing == null) return;

        existing.Title = item.Title;
        existing.Description = item.Description;
    }

    public void Delete(int id)
    {
        var existing = Get(id);
        if (existing != null)
            _items.Remove(existing);
    }
}
