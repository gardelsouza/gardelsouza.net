using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using api.Models;
using api.Services;
using api.Infra;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly ILogger<MessageController> _logger;
        private readonly IMessageService _messageDbService;

        public MessageController(ILogger<MessageController> logger, IMessageService messageDbService)
        {
            _messageDbService = messageDbService;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Message> Get([FromQuery]PageParameters parameters)
        {
            return _messageDbService.FindAll(parameters);
        }

        [HttpGet]
        public IEnumerable<Message> GetPendingReading([FromQuery]PageParameters parameters)
        {
            return _messageDbService.FindPendingReading(parameters);
        }

        [HttpPost]
        public ActionResult<Message> Insert(Message dto)
        {
            var id = _messageDbService.Insert(dto);
            if (id != default)
                return CreatedAtRoute("FindOne", new { id = id }, dto);
            else
                return BadRequest();
        }

        [HttpPut]
        public int UpdateViewedMessages(Message lastViewed)
        {
            return 0;
        }
    }
}