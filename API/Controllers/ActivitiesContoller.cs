using Application.Activities;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  public class ActivitiesController : BaseApiController
  {
    [HttpGet]
    public async Task<ActionResult<List<Activity>>> GetActivities()
    {
      return await Mediator.Send(new List.Query());
    }

    [HttpGet("{id}")] //api/activities/id
    public async Task<ActionResult<Activity>> GetActivity(Guid id)
    {
      return await Mediator.Send(new Details.Query { Id = id });
    }

    [HttpPost]
    public async Task<IActionResult> CreateActivity(Activity activity)
    {
      await Mediator.Send(new Create.Command {Activity = activity});
      return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditActivity(Guid Id, Activity activity) {
      activity.Id = Id;
      await Mediator.Send(new Edit.Command {Activity = activity});
      return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id) {
      var result = await Mediator.Send(new Delete.Command(id));
      if (!result) {
        return NotFound();
      }
      return Ok();
    }
  }
}