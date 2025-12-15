using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WorkLedger.Logging;

namespace WorkLedger.Pages;

public class ObservabilityModel : PageModel
{
    private readonly ILogStore _logStore;

    public IReadOnlyList<LogEntry> Entries { get; private set; } = Array.Empty<LogEntry>();

    public ObservabilityModel(ILogStore logStore)
    {
        _logStore = logStore;
    }

    public void OnGet()
    {
        Entries = _logStore.GetEntries();
    }
}
