using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AdAPI.Models;

public class Ad
{
    public int Id { get; set; }

    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string Description { get; set; } = string.Empty;

    [Precision(18, 2)]
    public decimal Price { get; set; }

    [MaxLength(200)]
    public string ContactEmail { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}
