using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GFD.Siscom.Enrollment.Middleware;
using GFD.Siscom.Enrollment.Models;
using GFD.Siscom.Enrollment.Utilities.Auth;
using GFD.Siscom.Enrollment.Utilities.Parameters;
using GFD.Siscom.Enrollment.Utilities.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace GFD.Siscom.Enrollment.Controllers
{
    [Auth()]
    public class TaxUsersController : Controller
    {
        private readonly IOptions<BaseModel> appSettings;
        private RequestApi RequestsApi { get; set; }
        public TaxUsersController(IOptions<BaseModel> app)
        {
            appSettings = app;
            RequestsApi = new RequestApi(appSettings.Value.WebApiBaseUrl);
        }

        [HttpGet("TaxUser/GetTaxUser/{id}")]
        public async Task<IActionResult> GetTaxUser([FromRoute] int id)
        {
            try
            {
                var result = await RequestsApi.SendURIAsync("/api/TaxUsers/" + id, HttpMethod.Get, Auth.Login.Token);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }
                var data = JsonConvert.DeserializeObject<TaxUserVM>(result);
                return Ok(data);
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }

        [HttpPut("TaxUser/EditTaxUser/{idTaxUser}")]
        public async Task<IActionResult> EditTaxUser([FromBody] TaxUserVM taxUser, [FromRoute] int idTaxUser)
        {
            try
            {
                var body = new StringContent(JsonConvert.SerializeObject(taxUser), Encoding.UTF8, "application/json");
                var result = await RequestsApi.SendURIAsync("/api/TaxUsers/" + idTaxUser, HttpMethod.Put, Auth.Login.Token, body);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }

        [HttpPut("TaxAddress/EditTaxAddress/{idTaxAddress}")]
        public async Task<IActionResult> EditTaxAddress([FromBody] TaxAddressVM taxAddress, [FromRoute] int idTaxAddress)
        {
            try
            {
                var body = new StringContent(JsonConvert.SerializeObject(taxAddress), Encoding.UTF8, "application/json");
                var result = await RequestsApi.SendURIAsync("/api/TaxAddress/" + idTaxAddress, HttpMethod.Put, Auth.Login.Token, body);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }

        [HttpGet("TaxUser/GetListTaxUsers/Index")]
        public IActionResult GetListTaxUsersIndex()
        {
            ViewData["Title"] = "Registro de Contribuyentes";
            return View("~/Views/Ordenes/ListTaxUsers/ListTaxUsers.cshtml");
        }

        [HttpGet("TaxUser/GetTaxUserByParameters")]
        public async Task<IActionResult> GetTaxUserByParameters([FromQuery] string value = "", [FromQuery] bool isProvider = false, [FromQuery] bool isName = true)
        {
            try
            {
                var url = isName ? "/api/OrderSales/Name/" + value + "/" : "/api/OrderSales/RFC/" + value + "/";
                var provider = isProvider ? 2 : 1;
                var result = await RequestsApi.SendURIAsync(url + provider, HttpMethod.Get, Auth.Login.Token);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }
                var data = JsonConvert.DeserializeObject<List<TaxUserVM>>(result);
                return Ok(data);
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }
    }
}
