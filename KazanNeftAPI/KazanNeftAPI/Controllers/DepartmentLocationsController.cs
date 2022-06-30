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
    public class DepartmentLocationsController : ApiController
    {
        private KazanNeftEntities _db = new KazanNeftEntities();

        // GET: api/DepartmentLocations
        public IHttpActionResult GetDepartmentLocations(int departmentId)
        {
            return Ok(_db.DepartmentLocations.ToList().ConvertAll(p=> new Models.ResponseDepartmentLocations(p)).ToList());
        }

        // GET: api/DepartmentLocations/5
        [ResponseType(typeof(DepartmentLocations))]
        public IHttpActionResult GetDepartmentLocations(long id)
        {
            DepartmentLocations departmentLocations = _db.DepartmentLocations.Find(id);
            if (departmentLocations == null)
            {
                return NotFound();
            }

            return Ok(departmentLocations);
        }

        // PUT: api/DepartmentLocations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDepartmentLocations(long id, DepartmentLocations departmentLocations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != departmentLocations.ID)
            {
                return BadRequest();
            }

            _db.Entry(departmentLocations).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentLocationsExists(id))
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

        // POST: api/DepartmentLocations
        [ResponseType(typeof(DepartmentLocations))]
        public IHttpActionResult PostDepartmentLocations(DepartmentLocations departmentLocations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.DepartmentLocations.Add(departmentLocations);
            _db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = departmentLocations.ID }, departmentLocations);
        }

        // DELETE: api/DepartmentLocations/5
        [ResponseType(typeof(DepartmentLocations))]
        public IHttpActionResult DeleteDepartmentLocations(long id)
        {
            DepartmentLocations departmentLocations = _db.DepartmentLocations.Find(id);
            if (departmentLocations == null)
            {
                return NotFound();
            }

            _db.DepartmentLocations.Remove(departmentLocations);
            _db.SaveChanges();

            return Ok(departmentLocations);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DepartmentLocationsExists(long id)
        {
            return _db.DepartmentLocations.Count(e => e.ID == id) > 0;
        }
    }
}