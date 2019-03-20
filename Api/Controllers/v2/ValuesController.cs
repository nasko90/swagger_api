using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v2
{

    [ApiVersion("2.0")]
    [Route("api/{version:apiVersion}/values")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v2")]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// Super cool api get
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string[]> Get()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(1000);
            });

            var values = new[] { "version 2", "version 2" };
            return values;
        }
    }
}