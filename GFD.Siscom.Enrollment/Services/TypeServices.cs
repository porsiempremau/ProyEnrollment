using GFD.Siscom.Enrollment.Models;
using GFD.Siscom.Enrollment.Utilities.Auth;
using GFD.Siscom.Enrollment.Utilities.Parameters;
using GFD.Siscom.Enrollment.Utilities.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Services
{
    public class TypeServices : Controller
    {
        private readonly IOptions<BaseModel> appSettings;
        private RequestApi RequestsApi { get; set; }
        public TypeServices(IOptions<BaseModel> app)
        {
            appSettings = app;
            RequestsApi = new RequestApi(appSettings.Value.WebApiBaseUrl);
        }

        [HttpGet("/TypeServices/Index")]
        public IActionResult Index()
        {
            return View("~/Views/Catalogs/TypeServices.cshtml");
        }

        [HttpGet("/TypeServices/Get")]
        public async Task<List<TypeServicesGralVM>> Get()
        {
            try
            {
                var result = await RequestsApi.SendURIAsync("/api/TypeServices", HttpMethod.Get, Auth.Login.Token);
                if (result.Contains("error"))
                {
                    return null;
                }

                var data = JsonConvert.DeserializeObject<List<TypeServicesGralVM>>(result);
                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
