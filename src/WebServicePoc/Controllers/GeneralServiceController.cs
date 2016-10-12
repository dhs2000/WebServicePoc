using System;
using System.Threading.Tasks;

using Infrastructure.RequestFactory;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace WebServicePoc.Controllers
{
    [Route("api")]
    public class GeneralServiceController : Controller
    {
        private readonly IMediator mediator;

        private readonly IRequestFactory requestFactory;

        public GeneralServiceController(IMediator mediator, IRequestFactory requestFactory)
        {
            if (mediator == null)
            {
                throw new ArgumentNullException(nameof(mediator));
            }

            if (requestFactory == null)
            {
                throw new ArgumentNullException(nameof(requestFactory));
            }

            this.mediator = mediator;
            this.requestFactory = requestFactory;
        }

        // POST api/command/{CommandName}
        [HttpPost("command/{CommandName}")]
        public async Task<ActionResult> PostCommand(string commandName, [FromBody] dynamic body)
        {
            dynamic request = this.requestFactory.CreateRequest(commandName, body);
            await this.mediator.SendAsync(request);
            return this.Ok();
        }

        // POST api/query/{CommandName}
        [HttpPost("query/{QueryName}")]
        public async Task<dynamic> PostQuery(string queryName, [FromBody] dynamic body)
        {
            dynamic request = this.requestFactory.CreateRequest(queryName, body);
            return await this.mediator.SendAsync(request);
        }
    }
}