using System;
using System.Collections.Generic;
using System.Linq;

namespace WorkLedger.Logging;

public class LogStore : ILogStore
{
    private readonly LinkedList<LogEntry> _entries = new();
    private readonly int _limit;
    private readonly object _sync = new();

    public LogStore(int limit = 64)
    {
        _limit = limit;
    }

    public void Append(string message)
    {
        var entry = new LogEntry(DateTime.UtcNow, message);
        lock (_sync)
        {
            _entries.AddFirst(entry);
            if (_entries.Count > _limit)
            {
                _entries.RemoveLast();
            }
        }
    }

    public IReadOnlyList<LogEntry> GetEntries()
    {
        lock (_sync)
        {
            return _entries.ToList();
        }
    }
}
