using System.ComponentModel.DataAnnotations;

namespace AdAPI.Models;

public class AdDto
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(2000)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Range(0, 10_000_000)]
    public decimal? Price { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(200)]
    public string ContactEmail { get; set; } = string.Empty;
}
