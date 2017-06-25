using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StoryExplorer.Api.Controllers
{
    public class AdventurersController : ApiController
    {
        // GET: api/Adventurers
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Adventurers/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Adventurers
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Adventurers/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Adventurers/5
        public void Delete(int id)
        {
        }
    }
}
