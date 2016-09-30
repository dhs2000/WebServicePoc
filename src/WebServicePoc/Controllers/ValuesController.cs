using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ApplicationServices.Projects;

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

        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<Project>> Get()
        {
            return (await this.mediator.SendAsync(new GetProjectsRequest())).Items;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody] CreateProjectCommand createProject)
        {
            await this.mediator.SendAsync(new CreateProjectCommand("111", "P3"));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
