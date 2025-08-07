
using DevOpsLogger.Api.Data;
using DevOpsLogger.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace DevOpsLogger.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IncidentsController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Incident>>> GetIncidents([FromQuery] string? status)
    {
        var query = context.Incidents.AsQueryable();

        if (!string.IsNullOrWhiteSpace(status))
        {
            query = query.Where(i => i.Status.ToLower() == status.ToLower());
        }

        var incidents = await query.OrderByDescending(i => i.CreatedAt).ToListAsync();

        return Ok(incidents);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Incident>> GetIncident(int id)
    {
        var incident = await context.Incidents.FindAsync(id);
        return incident == null ? NotFound() : Ok(incident);
    }

    [HttpPost]
    public async Task<ActionResult<Incident>> CreateIncident(Incident incident)
    {
        context.Incidents.Add(incident);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetIncident), new { id = incident.Id }, incident);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateIncident(int id, Incident updated)
    {
        if (id != updated.Id) return BadRequest();

        var incident = await context.Incidents.FindAsync(id);
        if (incident == null) return NotFound();

        incident.Title = updated.Title;
        incident.Description = updated.Description;
        incident.Severity = updated.Severity;
        incident.Status = updated.Status;

        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteIncident(int id)
    {
        var incident = await context.Incidents.FindAsync(id);
        if (incident == null) return NotFound();

        context.Incidents.Remove(incident);
        await context.SaveChangesAsync();
        return NoContent();
    }
}