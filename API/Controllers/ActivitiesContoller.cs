using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
  public class ActivitiesController : BaseApiController
  {
    private readonly DataContext _context;
    public ActivitiesController(DataContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Activity>>> GetActivities()
    {
      var activities = await _context.Activities.ToListAsync();
      return Ok(activities);
    }

    [HttpGet("{id}")] //api/activities/id
    public async Task<ActionResult<Activity>> GetActivity(Guid id) {
      return await _context.Activities.FindAsync(id);
    }
  }
}