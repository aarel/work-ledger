using System;

namespace WorkLedger.Logging;

public record LogEntry(DateTime Timestamp, string Message);
