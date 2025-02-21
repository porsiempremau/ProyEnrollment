﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GFD.Siscom.Enrollment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using GFD.Siscom.Enrollment.Utilities.Parameters;
using GFD.Siscom.Enrollment.Utilities.Services;
using GFD.Siscom.Enrollment.Middleware;
using System.Net.Http;
using GFD.Siscom.Enrollment.Utilities.Auth;

namespace GFD.Siscom.Enrollment.Controllers
{   
    [Auth]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOptions<BaseModel> appSettings;
        private RequestApi RequestsApi { get; set; }

        public HomeController(ILogger<HomeController> logger, IOptions<BaseModel> app)
        {
            _logger = logger;
            appSettings = app;
            RequestsApi = new RequestApi(appSettings.Value.WebApiBaseUrl);
        }
        
        [HttpGet("Home")]
        public IActionResult Index()
        {
            return View("~/Views/Home/Index.cshtml");
        }

        [HttpGet("Home/GetDivision")]
        public async Task<IActionResult> GetDivision ()
        {
            try
            {
                var result = await RequestsApi.SendURIAsync("/api/Division", HttpMethod.Get, Auth.Login.Token);
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
