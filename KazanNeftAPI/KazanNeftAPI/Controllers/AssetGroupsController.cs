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
    public class AssetGroupsController : ApiController
    {
        private KazanNeftEntities _db = new KazanNeftEntities();

        [ResponseType(typeof(List<Models.ResponseAssetGroups>))]

        // GET: api/AssetGroups
        public IHttpActionResult GetAssetGroups()
        {
            return  Ok(_db.AssetGroups.ToList().ConvertAll(p=> new Models.ResponseAssetGroups(p)).ToList());
        }       
    }
}