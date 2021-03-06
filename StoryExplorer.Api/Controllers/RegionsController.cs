﻿using System;
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
    [RoutePrefix("api")]
    public class RegionsController : ApiController
    {
        private StoryExplorerEntities db = new StoryExplorerEntities();

        // GET: api/Regions
        public IQueryable<Region> GetRegions()
        {
            return db.Regions.Include("Scenes");
        }

        // GET: api/Regions/5
        [ResponseType(typeof(Region))]
        public IHttpActionResult GetRegion(int id)
        {
            Region region = db.Regions.Find(id);
            if (region == null)
            {
                return NotFound();
            }

            return Ok(region);
        }

        // PUT: api/Regions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRegion(int id, Region region)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != region.Id)
            {
                return BadRequest();
            }

            Region dbRegion = db.Regions.Find(id);
            dbRegion.Name = region.Name;
            dbRegion.Description = region.Description;
            //db.Entry(region).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegionExists(id))
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

        // POST: api/Regions
        [ResponseType(typeof(Region))]
        public IHttpActionResult PostRegion(Region region)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            region.Created = DateTime.Now;

            db.Regions.Add(region);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = region.Id }, region);
        }

        // DELETE: api/Regions/5
        [ResponseType(typeof(Region))]
        public IHttpActionResult DeleteRegion(int id)
        {
            Region region = db.Regions.Find(id);
            if (region == null)
            {
                return NotFound();
            }

            db.Regions.Remove(region);
            db.SaveChanges();

            return Ok(region);
        }

        // POST
        [ResponseType(typeof(Adventurer))]
        [Route("Regions/{regionId}/Scenes", Name = "PostScene")]
        public IHttpActionResult PostScene(int regionId, Scene scene)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Region region = db.Regions.Find(regionId);

            
            db.Scenes.Add(scene);
            region.Scenes.Add(scene);
            db.SaveChanges();

            return CreatedAtRoute("PostScene", new { id = scene.Id }, scene);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RegionExists(int id)
        {
            return db.Regions.Count(e => e.Id == id) > 0;
        }
    }
}