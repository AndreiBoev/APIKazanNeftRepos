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
    public class AssetsController : ApiController
    {
        private KazanNeftEntities _db = new KazanNeftEntities();

        // GET: api/Assets
        public IHttpActionResult GetAssets()
        {
            return Ok(_db.Assets.ToList().ConvertAll(p=>new Models.ResponseAsset(p)).ToList());
        }

        // GET: api/Assets/5
        [ResponseType(typeof(Assets))]
        public IHttpActionResult GetAssets(long id)
        {
            Assets assets = _db.Assets.Find(id);
            if (assets == null)
            {
                return NotFound();
            }

            return Ok(assets);
        }

        // PUT: api/Assets/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAssets(long id, Assets assets)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != assets.ID)
            {
                return BadRequest();
            }

            _db.Entry(assets).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetsExists(id))
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

        // POST: api/Assets
        [ResponseType(typeof(Assets))]
        public IHttpActionResult PostAssets(Assets assets)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Assets.Add(assets);
            _db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = assets.ID }, assets);
        }

        // DELETE: api/Assets/5
        [ResponseType(typeof(Assets))]
        public IHttpActionResult DeleteAssets(long id)
        {
            Assets assets = _db.Assets.Find(id);
            if (assets == null)
            {
                return NotFound();
            }

            _db.Assets.Remove(assets);
            _db.SaveChanges();

            return Ok(assets);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AssetsExists(long id)
        {
            return _db.Assets.Count(e => e.ID == id) > 0;
        }
    }
}