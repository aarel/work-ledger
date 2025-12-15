using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WorkLedger.Pages.Items;

public class EditModel : PageModel
{
    private readonly WorkItemService _service;

    [BindProperty]
    public WorkItem Item { get; set; } = new();

    public EditModel(WorkItemService service)
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
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _service.UpdateItem(Item);
        return RedirectToPage("Index");
    }
}
