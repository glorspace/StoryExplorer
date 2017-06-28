using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using StoryExplorer.Repository;

namespace StoryExplorer.Api.Controllers
{
    public class PersonalitiesController : ApiController
    {
        private StoryExplorerEntities db = new StoryExplorerEntities();

        // GET: api/Personalities
        public IQueryable<Personality> GetPersonalities()
        {
            return db.Personalities;
        }

        // GET: api/Personalities/5
        [ResponseType(typeof(Personality))]
        public IHttpActionResult GetPersonality(int id)
        {
            Personality personality = db.Personalities.Find(id);
            if (personality == null)
            {
                return NotFound();
            }

            return Ok(personality);
        }

        // PUT: api/Personalities/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPersonality(int id, Personality personality)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != personality.Id)
            {
                return BadRequest();
            }

            db.Entry(personality).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonalityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Personalities
        [ResponseType(typeof(Personality))]
        public IHttpActionResult PostPersonality(Personality personality)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Personalities.Add(personality);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = personality.Id }, personality);
        }

        // DELETE: api/Personalities/5
        [ResponseType(typeof(Personality))]
        public IHttpActionResult DeletePersonality(int id)
        {
            Personality personality = db.Personalities.Find(id);
            if (personality == null)
            {
                return NotFound();
            }

            db.Personalities.Remove(personality);
            db.SaveChanges();

            return Ok(personality);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonalityExists(int id)
        {
            return db.Personalities.Count(e => e.Id == id) > 0;
        }
    }
}