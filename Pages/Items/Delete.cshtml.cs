using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WorkLedger.Pages.Items;

public class DeleteModel : PageModel
{
    private readonly WorkItemService _service;

    [BindProperty]
    public WorkItem Item { get; set; } = new();

    public DeleteModel(WorkItemService service)
    {
        _service = service;
    }

    public IActionResult OnGet(int id)
    {
        var existing = _service.GetItem(id);
        if (existing == null)
        {
            return NotFound();
        }

        Item = existing;
        return Page();
    }

    public IActionResult OnPost()
    {
        _service.DeleteItem(Item.Id);
        return RedirectToPage("Index");
    }
}
