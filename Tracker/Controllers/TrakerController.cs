﻿using Microsoft.AspNetCore.Mvc;
using Tracker.Data;
using Tracker.Dtos;
using Tracker.Helpers;
using Tracker.Models;

namespace Tracker.Controllers
{
    [Route("api/tracker")]
    [ApiController]
    public class TrakerController : Controller
    {
        private readonly ITrackerActionRepository _repository;
        private readonly JwtService _jwtService;

        public TrakerController(ITrackerActionRepository repository, JwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }

        [HttpPost("action")]
        public IActionResult CreateAction(CreateActionDto dto)
        {
            try
            {
                var jwt = Request.Cookies["token"];

                var token = _jwtService.Verify(jwt);

                int userId = int.Parse(token.Issuer);
                var tracker = new TrackerAction
                {
                    type = dto.type,
                    userId = userId,
                    date = DateOnly.FromDateTime(DateTime.Now),
                    time = TimeOnly.FromDateTime(DateTime.Now)
                };

                if (tracker == null)
                {
                    return BadRequest(new { message = "Некорректное действие" });
                }


                return Created("Success", _repository.Create(tracker));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("{date}")]
        public IEnumerable<TrackerAction> GetSelectDay(DateOnly date)
        {
            return _repository.GetByDate(date);

        }
    }
}
