using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WorkLedger.Pages.Items;

public class CreateModel : PageModel
{
    private readonly WorkItemService _service;

    [BindProperty]
    public WorkItem Item { get; set; } = new();

    public CreateModel(WorkItemService service)
    {
        _service = service;
    }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _service.CreateItem(Item);
        return RedirectToPage("Index");
    }
}
