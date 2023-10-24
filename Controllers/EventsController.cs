﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using paroquiaRussas.Models;
using paroquiaRussas.Repository;
using paroquiaRussas.Utility;
using paroquiaRussas.Utility.Resources;
using System.Security.Cryptography;

namespace paroquiaRussas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public EventsController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [Route("View")]
        public IActionResult Index()
        {
            List<Event> eventList = GetAllEvents();
            eventList.Sort((e1, e2) => e2.EventDate.CompareTo(e1.EventDate));

            return View(eventList);
        }

        [HttpGet]
        public List<Event> GetAllEvents()
        {
            EventsRepository eventsRepository = new EventsRepository(_appDbContext);
            List<Event> events = eventsRepository.GetEvents();

            return events;
        }

        [HttpGet("{id}")]
        public ActionResult<Event> GetEventById(int id)
        {
            try
            {
                EventsRepository eventsRepository = new EventsRepository(_appDbContext);
                Event eventToGet = eventsRepository.GetEventById(id);

                if (eventToGet == null)
                    return NotFound();

                return eventsRepository.GetEventById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(Exceptions.EXC02, ex);
            }
        }

        [HttpGet("GetEventByDate/{date}")]
        public List<Event> GetEventByDate(string date)
        {
            try
            {
                EventsRepository eventsRepository = new EventsRepository(_appDbContext);

                List<Event> eventsToReturn = eventsRepository.GetEventsByDate(date);

                if (eventsToReturn == null)
                    return new List<Event>();

                return eventsToReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(Exceptions.EXC03, ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent([FromForm]  Event eventToPost)
        {
            try
            {
                EventsRepository eventsRepository = new EventsRepository(_appDbContext);
                var result = eventsRepository.CreateNewEvent(eventToPost);

                await _appDbContext.SaveChangesAsync();

                TempData["SucessMessage"] = Messages.MSG01;
                return RedirectToAction("Index", "Admin");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = Exceptions.EXC04;
                return RedirectToAction("Index", "Admin");
            }
        }


        [HttpPut]
        public IActionResult EditEvent(Event eventUpdate)
        {
            try
            {
                EventsRepository eventRepository = new EventsRepository(_appDbContext);
                Event eventToEdit = eventRepository.UpdateEvent(eventUpdate);

                if (eventToEdit == null)
                    return NotFound();

                _appDbContext.SaveChanges();

                return Ok(Messages.MSG02);
            }
            catch (Exception ex)
            {
                throw new Exception(Exceptions.EXC05, ex);
            }
        }

        [HttpDelete("DeleteEvent/{id}")]
        public IActionResult DeleteEvent(int id)
        {
            try
            {
                EventsRepository eventsRepository = new EventsRepository(_appDbContext);

                Event eventToDelete = eventsRepository.DeleteEventById(id);

                if (eventToDelete == null)
                    return Json(new { error = Exceptions.EXC06 });

                _appDbContext.SaveChangesAsync();

                return Json(new { message = Messages.MSG03 });
            }
            catch (Exception ex)
            {
                return Json(new { error = Exceptions.EXC06 });
            }
        }
    }
}