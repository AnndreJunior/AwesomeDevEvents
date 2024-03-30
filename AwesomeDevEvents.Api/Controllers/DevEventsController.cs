using AutoMapper;
using AwesomeDevEvents.Api.Entities;
using AwesomeDevEvents.Api.Models;
using AwesomeDevEvents.Api.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AwesomeDevEvents.Api.Controllers;

[ApiController]
[Route("api/dev-events")]
public class DevEventsController : ControllerBase
{
    private readonly DevEventsDbContext _context;
    private readonly IMapper _mapper;

    public DevEventsController(DevEventsDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    /* describing controller in swagger */
    /// <summary>
    /// Get all events
    /// </summary>
    /// <returns>Events collection</returns>
    /// <response code="200">Success</response>
    [HttpGet]
    // informing expected response status code
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var devEvents = _context.DevEvents.Where(devEvent => !devEvent.IsDeleted);

        var viewModel = _mapper.Map<List<DevEventViewModel>>(devEvents);

        return Ok(viewModel);
    }

    /// <summary>
    /// Get an event with its id
    /// </summary>
    /// <param name="id">Event identifier(id)</param>
    /// <returns>Event data</returns>
    /// <response code="204">Success</response>
    /// <response code="404">Not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var devEvent = _context.DevEvents
            .Include(de => de.Speakers)
            .SingleOrDefault(devEvent => devEvent.Id == id);
        if (devEvent == null)
            return NotFound();
        var viewModel = _mapper.Map<DevEventViewModel>(devEvent);

        return Ok(viewModel);
    }

    /// <summary>
    /// Create an event
    /// </summary>
    /// <param name="request">Event body</param>
    /// <returns>New event</returns>
    /// <response code="201">Success</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult Post(DevEventInputModel request)
    {
        var devEventInput = _mapper.Map<DevEvent>(request);
        _context.DevEvents.Add(devEventInput);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = devEventInput.Id }, request);
    }

    /// <summary>
    /// Update event
    /// </summary>
    /// <param name="id">Event identifier(id)</param>
    /// <param name="request">Event request body</param>
    /// <returns>Anything</returns>
    /// <response code="204">Success</response>
    /// <response code="404">Not found</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Update(Guid id, DevEventInputModel request)
    {
        var devEvent = _context.DevEvents.SingleOrDefault(devEvent => devEvent.Id == id);
        if (devEvent == null)
            return NotFound();
        devEvent.Update(request.Title, request.Description, request.StartDate, request.EndDate);

        _context.DevEvents.Update(devEvent);
        _context.SaveChanges();

        return NoContent();
    }

    /// <summary>
    /// Delete event
    /// </summary>
    /// <param name="id">Event identifier</param>
    /// <returns>Nothing</returns>
    /// <response code="204">Success</response>
    /// <response code="404">Not found</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(Guid id)
    {
        var devEvent = _context.DevEvents.SingleOrDefault(devEvent => devEvent.Id == id);
        if (devEvent == null)
            return NotFound();
        devEvent.Delete();

        _context.SaveChanges();

        return NoContent();
    }

    /// <summary>
    /// Create event speaker
    /// </summary>
    /// <param name="id">Event identifier</param>
    /// <param name="request">event speaker request body</param>
    /// <returns>Nothing</returns>
    /// <response code="204">Success</response>
    /// <response code="404">Not found</response>
    [HttpPost("{id}/speakers")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult PostSpeaker(Guid id, DevEventSpeakerInputModel request)
    {
        var devEventSpeakerInput = _mapper.Map<DevEventSpeaker>(request);

        devEventSpeakerInput.DevEventId = id;
        var devEvent = _context.DevEvents.Any(devEvent => devEvent.Id == id);
        if (devEvent == false)
            return NotFound();
        _context.DevEventsSpeakers.Add(devEventSpeakerInput);
        _context.SaveChanges();

        return NoContent();
    }
}
