using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
    public class AgreementController : Controller
    {
        private readonly IOptions<BaseModel> appSettings; 
        private RequestApi RequestsApi { get; set; }
        public AgreementController(IOptions<BaseModel> app)
        {
            appSettings = app;
            RequestsApi = new RequestApi(appSettings.Value.WebApiBaseUrl);
        }
        [HttpGet("Agreement/List")]
        public IActionResult Index()
        {
            return View("~/Views/Agreements/ListAgreement.cshtml");
        }

        [HttpGet("Agreement/Get/List")]
        public async Task<IActionResult> GetAgreementList([FromQuery] string name = "", [FromQuery] string account = "",
            [FromQuery] string rfc = "", [FromQuery] string address = "")
        {
            try
            {
                var type = 0;
                var StringSearch = "";

                if (account != "")
                {
                    type = 1;
                    StringSearch = account;
                }
                if (name != "")
                {
                    type = 2;
                    StringSearch = name;
                }
                if (address != "")
                {
                    type = 3;
                    StringSearch = address;
                }
                if (rfc != "")
                {
                    type = 4;
                    StringSearch = rfc;
                }

                var url = "/api/Agreements/FindAgreementParam?Type=" + type + "&StringSearch=" + StringSearch;
                var result = await RequestsApi.SendURIAsync(url, HttpMethod.Get, Auth.Login.Token);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }
                var data = JsonConvert.DeserializeObject<List<AgreementListVM>>(result);
                return Ok(data);

            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpGet("Agreement/EditCreateView")]
        public IActionResult EditCreateView()
        {
            return View("~/Views/Agreements/AgreementCreateEdit.cshtml");
        }

        [HttpGet("Agreement/GetData")]
        public async Task<IActionResult> GetData()
        {
            try
            {
                var result = await RequestsApi.SendURIAsync("/api/Agreements/GetData", HttpMethod.Get, Auth.Login.Token);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }
                return Ok(JsonConvert.DeserializeObject<AgreementDataVM>(result));
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpPost("Agreement/CreateEdit")]
        public async Task<IActionResult> CreateEditAgreement([FromBody] AgreementVM agreement)
        {
            try
            {
                var body = new StringContent(JsonConvert.SerializeObject(agreement), Encoding.UTF8, "application/json");
                var result = await RequestsApi.SendURIAsync("/api/Agreements/", HttpMethod.Post, Auth.Login.Token, body);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }
                return Ok(result);

            }
            catch (Exception e)
            {
                return null;
            }
        }


    }
}
