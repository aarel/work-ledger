using WorkLedger.Logging;
using Xunit;

namespace WorkLedger.Tests;

public class LogStoreTests
{
    [Fact]
    public void Append_StoresRecentEntries()
    {
        var store = new LogStore(2);

        store.Append("one");
        store.Append("two");
        store.Append("three");

        var entries = store.GetEntries();

        Assert.Equal(2, entries.Count);
        Assert.Equal("three", entries[0].Message);
        Assert.Equal("two", entries[1].Message);
    }
}
