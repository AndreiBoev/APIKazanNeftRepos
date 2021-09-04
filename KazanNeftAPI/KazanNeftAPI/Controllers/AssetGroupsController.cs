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
using KazanNeftAPI;

namespace KazanNeftAPI.Controllers
{
    public class AssetGroupsController : ApiController
    {
        private KazanNeftEntities _db = new KazanNeftEntities();

        private AssetGroupsController()
        {
            _db.Configuration.LazyLoadingEnabled = false;
            _db.Configuration.ProxyCreationEnabled = false;
        }

        [ResponseType(typeof(List<Models.ResponseAssetGroups>))]
        // GET: api/AssetGroups
        public IHttpActionResult GetAssetGroups()
        {
            return  Ok(_db.AssetGroups.ToList().ConvertAll(p=> new Models.ResponseAssetGroups(p)).ToList());
        }

        // GET: api/AssetGroups/5
        [ResponseType(typeof(AssetGroups))]
        public IHttpActionResult GetAssetGroups(long id)
        {
            AssetGroups assetGroups = _db.AssetGroups.Find(id);
            if (assetGroups == null)
            {
                return NotFound();
            }

            return Ok(assetGroups);
        }

        // PUT: api/AssetGroups/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAssetGroups(long id, AssetGroups assetGroups)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != assetGroups.ID)
            {
                return BadRequest();
            }

            _db.Entry(assetGroups).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetGroupsExists(id))
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

        // POST: api/AssetGroups
        [ResponseType(typeof(AssetGroups))]
        public IHttpActionResult PostAssetGroups(AssetGroups assetGroups)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.AssetGroups.Add(assetGroups);
            _db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = assetGroups.ID }, assetGroups);
        }

        // DELETE: api/AssetGroups/5
        [ResponseType(typeof(AssetGroups))]
        public IHttpActionResult DeleteAssetGroups(long id)
        {
            AssetGroups assetGroups = _db.AssetGroups.Find(id);
            if (assetGroups == null)
            {
                return NotFound();
            }

            _db.AssetGroups.Remove(assetGroups);
            _db.SaveChanges();

            return Ok(assetGroups);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AssetGroupsExists(long id)
        {
            return _db.AssetGroups.Count(e => e.ID == id) > 0;
        }
    }
}