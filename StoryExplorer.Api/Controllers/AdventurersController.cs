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
    public class AdventurersController : ApiController
    {
        private StoryExplorerEntities db = new StoryExplorerEntities();

        // GET: api/Adventurers
        public IQueryable<Adventurer> GetAdventurers()
        {
            return db.Adventurers;
        }

        // GET: api/Adventurers/5
        [ResponseType(typeof(Adventurer))]
        public IHttpActionResult GetAdventurer(int id)
        {
            Adventurer adventurer = db.Adventurers.Find(id);
            if (adventurer == null)
            {
                return NotFound();
            }

            return Ok(adventurer);
        }

        // PUT: api/Adventurers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAdventurer(int id, Adventurer adventurer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != adventurer.Id)
            {
                return BadRequest();
            }

            db.Entry(adventurer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdventurerExists(id))
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

        // POST: api/Adventurers
        [ResponseType(typeof(Adventurer))]
        public IHttpActionResult PostAdventurer(Adventurer adventurer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            adventurer.Created = DateTime.Now;
            db.Adventurers.Add(adventurer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = adventurer.Id }, adventurer);
        }

        // DELETE: api/Adventurers/5
        [ResponseType(typeof(Adventurer))]
        public IHttpActionResult DeleteAdventurer(int id)
        {
            Adventurer adventurer = db.Adventurers.Find(id);
            if (adventurer == null)
            {
                return NotFound();
            }

            db.Adventurers.Remove(adventurer);
            db.SaveChanges();

            return Ok(adventurer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AdventurerExists(int id)
        {
            return db.Adventurers.Count(e => e.Id == id) > 0;
        }
    }
}