using CricApp.DAL.Data;
using CricApp.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CricApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamsController : ControllerBase
{
    private readonly CricketDbContext _context;

    public TeamsController(CricketDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
    {
        return await _context.Teams
            .Include(t => t.Players)
            .ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Team>> GetTeam(int id)
    {
        var team = await _context.Teams
            .Include(t => t.Players)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (team is null)
        {
            return NotFound();
        }

        return team;
    }

    [HttpPost]
    public async Task<ActionResult<Team>> CreateTeam(Team team)
    {
        _context.Teams.Add(team);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTeam), new { id = team.Id }, team);
    }
}
