using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KazanNeftAPI.Models
{
    public class ResponseAsset
    {
        public ResponseAsset(Assets asset)
        {
            ID = asset.ID;
            AssetName = asset.AssetName;
            AssetSN = asset.AssetSN;
            Department = asset.DepartmentLocations.Departments.Name;
            AssetGroup = asset.AssetGroups.Name;
            WarrantyDate = asset.WarrantyDate;
        }

        public long ID { get; set; }
        public string AssetSN { get; set; }
        public string AssetName { get; set; }
        public string Department { get; set; }
        public string AssetGroup { get; set; }
        public Nullable<System.DateTime> WarrantyDate { get; set; }
    }
}