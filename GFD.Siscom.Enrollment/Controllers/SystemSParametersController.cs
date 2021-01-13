using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    public class SystemSParametersController : Controller
    {
        private readonly IOptions<BaseModel> appSettings;
        private RequestApi RequestsApi { get; set; }
        public SystemSParametersController(IOptions<BaseModel> app)
        {
            appSettings = app;
            RequestsApi = new RequestApi(appSettings.Value.WebApiBaseUrl);
        }
        
        [HttpGet("SystemsParameters/GetAll")]
        public async Task<IActionResult> GetAllSystemsParameters()
        {
            try
            {
                var result = await RequestsApi.SendURIAsync("/api/ValueParameters/GetAllParameters", HttpMethod.Get, Auth.Login.Token);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }
                var data = JsonConvert.DeserializeObject<List<SystemsParametersVM>>(result);
                return Ok(data);
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }

       
    }
}
