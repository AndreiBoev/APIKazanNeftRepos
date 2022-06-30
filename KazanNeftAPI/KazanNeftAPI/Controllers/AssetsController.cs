using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using KazanNeftAPI;

namespace KazanNeftAPI.Controllers
{
    public class AssetsController : ApiController
    {
        private KazanNeftEntities _db = new KazanNeftEntities();

        // GET: api/Assets
        public IHttpActionResult GetAssets()
        {
            return Ok(_db.Assets.ToList().ConvertAll(p => new Models.ResponseAsset(p)).ToList());
        }

        // GET: api/GeneratedAssetSN
        public IHttpActionResult GetGeneratedAssetSN(long departmentID, long assetGroupID)
        {
            #region Проверка
            if (_db.AssetGroups.FirstOrDefault(p => p.ID == assetGroupID) == null)
                ModelState.AddModelError("AssetGroupID", "AssetGroupId не найден в БД.");
            if (_db.Departments.FirstOrDefault(p => p.ID == departmentID) == null)
                ModelState.AddModelError("DepartmentID", "DepartmentID не найден в БД.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            #endregion
            string departmentIdDisplay = departmentID.ToString();
            if (departmentIdDisplay.Length != 2)
                departmentIdDisplay = "0" + departmentIdDisplay;

            string assetGroupIdDisplay = assetGroupID.ToString();
            if (assetGroupIdDisplay.Length != 2)
                assetGroupIdDisplay = "0" + assetGroupIdDisplay;

            var number = 0;
            foreach (var asset in _db.Assets)
            {
                var currentAssetSN = asset.AssetSN.ToCharArray();
                if (currentAssetSN[0].ToString() + currentAssetSN[1].ToString() == departmentIdDisplay
                    && currentAssetSN[3].ToString() + currentAssetSN[4].ToString() == assetGroupIdDisplay)
                {
                    var currentNumber = int.Parse(currentAssetSN[6].ToString() + currentAssetSN[7].ToString() + currentAssetSN[8].ToString() + currentAssetSN[9].ToString());
                    if (currentNumber > number)
                    {
                        number = currentNumber;
                    }
                }
            }
            number++;
            string displayNumber = number.ToString();
            switch (displayNumber.Length)
            {
                case 1:
                    displayNumber = "000" + displayNumber;
                    break;
                case 2:
                    displayNumber = "00" + displayNumber;
                    break;
                case 3:
                    displayNumber = "0" + displayNumber;
                    break;
            }
            string assetSN = departmentIdDisplay + "/" + assetGroupIdDisplay + "/" + displayNumber;
            return Ok(assetSN);
        }

        // GET: api/Assets/5
        [ResponseType(typeof(Assets))]
        public IHttpActionResult GetAssets(long id)
        {
            Assets assets = _db.Assets.Find(id);
            if (assets == null)
            {
                return NotFound();
            }

            return Ok(assets);
        }

        // PUT: api/Assets/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAssets(long id, Assets assets)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != assets.ID)
            {
                return BadRequest();
            }

            _db.Entry(assets).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetsExists(id))
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

        // POST: api/Assets
        [ResponseType(typeof(Assets))]
        public async Task<IHttpActionResult> PostAssetsAsync(Assets assets)
        {
            #region Проверка на пустоту
            if (assets == null)
                return BadRequest("Передайте актив");
            if (string.IsNullOrWhiteSpace(assets.AssetName))
                ModelState.AddModelError("AssetName", "AssetName должно быть заполнено.");
            if (string.IsNullOrWhiteSpace(assets.AssetSN))
                ModelState.AddModelError("AssetSN", "AssetSN должно быть заполнено.");
            if (assets.AssetGroupID == 0)
                ModelState.AddModelError("AssetGroupID", "AssetGroupID должно быть заполнено.");
            if (assets.DepartmentLocationID == 0)
                ModelState.AddModelError("DepartmentLocationID", "DepartmentLocationID должно быть заполнено.");
            if (assets.EmployeeID == 0)
                ModelState.AddModelError("EmployeeID", "EmployeeID должно быть заполнено.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            #endregion
            #region Основная проверка
            if (_db.DepartmentLocations.FirstOrDefault(p => p.ID == assets.DepartmentLocationID) == null)
            {
                ModelState.AddModelError("DepartmentLocationID", "DepartmentLocationID не найден в БД.");
            }
            if (_db.Employees.FirstOrDefault(p => p.ID == assets.EmployeeID) == null)
            {
                ModelState.AddModelError("EmployeeID", "EmployeeID не найден в БД.");
            }
            if (_db.AssetGroups.FirstOrDefault(p => p.ID == assets.AssetGroupID) == null)
            {
                ModelState.AddModelError("AssetGroupID", "AssetGroupID не найден в БД.");
            }
            if (_db.Assets.FirstOrDefault(p => p.AssetName == assets.AssetName) != null)
            {
                ModelState.AddModelError("AssetName", "Актив с таким именем уже есть в БД.");
            }
            #region Check AssetSN
            var assetSN = assets.AssetSN.ToCharArray();
            if (assetSN[2].ToString() != "/" || assetSN[5].ToString() != "/" || assetSN.Length != 10)
            {
                ModelState.AddModelError("AssetSN", "AssetSN не соответствует формату dd/gg/nnnn.");
            }
            else
            {
                if (_db.Assets.ToList().FirstOrDefault(p => p.AssetSN == assets.AssetSN) == null)
                {
                    long departmentID = long.Parse(assetSN[0].ToString() + assetSN[1].ToString());
                    if (_db.Departments.FirstOrDefault(p => p.ID == departmentID) == null)
                    {
                        ModelState.AddModelError("AssetSN", "Department не найден в БД");
                    }
                    long assetGroupID = long.Parse(assetSN[3].ToString() + assetSN[4].ToString());
                    if (_db.AssetGroups.FirstOrDefault(p => p.ID == assetGroupID) == null)
                    {
                        ModelState.AddModelError("AssetSN", "AssetGroup не найден в БД");
                    }
                }
                else
                {
                    assets.AssetSN = await GetGeneratedAssetSN(_db.DepartmentLocations.FirstOrDefault(p=> p.ID == assets.DepartmentLocationID).DepartmentID, assets.AssetGroupID).ExecuteAsync(new System.Threading.CancellationToken()).Result.Content.ReadAsStringAsync();
                }
            }
            #endregion
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            #endregion

            _db.Assets.Add(assets);
            _db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = assets.ID }, new Models.ResponseAsset(assets));
        }

        // DELETE: api/Assets/5
        [ResponseType(typeof(Assets))]
        public IHttpActionResult DeleteAssets(long id)
        {
            Assets assets = _db.Assets.Find(id);
            if (assets == null)
            {
                return NotFound();
            }

            _db.Assets.Remove(assets);
            _db.SaveChanges();

            return Ok(assets);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AssetsExists(long id)
        {
            return _db.Assets.Count(e => e.ID == id) > 0;
        }
    }
}