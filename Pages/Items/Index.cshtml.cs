using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace WorkLedger.Pages.Items;

public class IndexModel : PageModel
{
    private readonly WorkItemService _service;

    public IndexModel(WorkItemService service)
    {
        _service = service;
    }

    public IEnumerable<WorkItem> Items { get; private set; } = Enumerable.Empty<WorkItem>();

    public void OnGet()
    {
        Items = _service.ListItems();
    }
}
