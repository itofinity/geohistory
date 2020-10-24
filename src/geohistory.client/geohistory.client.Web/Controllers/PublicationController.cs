using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UK.CO.Itofinity.GeoHistory.Client.Api.Model.Domain;
using UK.CO.Itofinity.GeoHistory.Client.Spi.Service;
using UK.CO.Itofinity.GeoHistory.Spi.Domain.Publication;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
// API versioning https://dotnetcoretutorials.com/2017/01/17/api-versioning-asp-net-core/

namespace UK.CO.Itofinity.GeoHistory.Client.Web.Controllers
{
    [Route("api/publication")]
    [ApiController]
    public class PublicationController : ControllerBase
    {
        public IPublicationService Service { get; }

        public PublicationController(IPublicationService service)
        {
            Service = service;
        }

        // GET: api/<PublicationsController>
        [HttpGet]
        public async Task<List<IPublication>> Get([FromQuery] string title)
        {
            return await Service.Query(title);
            //return new string[] { "value1", "value2", query };
        }

        // GET: api/<PublicationsController>
        [HttpGet("/search")]
        public async Task<Dictionary<string,List<IPublication>>> Search([FromQuery] string title)
        {
            return await Service.Search(new PublicationDto(null, title, null, null));
            //return new string[] { "value1", "value2", query };
        }

        // GET api/<PublicationsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PublicationsController>
        [HttpPost]
        public async Task<IPublication> PostAsync([FromBody] IPublication book)
        {
            return await Service.Add(book);
        }

        // PUT api/<PublicationsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PublicationsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
