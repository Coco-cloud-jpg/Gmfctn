using System;
using Data_;
using Data_.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Services.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Data_.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Gmfctn.Controllers
{
    [Route("api/events")]
    [ApiController]
    [Authorize]

    public class EventController : ControllerBase
    {
        private IEventService EventService;
        public EventController(IEventService _EventService)
        {
            EventService = _EventService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventReadDTO>>> GetEvents(CancellationToken Cancel)
        {
            try
            {
                return Ok(await EventService.GetAllEvents(Cancel));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
