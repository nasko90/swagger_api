using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Api.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/{version:apiVersion}/values")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [ValidateModelState]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// Take something
        /// </summary>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="200">Values</response>
        [HttpGet]
        public JsonResult Get()
        {
            Thread.Sleep(1000);
            var values = new[] { "value1", "value2" };
            return new JsonResult(values);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromHeader] string idempotentKey, [FromBody] string value)
        {

            if (!ModelState.IsValid)
            {

                return BadRequest();
            }

            return new JsonResult("Super");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) { }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id) { }
    }
}