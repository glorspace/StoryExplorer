using StoryExplorer.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace StoryExplorer.Api.Controllers
{
    [RoutePrefix("api")]
    public class DesignatedAuthorsController : ApiController
    {
        private StoryExplorerEntities db = new StoryExplorerEntities();

        // GET
        [Route("Regions/{regionId}/DesignatedAuthors")]
        public IHttpActionResult GetDesignatedAuthors(int regionId)
        {
            Region region = db.Regions.Find(regionId);
            //var designatedAuthors = region.Adventurers1;
            //var eligibleAdventurers = db.Adventurers.Where(x => x.Id != region.OwnerId && !region.Adventurers1.Any(y => y.Id == x.Id));
            var designatedAuthorIds = region.Adventurers1.Select(x => x.Id);
            var eligibleAdventurers = db.Adventurers.Where(x => x.Id != region.OwnerId && !designatedAuthorIds.Contains(x.Id));
            return Ok(new { DesignatedAuthors = region.Adventurers1, EligibleAdventurers = eligibleAdventurers });
        }

        // POST
        [ResponseType(typeof(Adventurer))]
        [Route("Regions/{regionId}/DesignatedAuthors/{designatedAuthorId}", Name ="PostDesignatedAuthor")]
        public IHttpActionResult PostDesignatedAuthor(int regionId, int designatedAuthorId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Region region = db.Regions.Find(regionId);
            if (region == null)
            {
                return NotFound();
            }

            Adventurer designatedAuthor = db.Adventurers.Find(designatedAuthorId);

            region.Adventurers1.Add(designatedAuthor);
            db.SaveChanges();

            return CreatedAtRoute("PostDesignatedAuthor", new { regionId = regionId, designatedAuthorId = designatedAuthorId }, designatedAuthor);
        }

        // DELETE
        [ResponseType(typeof(Adventurer))]
        [Route("Regions/{regionId}/DesignatedAuthors/{adventurerId}")]
        public IHttpActionResult DeleteDesignatedAuthors(int regionId, int adventurerId)
        {
            Region region = db.Regions.Find(regionId);
            if (region == null)
            {
                return NotFound();
            }

            Adventurer designatedAuthor = db.Adventurers.Find(adventurerId);
            if (designatedAuthor == null)
            {
                return NotFound();
            }

            region.Adventurers1.Remove(designatedAuthor);
            db.SaveChanges();

            return Ok(designatedAuthor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
