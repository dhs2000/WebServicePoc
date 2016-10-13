using System;
using System.Threading.Tasks;

using Infrastructure.Messages;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Messages;

namespace WebServicePoc.Controllers
{
    [Route("api")]
    public class GeneralServiceController : Controller
    {
        private readonly IMediator mediator;

        private readonly IMessageFactory messageFactory;

        public GeneralServiceController(IMediator mediator, IMessageFactory messageFactory)
        {
            if (mediator == null)
            {
                throw new ArgumentNullException(nameof(mediator));
            }

            if (messageFactory == null)
            {
                throw new ArgumentNullException(nameof(messageFactory));
            }

            this.mediator = mediator;
            this.messageFactory = messageFactory;
        }

        // POST api/command/{CommandName}
        [HttpPost("command/{CommandName}")]
        public async Task<ActionResult> PostCommand(string commandName, [FromBody] dynamic body)
        {
            dynamic request = this.messageFactory.CreateMessage(commandName, body);
            await this.mediator.SendAsync(request);
            return this.Ok();
        }

        // POST api/query/{CommandName}
        [HttpPost("query/{QueryName}")]
        public async Task<dynamic> PostQuery(string queryName, [FromBody] dynamic body)
        {
            dynamic request = this.messageFactory.CreateMessage(queryName, body);
            return await this.mediator.SendAsync(request);
        }
    }
}