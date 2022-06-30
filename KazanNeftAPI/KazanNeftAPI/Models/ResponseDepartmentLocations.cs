using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KazanNeftAPI.Models
{
    public class ResponseDepartmentLocations
    {
        public ResponseDepartmentLocations(DepartmentLocations departmentLocation)
        {
            Id = departmentLocation.ID;
            Location = departmentLocation.Locations.Name + " (" + departmentLocation.StartDate.ToString("dd/MM/yyyy");
            if (departmentLocation.EndDate != null)
                Location += "-" + departmentLocation.EndDate.Value.ToString("dd/MM/yyyy") + ")";
            else
                Location += ")";
        }
        public long Id { get; set; }
        public string Location { get; set; }
    }
}