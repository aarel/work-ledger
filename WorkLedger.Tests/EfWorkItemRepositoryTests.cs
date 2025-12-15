using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using WorkLedger.Data;
using Xunit;

namespace WorkLedger.Tests;

public class EfWorkItemRepositoryTests
{
    private static EfWorkItemRepository CreateRepository(SqliteConnection connection)
    {
        var options = new DbContextOptionsBuilder<WorkLedgerDbContext>()
            .UseSqlite(connection)
            .Options;
        var context = new WorkLedgerDbContext(options);
        context.Database.EnsureCreated();
        return new EfWorkItemRepository(context);
    }

    [Fact]
    public void Add_PersistsToDatabase()
    {
        using var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();
        var repository = CreateRepository(connection);
        var item = new WorkItem { Title = "Persisted", Description = "Stored via EF" };

        repository.Add(item);

        var stored = repository.Get(item.Id);

        Assert.NotNull(stored);
        Assert.Equal("Persisted", stored!.Title);
        Assert.Equal("Stored via EF", stored.Description);
    }

    [Fact]
    public void Update_SyncsChanges()
    {
        using var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();
        var repository = CreateRepository(connection);
        var item = new WorkItem { Title = "Draft", Description = "Before update" };

        repository.Add(item);
        item.Title = "Final";
        item.Description = "After update";

        repository.Update(item);

        var stored = repository.Get(item.Id);

        Assert.NotNull(stored);
        Assert.Equal("Final", stored!.Title);
        Assert.Equal("After update", stored.Description);
    }

    [Fact]
    public void Delete_RemovesFromDatabase()
    {
        using var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();
        var repository = CreateRepository(connection);
        var item = new WorkItem { Title = "Temporary", Description = "Will be deleted" };

        repository.Add(item);
        repository.Delete(item.Id);

        var stored = repository.Get(item.Id);

        Assert.Null(stored);
    }
}
