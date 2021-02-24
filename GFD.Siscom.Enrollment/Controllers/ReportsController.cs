using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GFD.Siscom.Enrollment.Utilities.Auth;
using GFD.Siscom.Enrollment.Utilities.Parameters;
using GFD.Siscom.Enrollment.Utilities.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GFD.Siscom.Enrollment.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IOptions<BaseModel> appSettings;
        private RequestApi RequestsApi { get; set; }
        public ReportsController(IOptions<BaseModel> app)
        {
            appSettings = app;
            RequestsApi = new RequestApi(appSettings.Value.WebApiBaseUrl);
        }
        
        [HttpGet("Reports/Index")]
        public ActionResult Index([FromQuery] string report, [FromQuery] string title)
        {
            ViewData["Title"] = "Reporte de " + title;
            return View("~/Views/Reports/ReportIndex.cshtml", new { NameReport = report});
        }

        [HttpGet("Reports/GetDataToSelects")]
        public async Task<IActionResult> GetDataToSelects([FromQuery] string NameEndpoint = "")
        {
            try
            {
                var url = "/api/" + NameEndpoint;
                var result = await RequestsApi.SendURIAsync(url, HttpMethod.Get, Auth.Login.Token);
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


        [HttpPost("Reports/GetData")]
        public async Task<IActionResult> GetData([FromBody] object data, [FromQuery] string typeReport = "")
        {
            try
            {
                var url = "/api/Reports/" + typeReport;
                var body = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
                var result = await RequestsApi.SendURIAsync(url, HttpMethod.Post, Auth.Login.Token, body);
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
    }
}
