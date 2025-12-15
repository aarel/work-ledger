using System;
using System.ComponentModel.DataAnnotations;

namespace WorkLedger;

public class WorkItem
{
    public int Id { get; set; }

    [Required, StringLength(80)]
    public string Title { get; set; } = string.Empty;

    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    public DateTime Created { get; set; } = DateTime.UtcNow;
}
