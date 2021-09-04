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

        [ResponseType(typeof(List<Models.ResponseDepartment>))]

        // GET: api/Departments
        public IHttpActionResult GetDepartments()
        {
            return Ok(_db.Departments.ToList().ConvertAll(p=> new Models.ResponseDepartment(p)).ToList());
        }
    }
}