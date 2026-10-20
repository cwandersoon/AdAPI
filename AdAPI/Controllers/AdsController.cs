using AdAPI.Data;
using AdAPI.Models;
using Microsoft.AspNetCore.JsonPatch.SystemTextJson;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AdsController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Ad>>> GetAll()
    {
        return await db.Ads.AsNoTracking().ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Ad>> GetById(int id)
    {
        var ad = await db.Ads.FindAsync(id);
        if (ad is null) return NotFound();
        return ad;
    }

    [HttpPost]
    public async Task<ActionResult<Ad>> Create(AdDto dto)
    {
        var ad = new Ad { CreatedAt = DateTime.UtcNow };
        CopyToEntity(dto, ad);

        db.Ads.Add(ad);
        await db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = ad.Id }, ad);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, AdDto dto)
    {
        var ad = await db.Ads.FindAsync(id);
        if (ad is null) return NotFound();

        CopyToEntity(dto, ad);
        await db.SaveChangesAsync();

        return NoContent();
    }

    [HttpPatch("{id:int}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> Patch(int id, JsonPatchDocument<AdDto> patch)
    {
        var ad = await db.Ads.FindAsync(id);
        if (ad is null) return NotFound();

        var dto = new AdDto
        {
            Title = ad.Title,
            Description = ad.Description,
            Price = ad.Price,
            ContactEmail = ad.ContactEmail
        };

        patch.ApplyTo(dto, error => ModelState.AddModelError(nameof(patch), error.ErrorMessage));

        if (!ModelState.IsValid || !TryValidateModel(dto))
        {
            return ValidationProblem(ModelState);
        }

        CopyToEntity(dto, ad);
        await db.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ad = await db.Ads.FindAsync(id);
        if (ad is null) return NotFound();

        db.Ads.Remove(ad);
        await db.SaveChangesAsync();

        return NoContent();
    }

    private static void CopyToEntity(AdDto dto, Ad ad)
    {
        ad.Title = dto.Title;
        ad.Description = dto.Description;
        ad.Price = dto.Price ?? 0;
        ad.ContactEmail = dto.ContactEmail;
    }
}
