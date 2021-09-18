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
    public class EmployeesController : ApiController
    {
        private KazanNeftEntities _db = new KazanNeftEntities();

        // GET: api/Employees
        public IHttpActionResult GetEmployees()
        {
            return Ok(_db.Employees.ToList().ConvertAll(p => new Models.ResponseEmploye(p)).ToList());
        }

        // GET: api/Employees/5
        [ResponseType(typeof(Employees))]
        public IHttpActionResult GetEmployees(long id)
        {
            Employees employees = _db.Employees.Find(id);
            if (employees == null)
            {
                return NotFound();
            }

            return Ok(employees);
        }

        // PUT: api/Employees/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployees(long id, Employees employees)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employees.ID)
            {
                return BadRequest();
            }

            _db.Entry(employees).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeesExists(id))
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

        // POST: api/Employees
        [ResponseType(typeof(Employees))]
        public IHttpActionResult PostEmployees(Employees employees)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Employees.Add(employees);
            _db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = employees.ID }, employees);
        }

        // DELETE: api/Employees/5
        [ResponseType(typeof(Employees))]
        public IHttpActionResult DeleteEmployees(long id)
        {
            Employees employees = _db.Employees.Find(id);
            if (employees == null)
            {
                return NotFound();
            }

            _db.Employees.Remove(employees);
            _db.SaveChanges();

            return Ok(employees);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeesExists(long id)
        {
            return _db.Employees.Count(e => e.ID == id) > 0;
        }
    }
}