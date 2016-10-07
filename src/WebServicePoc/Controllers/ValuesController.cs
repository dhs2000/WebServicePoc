using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using ApplicationServices.Projects;

using FluentValidation;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace WebServicePoc.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IMediator mediator;

        public ValuesController(IMediator mediator)
        {
            if (mediator == null)
            {
                throw new ArgumentNullException(nameof(mediator));
            }

            this.mediator = mediator;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<Project>> Get()
        {
            return (await this.mediator.SendAsync(new GetProjectsRequest())).Items;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<Project[]> Get(string id)
        {
            return (await this.mediator.SendAsync(new GetProjectsRequest() { Id = id })).Items;
        }

        // POST api/{CommandName}
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateProjectCommand createProject)
        {
            try
            {
                await this.mediator.SendAsync(createProject);
                return this.Ok();
            }
            catch (ValidationException ex)
            {
                return this.BadRequest(ex.Errors);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
    }
}