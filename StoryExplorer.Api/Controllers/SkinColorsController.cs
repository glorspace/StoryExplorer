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
    public class SkinColorsController : ApiController
    {
        private StoryExplorerEntities db = new StoryExplorerEntities();

        // GET: api/SkinColors
        public IQueryable<SkinColor> GetSkinColors()
        {
            return db.SkinColors;
        }

        // GET: api/SkinColors/5
        [ResponseType(typeof(SkinColor))]
        public IHttpActionResult GetSkinColor(int id)
        {
            SkinColor skinColor = db.SkinColors.Find(id);
            if (skinColor == null)
            {
                return NotFound();
            }

            return Ok(skinColor);
        }

        // PUT: api/SkinColors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSkinColor(int id, SkinColor skinColor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != skinColor.Id)
            {
                return BadRequest();
            }

            db.Entry(skinColor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkinColorExists(id))
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

        // POST: api/SkinColors
        [ResponseType(typeof(SkinColor))]
        public IHttpActionResult PostSkinColor(SkinColor skinColor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SkinColors.Add(skinColor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = skinColor.Id }, skinColor);
        }

        // DELETE: api/SkinColors/5
        [ResponseType(typeof(SkinColor))]
        public IHttpActionResult DeleteSkinColor(int id)
        {
            SkinColor skinColor = db.SkinColors.Find(id);
            if (skinColor == null)
            {
                return NotFound();
            }

            db.SkinColors.Remove(skinColor);
            db.SaveChanges();

            return Ok(skinColor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SkinColorExists(int id)
        {
            return db.SkinColors.Count(e => e.Id == id) > 0;
        }
    }
}