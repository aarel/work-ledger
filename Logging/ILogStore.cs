using System.Collections.Generic;

namespace WorkLedger.Logging;

public interface ILogStore
{
    void Append(string message);
    IReadOnlyList<LogEntry> GetEntries();
}
