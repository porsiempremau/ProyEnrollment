using GFD.Siscom.Enrollment.Models;
using GFD.Siscom.Enrollment.Utilities.Auth;
using GFD.Siscom.Enrollment.Utilities.Parameters;
using GFD.Siscom.Enrollment.Utilities.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Services
{
    public class CatalogsService : Controller
    {
        private readonly IOptions<BaseModel> appSettings;
        private RequestApi RequestsApi { get; set; }
        public CatalogsService(IOptions<BaseModel> app)
        {
            appSettings = app;
            RequestsApi = new RequestApi(appSettings.Value.WebApiBaseUrl);
        }

        [HttpGet("CatalogTypes/Get")]
        public async Task<IActionResult> Get([FromQuery] string type, [FromQuery] bool isToSelect, [FromQuery] string name = "")
        {
            try
            {
                
                var result = await RequestsApi.SendURIAsync("/api/" + type , HttpMethod.Get, Auth.Login.Token);
                if (result.Contains("error"))
                {
                    return null;
                }

                var data = JsonConvert.DeserializeObject<List<TypeServicesGralVM>>(result);
                if (isToSelect)
                {
                    return Ok(data);
                }
                else
                {
                    ViewData["Title"] = name;
                    return View("~/Views/Catalogs/CatalogsGeneral.cshtml", new { type = type, data = data });
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpGet("CatalogTypes/GetOne")]
        public async Task<IActionResult> GetOne([FromQuery] string type, [FromQuery] int id)
        {
            try
            {
                var result = await RequestsApi.SendURIAsync("/api/" + type + "/" + id, HttpMethod.Get, Auth.Login.Token);
                if (result.Contains("error"))
                {
                    return null;
                }

                var data = JsonConvert.DeserializeObject<TypeServicesGralVM>(result);
                return Ok(data);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpPost("CreateAndEditCatalog")]
        public async Task<IActionResult> CreateAndEdit(IFormCollection data, [FromQuery] string type)
        {
            var method = HttpMethod.Post;
            var url = "/api/" + type + "/";
            var mesagge = "Se agregó correctamente";
            var catalog = new TypeServicesGralVM()
            {
                Id = data.ContainsKey("ID") ? Convert.ToInt32(data["ID"].ToString()) : 0,
                Name = data.ContainsKey("name") ? data["name"].ToString() : "",
                Description = data.ContainsKey("description") ? data["description"].ToString() : "",
                Acronym = data.ContainsKey("acronym") ? data["acronym"].ToString() : "",
                IntakeAcronym = data.ContainsKey("intakeAcronym") ? data["intakeAcronym"].ToString() : "",
                IsActive = data.ContainsKey("isActive") ? Convert.ToBoolean(data["isActive"].ToString()) : false,
                clasificationGroup = data.ContainsKey("clasificationGroup") ? Convert.ToInt32(data["clasificationGroup"].ToString()) : 0
            };

            if (catalog.Id != 0)
            {
                method = HttpMethod.Put;
                url = "/api/" + type + "/" + catalog.Id;
                mesagge = "Se modificó correctamente";
            }
            var body = new StringContent(JsonConvert.SerializeObject(catalog), Encoding.UTF8, "application/json");
            var result = await RequestsApi.SendURIAsync(url, method, Auth.Login.Token, body);
            if (result.Contains("error"))
            {
                return Conflict(result);
            }

            return Ok(mesagge);
        }

        #region Services
        [HttpGet("Services/Get/{id}")]
        public async Task<IActionResult> GetServices([FromRoute] int id)
        {
            try
            {
                var ID = "";
                dynamic data = null;
                if (id != 0)
                {
                    ID = id.ToString();
                }
                var result = await RequestsApi.SendURIAsync("/api/Services/" + ID, HttpMethod.Get, Auth.Login.Token);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }

                if (id != 0)
                {
                    data = JsonConvert.DeserializeObject<ServicesVM>(result);
                    return Ok(data);
                }
                else
                {
                    ViewData["Title"] = "Servicios";
                    data = JsonConvert.DeserializeObject<List<ServicesVM>>(result);
                    return View("~/Views/Catalogs/ServicesCatalog.cshtml", data);
                }
                
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpPost("Services/Post")]
        public async Task<IActionResult> CreateEditService(IFormCollection data)
        {
            try
            {
                var method = HttpMethod.Post;
                var ID = "";
                var mesagge = "Se agregó correctamente";
                var service = new ServicesVM()
                {
                    Id = data.ContainsKey("id") ? Convert.ToInt32(data["id"].ToString()) : 0,
                    Name = data["name"].ToString(),
                    Order = Convert.ToInt32(data["order"].ToString()),
                    HaveTax = Convert.ToBoolean(data["haveTax"].ToString()),
                    InAgreement = Convert.ToBoolean(data["inAgreement"].ToString()),
                    IsActive = Convert.ToBoolean(data["isActive"].ToString()),
                };
                if (service.Id != 0)
                {
                    method = HttpMethod.Put;
                    ID = service.Id.ToString();
                    mesagge = "Se modificó correctamente";
                }
                var body = new StringContent(JsonConvert.SerializeObject(service), Encoding.UTF8, "application/json");
                var result = await RequestsApi.SendURIAsync("/api/Services/" + ID, method, Auth.Login.Token, body);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }
                return Ok(mesagge);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Discounts
        [HttpGet("Discounts/Get/{id}")]
        public async Task<IActionResult> GetDiscounts([FromRoute] int id, [FromQuery] bool isToSelect = false)
        {
            try
            {
                var ID = "";
                dynamic data = null;
                if (id != 0)
                {
                    ID = id.ToString();
                }
                var result = await RequestsApi.SendURIAsync("/api/Discounts/" + ID, HttpMethod.Get, Auth.Login.Token);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }
                if (isToSelect)
                {
                    data = JsonConvert.DeserializeObject<List<DiscountsVM>>(result);
                    return Ok(data);
                }
                if (id != 0)
                {
                    data = JsonConvert.DeserializeObject<DiscountsVM>(result);
                    return Ok(data);
                }
                else
                {
                    ViewData["Title"] = "Descuentos";
                    data = JsonConvert.DeserializeObject<List<DiscountsVM>>(result);
                    return View("~/Views/Catalogs/DiscountsCatalog.cshtml", data);
                }

            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpPost("Discounts/Post")]
        public async Task<IActionResult> CreateEditDiscount(IFormCollection data)
        {
            try
            {
                var method = HttpMethod.Post;
                var ID = "";
                var mesagge = "Se agregó correctamente";
                var discount = new DiscountsVM()
                {
                    Id = data.ContainsKey("id") ? Convert.ToInt32(data["id"].ToString()) : 0,
                    Name = data["name"].ToString(),
                    Percentage = Convert.ToInt32(data["percentage"].ToString()),
                    Mouths = Convert.ToInt32(data["months"].ToString()),
                    InAgreement = Convert.ToBoolean(data["inAgreement"].ToString()),
                    IsActive = Convert.ToBoolean(data["isActive"].ToString()),
                    StartDate = Convert.ToDateTime(data["startDate"].ToString()),
                    EndDate = Convert.ToDateTime(data["endDate"].ToString())
                };
                if (discount.Id != 0)
                {
                    method = HttpMethod.Put;
                    ID = discount.Id.ToString();
                    mesagge = "Se modificó correctamente";
                }
                var body = new StringContent(JsonConvert.SerializeObject(discount), Encoding.UTF8, "application/json");
                var result = await RequestsApi.SendURIAsync("/api/Discounts/" + ID, method, Auth.Login.Token, body);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }
                return Ok(mesagge);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Region
        [HttpGet("Regions/Get/{id}")]
        public async Task<IActionResult> GetRegion([FromRoute] int id, [FromQuery] bool isToSelect = false)
        {
            try
            {
                var ID = "";
                dynamic data = null;
                if (id != 0)
                {
                    ID = id.ToString();
                }
                var result = await RequestsApi.SendURIAsync("/api/Regions/" + ID, HttpMethod.Get, Auth.Login.Token);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }

                if (id != 0)
                {
                    data = JsonConvert.DeserializeObject<RegionVM>(result);
                    return Ok(data);
                }
                else if (isToSelect)
                {
                    data = JsonConvert.DeserializeObject<List<RegionVM>>(result);
                    return Ok(data);
                }
                else
                {
                    ViewData["Title"] = "Regiones";
                    data = JsonConvert.DeserializeObject<List<RegionVM>>(result);
                    return View("~/Views/Catalogs/RegionsCatalog.cshtml", data);
                }

            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpPost("Regions/Post")]
        public async Task<IActionResult> CreateEditRegion(IFormCollection data)
        {
            try
            {
                var method = HttpMethod.Post;
                var ID = "";
                var mesagge = "Se agregó correctamente";
                var region = new RegionVM()
                {
                    Id = data.ContainsKey("id") ? Convert.ToInt32(data["id"].ToString()) : 0,
                    Name = Convert.ToInt32(data["name"].ToString()),
                    Price = Convert.ToInt32(data["price"].ToString())
                };
                if (region.Id != 0)
                {
                    method = HttpMethod.Put;
                    ID = region.Id.ToString();
                    mesagge = "Se modificó correctamente";
                }
                var body = new StringContent(JsonConvert.SerializeObject(region), Encoding.UTF8, "application/json");
                var result = await RequestsApi.SendURIAsync("/api/Regions/" + ID, method, Auth.Login.Token, body);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }
                return Ok(mesagge);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Estados
        [HttpGet("States/Get/{id}")]
        public async Task<IActionResult> GetState([FromRoute] int id, [FromQuery] bool isToSelect)
        {
            try
            {
                var ID = "";
                dynamic data = null;
                if (id != 0)
                {
                    ID = id.ToString();
                }
                var result = await RequestsApi.SendURIAsync("/api/Countries/1/States/" + ID, HttpMethod.Get, Auth.Login.Token);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }

                if (id != 0)
                {
                    data = JsonConvert.DeserializeObject<StatesVM>(result);
                    return Ok(data);
                } else if (isToSelect)
                {
                    data = JsonConvert.DeserializeObject<List<StatesVM>>(result);
                    return Ok( data);
                }
                else
                {
                    ViewData["Title"] = "Estados";
                    data = JsonConvert.DeserializeObject<List<StatesVM>>(result);
                    return View("~/Views/Catalogs/StatesCatalog.cshtml", data);
                }

            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpPost("States/Post")]
        public async Task<IActionResult> CreateEditState(IFormCollection data)
        {
            try
            {
                var method = HttpMethod.Post;
                var ID = "";
                var mesagge = "Se agregó correctamente";
                var state = new StatesVM()
                {
                    Id = data.ContainsKey("id") ? Convert.ToInt32(data["id"].ToString()) : 0,
                    Name = data["name"].ToString(),
                };
                if (state.Id != 0)
                {
                    method = HttpMethod.Put;
                    ID = state.Id.ToString();
                    mesagge = "Se modificó correctamente";
                }
                var body = new StringContent(JsonConvert.SerializeObject(state), Encoding.UTF8, "application/json");
                var result = await RequestsApi.SendURIAsync("/api/Countries/1/States/" + ID, method, Auth.Login.Token, body);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }
                return Ok(mesagge);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Clasifications
        [HttpGet("Clasifications/Get/{id}")]
        public async Task<IActionResult> GetClasifications([FromRoute] int id, [FromQuery] bool isToSelect = false)
        {
            try
            {
                var ID = "";
                dynamic data = null;
                if (id != 0)
                {
                    ID = id.ToString();
                }
                var result = await RequestsApi.SendURIAsync("/api/Clasifications/" + ID, HttpMethod.Get, Auth.Login.Token);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }

                if (id != 0)
                {
                    data = JsonConvert.DeserializeObject<ClasificationVM>(result);
                    return Ok(data);
                } 
                else if (isToSelect)
                {
                    data = JsonConvert.DeserializeObject<List<ClasificationVM>>(result);
                    return Ok(data);
                }
                else
                {
                    ViewData["Title"] = "Clasificacion";
                    data = JsonConvert.DeserializeObject<List<ClasificationVM>>(result);
                    return View("~/Views/Catalogs/ClasificationsCatalog.cshtml", data);
                }

            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpPost("Clasifications/Post")]
        public async Task<IActionResult> CreateEditClasification(IFormCollection data)
        {
            try
            {
                var method = HttpMethod.Post;
                var ID = "";
                var mesagge = "Se agregó correctamente";
                var clasification = new ClasificationVM()
                {
                    Id = data.ContainsKey("id") ? Convert.ToInt32(data["id"].ToString()) : 0,
                    Name = data["name"].ToString(),
                };
                if (clasification.Id != 0)
                {
                    method = HttpMethod.Put;
                    ID = clasification.Id.ToString();
                    mesagge = "Se modificó correctamente";
                }
                var body = new StringContent(JsonConvert.SerializeObject(clasification), Encoding.UTF8, "application/json");
                var result = await RequestsApi.SendURIAsync("/api/Clasifications/" + ID, method, Auth.Login.Token, body);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }
                return Ok(mesagge);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Municipios
        [HttpGet("Towns/Index")]
        public IActionResult IndexTowns()
        {
            ViewData["Title"] = "Municipios";
            return View("~/Views/Catalogs/TownsCatalog.cshtml");
        }

        [HttpGet("Towns/SearchTownByIdState/{idState}/{idTown}")]
        public async Task<IActionResult> SearchTownByIdState([FromRoute] int idTown, [FromRoute] int idState)
        {
            try
            {
                var ID = "";
                dynamic data = null;
                if (idTown != 0)
                {
                    ID = idTown.ToString();
                }
                var result = await RequestsApi.SendURIAsync("/api/States/" + idState + "/Towns/" + ID, HttpMethod.Get, Auth.Login.Token);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }

                if (idTown != 0)
                {
                    data = JsonConvert.DeserializeObject<TownVM>(result);
                    return Ok(data);
                }
                else
                {
                    data = JsonConvert.DeserializeObject<List<TownVM>>(result);
                    return Ok(data);
                }

            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpPost("Towns/Post/{idState}")]
        public async Task<IActionResult> CreateEditTowns(IFormCollection data, [FromRoute] int idState)
        {
            try
            {
                var method = HttpMethod.Post;
                var ID = "";
                var mesagge = "Se agregó correctamente";
                var town = new TownVM()
                {
                    Id = data.ContainsKey("id") ? Convert.ToInt32(data["id"].ToString()) : 0,
                    Name = data["name"].ToString(),
                };
                if (town.Id != 0)
                {
                    method = HttpMethod.Put;
                    ID = town.Id.ToString();
                    mesagge = "Se modificó correctamente";
                }
                var body = new StringContent(JsonConvert.SerializeObject(town), Encoding.UTF8, "application/json");
                var result = await RequestsApi.SendURIAsync("/api/States/" + idState + "/Towns/" + ID, method, Auth.Login.Token, body);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }
                return Ok(mesagge);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Colonias
        [HttpGet("Suburbs/Index")]
        public IActionResult IndexSuburbs()
        {
            ViewData["Title"] = "Colonias";
            return View("~/Views/Catalogs/SuburbsCatalog.cshtml");
        }

        [HttpGet("Suburbs/SearchSuburbByIdTown/{idTown}/{idSuburb}")]
        public async Task<IActionResult> SearchSuburbs([FromRoute] int idSuburb, [FromRoute] int idTown)
        {
            try
            {
                var ID = "";
                dynamic data = null;
                if (idSuburb != 0)
                {
                    ID = idSuburb.ToString();
                }
                var result = await RequestsApi.SendURIAsync("/api/Towns/" + idTown + "/Suburbs/" + ID, HttpMethod.Get, Auth.Login.Token);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }

                if (idSuburb != 0)
                {
                    data = JsonConvert.DeserializeObject<SuburbVM>(result);
                    return Ok(data);
                }
                else
                {
                    data = JsonConvert.DeserializeObject<List<SuburbVM>>(result);
                    return Ok(data);
                }

            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpPost("Suburbs/Post/{idTown}")]
        public async Task<IActionResult> CreateEditSuburb(IFormCollection data, [FromRoute] int idTown)
        {
            try
            {
                var method = HttpMethod.Post;
                var ID = "";
                var mesagge = "Se agregó correctamente";
                var suburb = new SuburbVM()
                {
                    Id = data.ContainsKey("id") ? Convert.ToInt32(data["id"].ToString()) : 0,
                    Name = data["name"].ToString(),
                    ClasificationsId = Convert.ToInt32(data["clasification"].ToString()),
                    RegionsId = Convert.ToInt32(data["region"].ToString()),
                };
                if (suburb.Id != 0)
                {
                    method = HttpMethod.Put;
                    ID = suburb.Id.ToString();
                    mesagge = "Se modificó correctamente";
                }
                var body = new StringContent(JsonConvert.SerializeObject(suburb), Encoding.UTF8, "application/json");
                var result = await RequestsApi.SendURIAsync("/api/Towns/" + idTown + "/Suburbs/" + ID, method, Auth.Login.Token, body);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }
                return Ok(mesagge);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion
    }
}
