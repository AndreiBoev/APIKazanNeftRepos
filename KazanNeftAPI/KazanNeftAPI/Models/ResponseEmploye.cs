using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KazanNeftAPI.Models
{
    public class ResponseEmploye
    {
        public ResponseEmploye(Employees employe)
        {
            Id = employe.ID;
            FullName = employe.FirstName + " " + employe.LastName;
        }
        public long Id { get; set; }
        public string FullName { get; set; }
    }
}