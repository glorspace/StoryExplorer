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
using StoryExplorer.EFModel;

namespace StoryExplorer.Api.Controllers
{
    public class HairColorsController : ApiController
    {
        private StoryExplorerEntities db = new StoryExplorerEntities();

        // GET: api/HairColors
        public IQueryable<HairColor> GetHairColors()
        {
            return db.HairColors;
        }

        // GET: api/HairColors/5
        [ResponseType(typeof(HairColor))]
        public IHttpActionResult GetHairColor(int id)
        {
            HairColor hairColor = db.HairColors.Find(id);
            if (hairColor == null)
            {
                return NotFound();
            }

            return Ok(hairColor);
        }

        // PUT: api/HairColors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHairColor(int id, HairColor hairColor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hairColor.Id)
            {
                return BadRequest();
            }

            db.Entry(hairColor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HairColorExists(id))
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

        // POST: api/HairColors
        [ResponseType(typeof(HairColor))]
        public IHttpActionResult PostHairColor(HairColor hairColor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HairColors.Add(hairColor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = hairColor.Id }, hairColor);
        }

        // DELETE: api/HairColors/5
        [ResponseType(typeof(HairColor))]
        public IHttpActionResult DeleteHairColor(int id)
        {
            HairColor hairColor = db.HairColors.Find(id);
            if (hairColor == null)
            {
                return NotFound();
            }

            db.HairColors.Remove(hairColor);
            db.SaveChanges();

            return Ok(hairColor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HairColorExists(int id)
        {
            return db.HairColors.Count(e => e.Id == id) > 0;
        }
    }
}