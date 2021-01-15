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
    public class DiscountsController : Controller
    {
        private readonly IOptions<BaseModel> appSettings;
        private RequestApi RequestsApi { get; set; }
        public DiscountsController(IOptions<BaseModel> app)
        {
            appSettings = app;
            RequestsApi = new RequestApi(appSettings.Value.WebApiBaseUrl);
        }
        // GET: DiscountsController
        [HttpGet("Discounts/AgreementList")]
        public ActionResult Index([FromQuery] string page = "discounts")
        {
            ViewData["Title"] = "Descuento Población Vulnerable";
            return View("~/Views/Agreements/ListAgreement.cshtml", new { page = page});
        }

        [HttpGet("Discounts/ViewDiscountVulnerable")]
        public ActionResult ViewDiscountVulnerable()
        {
            ViewData["Title"] = "Alta Descuento Población Vulnerable";
            return View("~/Views/Discounts/DiscountVulnerable.cshtml");
        }

        [HttpPost("Discounts/AddDiscountToAgreement")]
        public async Task<IActionResult> AddDiscount([FromBody] object data)
        {
            try
            {
                var body = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
                var result = await RequestsApi.SendURIAsync("/api/Agreements/AddDiscount", HttpMethod.Post, Auth.Login.Token, body);
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

        [HttpPost("Discounts/AddDiscountToDebt/{idAgreement}")]
        public async Task<IActionResult> AddDiscountToDebt([FromRoute] int idAgreement)
        {
            try
            {
                var body = new StringContent("", Encoding.UTF8, "application/json");
                var result = await RequestsApi.SendURIAsync("/api/Agreements/addDiscountDebt/" + idAgreement, HttpMethod.Post, Auth.Login.Token, body);
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

        [HttpPost("Discounts/ReverseVulnerable/{idAgreement}")]
        public async Task<IActionResult> ReverseVulnerable([FromRoute] int idAgreement)
        {
            try
            {
                var result = await RequestsApi.SendURIAsync("/api/Agreements/reverseVulnerable/" + idAgreement, HttpMethod.Post, Auth.Login.Token);
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
