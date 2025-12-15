using System.Collections.Generic;

namespace WorkLedger;

public interface IWorkItemRepository
{
    IEnumerable<WorkItem> GetAll();
    WorkItem? Get(int id);
    void Add(WorkItem item);
    void Update(WorkItem item);
    void Delete(int id);
}
