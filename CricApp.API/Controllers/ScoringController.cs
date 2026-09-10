using CricApp.DAL.Data;
using CricApp.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CricApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScoringController : ControllerBase
{
    private readonly CricketDbContext _context;

    public ScoringController(CricketDbContext context)
    {
        _context = context;
    }

    [HttpGet("live/{matchId:int}")]
    public async Task<ActionResult<IEnumerable<Innings>>> GetLiveScore(int matchId)
    {
        var innings = await _context.Innings
            .Where(i => i.MatchId == matchId)
            .Include(i => i.Balls)
            .ToListAsync();

        return innings;
    }

    [HttpPost("record-ball")]
    public async Task<IActionResult> RecordBall([FromBody] Ball ball)
    {
        _context.Balls.Add(ball);
        await _context.SaveChangesAsync();

        return Ok(ball);
    }
}
