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
            DepartmentID = new KazanNeftEntities().Assets.ToList()
                .FirstOrDefault(p => p.ID == asset.ID).DepartmentLocations.DepartmentID;
            WarrantyDate = asset.WarrantyDate;
        }

        public long ID { get; set; }
        public string AssetSN { get; set; }
        public string AssetName { get; set; }
        public long DepartmentID { get; set; }
        public Nullable<System.DateTime> WarrantyDate { get; set; }
    }
}