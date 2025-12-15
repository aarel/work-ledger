using Microsoft.EntityFrameworkCore;
using WorkLedger;
using WorkLedger.Data;
using WorkLedger.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddDbContext<WorkLedgerDbContext>(options =>
    options.UseSqlite("Data Source=workledger.db"));
builder.Services.AddScoped<IWorkItemRepository, EfWorkItemRepository>();
builder.Services.AddSingleton<ILogStore, LogStore>();
builder.Services.AddScoped<WorkItemService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<WorkLedgerDbContext>();
    db.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

app.MapGet("/api/items", (WorkItemService service) => Results.Ok(service.ListItems()));

app.MapGet("/api/items/{id:int}", (WorkItemService service, int id) =>
{
    var item = service.GetItem(id);
    return item is null ? Results.NotFound() : Results.Ok(item);
});

app.MapPost("/api/items", (WorkItemService service, WorkItem item) =>
{
    service.CreateItem(item);
    return Results.Created($"/api/items/{item.Id}", item);
});

app.MapPut("/api/items/{id:int}", (WorkItemService service, int id, WorkItem payload) =>
{
    var existing = service.GetItem(id);
    if (existing is null)
    {
        return Results.NotFound();
    }

    payload.Id = id;
    service.UpdateItem(payload);
    return Results.NoContent();
});

app.MapDelete("/api/items/{id:int}", (WorkItemService service, int id) =>
{
    service.DeleteItem(id);
    return Results.NoContent();
});

app.MapGet("/favicon.ico", () => Results.NoContent());
app.Run();
