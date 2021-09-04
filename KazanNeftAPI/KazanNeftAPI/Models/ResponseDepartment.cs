using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KazanNeftAPI.Models
{
    public class ResponseDepartment
    {
        public ResponseDepartment(Departments department)
        {
            Id = department.ID;
            Name = department.Name;
        }
        public long Id { get; set; }
        public string Name { get; set; }
    }
}