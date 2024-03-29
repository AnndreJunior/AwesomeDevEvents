using AwesomeDevEvents.Api.Entities;
using AwesomeDevEvents.Api.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AwesomeDevEvents.Api.Controllers;

[ApiController]
[Route("api/dev-events")]
public class DevEventsController : ControllerBase
{
    private readonly DevEventsDbContext _context;

    public DevEventsController(DevEventsDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var devEvents = _context.DevEvents.Where(devEvent => !devEvent.IsDeleted);

        return Ok(devEvents);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var devEvent = _context.DevEvents
            .Include(de => de.Speakers)
            .SingleOrDefault(devEvent => devEvent.Id == id);
        if (devEvent == null)
            return NotFound();

        return Ok(devEvent);
    }

    [HttpPost]
    public IActionResult Post(DevEvent request)
    {
        _context.DevEvents.Add(request);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = request.Id }, request);
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, DevEvent request)
    {
        var devEvent = _context.DevEvents.SingleOrDefault(devEvent => devEvent.Id == id);
        if (devEvent == null)
            return NotFound();
        devEvent.Update(request.Title, request.Description, request.StartDate, request.EndDate);

        _context.DevEvents.Update(devEvent);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var devEvent = _context.DevEvents.SingleOrDefault(devEvent => devEvent.Id == id);
        if (devEvent == null)
            return NotFound();
        devEvent.Delete();

        _context.SaveChanges();

        return NoContent();
    }

    [HttpPost("{id}/speakers")]
    public IActionResult PostSpeaker(Guid id, DevEventSpeaker request)
    {
        request.DevEventId = id;
        var devEvent = _context.DevEvents.Any(devEvent => devEvent.Id == id);
        if (devEvent == false)
            return NotFound();
        _context.DevEventsSpeakers.Add(request);
        _context.SaveChanges();

        return NoContent();
    }
}
