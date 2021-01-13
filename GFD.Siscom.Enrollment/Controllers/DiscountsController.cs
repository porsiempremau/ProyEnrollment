using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
