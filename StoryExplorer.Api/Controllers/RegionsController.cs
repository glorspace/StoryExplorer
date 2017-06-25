using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StoryExplorer.Api.Controllers
{
    public class RegionsController : ApiController
    {
        // GET: api/Regions
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Regions/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Regions
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Regions/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Regions/5
        public void Delete(int id)
        {
        }
    }
}
