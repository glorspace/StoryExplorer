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
    public class EyeColorsController : ApiController
    {
        private StoryExplorerEntities db = new StoryExplorerEntities();

        // GET: api/EyeColors
        public IQueryable<EyeColor> GetEyeColors()
        {
            return db.EyeColors;
        }

        // GET: api/EyeColors/5
        [ResponseType(typeof(EyeColor))]
        public IHttpActionResult GetEyeColor(int id)
        {
            EyeColor eyeColor = db.EyeColors.Find(id);
            if (eyeColor == null)
            {
                return NotFound();
            }

            return Ok(eyeColor);
        }

        // PUT: api/EyeColors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEyeColor(int id, EyeColor eyeColor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eyeColor.Id)
            {
                return BadRequest();
            }

            db.Entry(eyeColor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EyeColorExists(id))
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

        // POST: api/EyeColors
        [ResponseType(typeof(EyeColor))]
        public IHttpActionResult PostEyeColor(EyeColor eyeColor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EyeColors.Add(eyeColor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = eyeColor.Id }, eyeColor);
        }

        // DELETE: api/EyeColors/5
        [ResponseType(typeof(EyeColor))]
        public IHttpActionResult DeleteEyeColor(int id)
        {
            EyeColor eyeColor = db.EyeColors.Find(id);
            if (eyeColor == null)
            {
                return NotFound();
            }

            db.EyeColors.Remove(eyeColor);
            db.SaveChanges();

            return Ok(eyeColor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EyeColorExists(int id)
        {
            return db.EyeColors.Count(e => e.Id == id) > 0;
        }
    }
}