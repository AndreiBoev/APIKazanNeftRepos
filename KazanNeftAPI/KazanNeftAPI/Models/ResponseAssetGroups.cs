﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KazanNeftAPI.Models
{
    public class ResponseAssetGroups
    {
        public ResponseAssetGroups(AssetGroups assetGroup)
        {
            Id = assetGroup.ID;
            Name = assetGroup.Name;
        }
        public long Id { get; set; }
        public string Name { get; set; }
    }
}