using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using WorkLedger.Logging;

namespace WorkLedger;

public class WorkItemService
{
    private readonly IWorkItemRepository _repository;
    private readonly ILogger<WorkItemService> _logger;
    private readonly ILogStore _logStore;

    public WorkItemService(
        IWorkItemRepository repository,
        ILogger<WorkItemService> logger,
        ILogStore logStore)
    {
        _repository = repository;
        _logger = logger;
        _logStore = logStore;
    }

    public IEnumerable<WorkItem> ListItems()
    {
        const string message = "Listing work items.";
        _logger.LogDebug(message);
        _logStore.Append(message);
        return _repository.GetAll();
    }

    public WorkItem? GetItem(int id)
    {
        var message = $"Retrieving work item {id}.";
        _logger.LogDebug(message);
        _logStore.Append(message);
        return _repository.Get(id);
    }

    public void CreateItem(WorkItem item)
    {
        var message = $"Creating work item '{item.Title}'.";
        _logger.LogInformation(message);
        _logStore.Append(message);
        _repository.Add(item);
    }

    public void UpdateItem(WorkItem item)
    {
        var message = $"Updating work item {item.Id}.";
        _logger.LogInformation(message);
        _logStore.Append(message);
        _repository.Update(item);
    }

    public void DeleteItem(int id)
    {
        var message = $"Deleting work item {id}.";
        _logger.LogWarning(message);
        _logStore.Append(message);
        _repository.Delete(id);
    }
}
