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
    public class DepartmentsController : ApiController
    {
        private KazanNeftEntities _db = new KazanNeftEntities();

        private DepartmentsController()
        {
            _db.Configuration.LazyLoadingEnabled = false;
            _db.Configuration.ProxyCreationEnabled = false;
        }
        [ResponseType(typeof(List<Models.ResponseDepartment>))]
        // GET: api/Departments
        public IHttpActionResult GetDepartments()
        {
            return Ok(_db.Departments.ToList().ConvertAll(p=> new Models.ResponseDepartment(p)).ToList());
        }

        // GET: api/Departments/5
        [ResponseType(typeof(Departments))]
        public IHttpActionResult GetDepartments(long id)
        {
            Departments departments = _db.Departments.Find(id);
            if (departments == null)
            {
                return NotFound();
            }

            return Ok(departments);
        }

        // PUT: api/Departments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDepartments(long id, Departments departments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != departments.ID)
            {
                return BadRequest();
            }

            _db.Entry(departments).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentsExists(id))
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

        // POST: api/Departments
        [ResponseType(typeof(Departments))]
        public IHttpActionResult PostDepartments(Departments departments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Departments.Add(departments);
            _db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = departments.ID }, departments);
        }

        // DELETE: api/Departments/5
        [ResponseType(typeof(Departments))]
        public IHttpActionResult DeleteDepartments(long id)
        {
            Departments departments = _db.Departments.Find(id);
            if (departments == null)
            {
                return NotFound();
            }

            _db.Departments.Remove(departments);
            _db.SaveChanges();

            return Ok(departments);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DepartmentsExists(long id)
        {
            return _db.Departments.Count(e => e.ID == id) > 0;
        }
    }
}