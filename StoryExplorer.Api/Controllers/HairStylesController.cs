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
    public class HairStylesController : ApiController
    {
        private StoryExplorerEntities db = new StoryExplorerEntities();

        // GET: api/HairStyles
        public IQueryable<HairStyle> GetHairStyles()
        {
            return db.HairStyles;
        }

        // GET: api/HairStyles/5
        [ResponseType(typeof(HairStyle))]
        public IHttpActionResult GetHairStyle(int id)
        {
            HairStyle hairStyle = db.HairStyles.Find(id);
            if (hairStyle == null)
            {
                return NotFound();
            }

            return Ok(hairStyle);
        }

        // PUT: api/HairStyles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHairStyle(int id, HairStyle hairStyle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hairStyle.Id)
            {
                return BadRequest();
            }

            db.Entry(hairStyle).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HairStyleExists(id))
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

        // POST: api/HairStyles
        [ResponseType(typeof(HairStyle))]
        public IHttpActionResult PostHairStyle(HairStyle hairStyle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HairStyles.Add(hairStyle);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = hairStyle.Id }, hairStyle);
        }

        // DELETE: api/HairStyles/5
        [ResponseType(typeof(HairStyle))]
        public IHttpActionResult DeleteHairStyle(int id)
        {
            HairStyle hairStyle = db.HairStyles.Find(id);
            if (hairStyle == null)
            {
                return NotFound();
            }

            db.HairStyles.Remove(hairStyle);
            db.SaveChanges();

            return Ok(hairStyle);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HairStyleExists(int id)
        {
            return db.HairStyles.Count(e => e.Id == id) > 0;
        }
    }
}