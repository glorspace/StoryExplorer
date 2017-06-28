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
    public class HeightsController : ApiController
    {
        private StoryExplorerEntities db = new StoryExplorerEntities();

        // GET: api/Heights
        public IQueryable<Height> GetHeights()
        {
            return db.Heights;
        }

        // GET: api/Heights/5
        [ResponseType(typeof(Height))]
        public IHttpActionResult GetHeight(int id)
        {
            Height height = db.Heights.Find(id);
            if (height == null)
            {
                return NotFound();
            }

            return Ok(height);
        }

        // PUT: api/Heights/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHeight(int id, Height height)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != height.Id)
            {
                return BadRequest();
            }

            db.Entry(height).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HeightExists(id))
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

        // POST: api/Heights
        [ResponseType(typeof(Height))]
        public IHttpActionResult PostHeight(Height height)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Heights.Add(height);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = height.Id }, height);
        }

        // DELETE: api/Heights/5
        [ResponseType(typeof(Height))]
        public IHttpActionResult DeleteHeight(int id)
        {
            Height height = db.Heights.Find(id);
            if (height == null)
            {
                return NotFound();
            }

            db.Heights.Remove(height);
            db.SaveChanges();

            return Ok(height);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HeightExists(int id)
        {
            return db.Heights.Count(e => e.Id == id) > 0;
        }
    }
}