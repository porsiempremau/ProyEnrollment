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
    public class TypeServicesController : Controller
    {
        private readonly IOptions<BaseModel> appSettings;
        private RequestApi RequestsApi { get; set; }

        public TypeServicesController(IOptions<BaseModel> app)
        {
            appSettings = app;
            RequestsApi = new RequestApi(appSettings.Value.WebApiBaseUrl);
        }

        // GET: TypeServicesController
        [HttpGet("TypeServices/Get")]
        public async Task<IActionResult> GetTypeServices()
        {
            try
            {
                var result = await RequestsApi.SendURIAsync("api/TypeServices", HttpMethod.Get, Auth.Login.Token);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }

                var data = JsonConvert.DeserializeObject<List<TypeServiceVM>>(result);
                return Ok(data);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: TypeServicesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TypeServicesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TypeServicesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TypeServicesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TypeServicesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TypeServicesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TypeServicesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
