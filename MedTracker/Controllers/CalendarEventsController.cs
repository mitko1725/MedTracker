using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedTracker.Data;
using MedTracker.Models;
using MedTracker.Services.Interfaces;
using MedTracker.Services.Models.CalendarServiceModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarEventsController : ControllerBase
    {
        
        private readonly ICalendarService _calendar;

        public CalendarEventsController( ICalendarService calendar)
        {
           
            _calendar = calendar;
        }

        // GET: api/CalendarEvents
        [HttpGet]
        public async Task<IEnumerable<AppointmentDetailsServiceModels>> GetEvents([FromQuery] DateTime StartDate, [FromQuery] DateTime EndDate, [FromQuery] int DoctorId)
        {
            //should add doctor Id aswell
            var myList = await _calendar.GetCurrentDoctorAppointmentsForThisWeek(StartDate, EndDate, DoctorId);
            return myList;
        }

        // GET: api/CalendarEvents/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<CalendarEvent>> GetCalendarEvent(int id)
        //{
        //    var calendarEvent = await _context.Events.FindAsync(id);

        //    if (calendarEvent == null)
        //    {
        //        return NotFound();
        //    }

        //    return calendarEvent;
        //}

        //// PUT: api/CalendarEvents/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCalendarEvent(int id, CalendarEvent calendarEvent)
        //{
        //    if (id != calendarEvent.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(calendarEvent).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CalendarEventExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/CalendarEvents
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<CalendarEvent>> PostCalendarEvent(CalendarEvent calendarEvent)
        //{
        //    _context.Events.Add(calendarEvent);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetCalendarEvent", new { id = calendarEvent.Id }, calendarEvent);
        //}

        //// DELETE: api/CalendarEvents/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCalendarEvent(int id)
        //{
        //    var calendarEvent = await _context.Events.FindAsync(id);
        //    if (calendarEvent == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Events.Remove(calendarEvent);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool CalendarEventExists(int id)
        //{
        //    return _context.Events.Any(e => e.Id == id);
        //}
    }
}
