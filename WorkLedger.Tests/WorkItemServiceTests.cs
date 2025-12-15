using Microsoft.Extensions.Logging.Abstractions;
using WorkLedger;
using WorkLedger.Logging;
using Xunit;

namespace WorkLedger.Tests;

public class WorkItemServiceTests
{
    [Fact]
    public void CreateItem_AddsNewEntry()
    {
        var repository = new InMemoryWorkItemRepository();
        var service = new WorkItemService(repository, NullLogger<WorkItemService>.Instance, new LogStore());

        service.CreateItem(new WorkItem
        {
            Title = "Review",
            Description = "Inspect how the flow works"
        });

        var items = service.ListItems().ToList();

        Assert.Single(items);
        Assert.Equal("Review", items[0].Title);
    }

    [Fact]
    public void UpdateItem_PersistsChanges()
    {
        var repository = new InMemoryWorkItemRepository();
        var service = new WorkItemService(repository, NullLogger<WorkItemService>.Instance, new LogStore());
        var item = new WorkItem
        {
            Title = "Draft",
            Description = "Initial description"
        };

        service.CreateItem(item);
        item.Title = "Published";
        item.Description = "Updated text";

        service.UpdateItem(item);

        var updated = service.GetItem(item.Id);

        Assert.NotNull(updated);
        Assert.Equal("Published", updated!.Title);
        Assert.Equal("Updated text", updated.Description);
    }

    [Fact]
    public void DeleteItem_RemovesEntry()
    {
        var repository = new InMemoryWorkItemRepository();
        var service = new WorkItemService(repository, NullLogger<WorkItemService>.Instance, new LogStore());
        var item = new WorkItem
        {
            Title = "Obsolete",
            Description = "This item will be deleted"
        };

        service.CreateItem(item);
        service.DeleteItem(item.Id);

        var remaining = service.ListItems();

        Assert.Empty(remaining);
    }
}
