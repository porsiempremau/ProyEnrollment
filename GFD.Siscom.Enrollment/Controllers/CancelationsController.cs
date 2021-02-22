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
    public class CancelationsController : Controller
    {
        private readonly IOptions<BaseModel> appSettings;
        private RequestApi RequestsApi { get; set; }
        public CancelationsController(IOptions<BaseModel> app)
        {
            appSettings = app;
            RequestsApi = new RequestApi(appSettings.Value.WebApiBaseUrl);
        }

        [HttpGet("Cancelations/CancelationsToAuthIndex")]
        public ActionResult CancelationsToAuthIndex([FromQuery] bool SearchByDate = false)
        {
            if (SearchByDate)
            {
                ViewData["Title"] = "Búsqueda de Cancelaciones";
            }
            else
            {
                ViewData["Title"] = "Autorización de Cancelaciones";
            }
            
            return View("~/Views/Cancelation/AuthCancelations/AuthCancelationsIndex.cshtml", new { byDate = SearchByDate });
        }

        [HttpGet("Cancelations/GetCancelations")]
        public async Task<IActionResult> GetCancelations()
        {
            try
            {
                var result = await RequestsApi.SendURIAsync("/api/TransactionCancelationRequest/", HttpMethod.Get, Auth.Login.Token);
                return Ok(result);
            }
            catch (Exception e)
            {
                var messageError = e.InnerException != null ? e.InnerException.ToString() : e.Message.ToString();
                return Conflict(messageError);
            }
        }

        [HttpGet("Cancelations/GetCancelationsTransactionId/{transactionId}")]
        public async Task<IActionResult> GetCancelationsTransactionId([FromRoute] int transactionId)
        {
            try
            {
                var result = await RequestsApi.SendURIAsync("/api/TransactionCancelationRequest/" + transactionId, HttpMethod.Get, Auth.Login.Token);
                return Ok(result);
            }
            catch (Exception e)
            {
                var messageError = e.InnerException != null ? e.InnerException.ToString() : e.Message.ToString();
                return Conflict(messageError);
            }
        }

        [HttpPost("Cancelations/PostCancelation")]
        public async Task<IActionResult> PostCancelation([FromBody] object Cancel)
        {
            try
            {
                var body = new StringContent(Cancel.ToString(), Encoding.UTF8, "application/json");
                var result = await RequestsApi.SendURIAsync("/api/TransactionCancelationRequest/", HttpMethod.Post, Auth.Login.Token, body);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                var messageError = e.InnerException != null ? e.InnerException.ToString() : e.Message.ToString();
                return Conflict(messageError);
            }
        }

    }
}
