using CricApp.DAL.Data;
using CricApp.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CricApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MatchesController : ControllerBase
{
    private readonly CricketDbContext _context;

    public MatchesController(CricketDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Match>>> GetMatches()
    {
        return await _context.Matches
            .Include(m => m.TeamOne)
            .Include(m => m.TeamTwo)
            .Include(m => m.Innings)
            .ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Match>> GetMatch(int id)
    {
        var match = await _context.Matches
            .Include(m => m.TeamOne)
            .Include(m => m.TeamTwo)
            .Include(m => m.Innings)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (match is null)
        {
            return NotFound();
        }

        return match;
    }

    [HttpPost]
    public async Task<ActionResult<Match>> CreateMatch(Match match)
    {
        _context.Matches.Add(match);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetMatch), new { id = match.Id }, match);
    }
}
