using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GFD.Siscom.Enrollment.Models;
using GFD.Siscom.Enrollment.Utilities.Auth;
using GFD.Siscom.Enrollment.Utilities.GeneraPDF;
using GFD.Siscom.Enrollment.Utilities.Parameters;
using GFD.Siscom.Enrollment.Utilities.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace GFD.Siscom.Enrollment.Controllers
{
    public class ConveniosController : Controller
    {
        private readonly IOptions<BaseModel> appSettings;
        private RequestApi RequestsApi { get; set; }
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        public Controller OtherController = null;
        public ConveniosController(IOptions<BaseModel> app, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            appSettings = app;
            RequestsApi = new RequestApi(appSettings.Value.WebApiBaseUrl);
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("Convenios/Index")]
        public ActionResult Index()
        {
            ViewData["Title"] = "Convenios de Pago";
            return View("~/Views/Convenios/ConveniosIndex.cshtml");
        }

        [HttpGet("Convenios/FindByParameter")]
        public async Task<IActionResult> FindByParameter([FromQuery] string folio = "", [FromQuery] string account = "", [FromQuery] int agreementId = 0)
        {
            try
            {
                var url = "/api/PartialPayment/FindPartialPayment";
                if (agreementId > 0)
                {
                    url = url + "ToAgree/" + agreementId;
                }
                if (folio != "")
                {
                    url = url + "Folio/" + folio;
                }
                if (account != "")
                {
                    url = url + "Agreement/" + account + "/0";
                }
                var result = await RequestsApi.SendURIAsync(url, HttpMethod.Get, Auth.Login.Token);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }
                //var data = JsonConvert.DeserializeObject<List<PartialPaymentAgreementVM>>(result);
                return Ok(result);
            }
            catch (Exception e)
            {
                var messageError = e.InnerException != null ? e.InnerException.ToString() : e.Message.ToString();
                return Conflict(messageError);
            }
        }

        #region CREAR CONVENIO
        [HttpGet("Convenios/CrearConvenioView")]
        public ActionResult CrearConvenioView()
        {
            ViewData["Title"] = "Generar Convenio";
            return View("~/Views/Convenios/CreateConvenio.cshtml");
        }

        [HttpPost("Convenios/PostConvenio")]
        public async Task<IActionResult> PostConvenio([FromBody] object partialPayment)
        {
            try
            {
                var body = new StringContent(partialPayment.ToString(), Encoding.UTF8, "application/json");
                var result = await RequestsApi.SendURIAsync("/api/PartialPayment/createPartialPayment/", HttpMethod.Post, Auth.Login.Token, body);
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
        #endregion

        #region GENERAR FORMATO PDF CONVENIO
        [HttpPost("Convenios/GeneraPDFConvenio")]
        public async Task<IActionResult> GeneraPDFConvenio([FromBody] PartialPaymentAgreementVM _partialPayment)
        {
            var a = await RequestsApi.SendURIAsync("/api/Agreements/" + _partialPayment.AgreementId, HttpMethod.Get, Auth.Login.Token);
            var ad = await RequestsApi.SendURIAsync("/api/Agreement/" + _partialPayment.AgreementId + "/Adresses", HttpMethod.Get, Auth.Login.Token);
            var p = await RequestsApi.SendURIAsync("/api/PartialPayment/FindPartialPaymentAmount/" + _partialPayment.idPartialPayment, HttpMethod.Get, Auth.Login.Token);
            AgreementVM _agreement = JsonConvert.DeserializeObject<AgreementVM>(a);
            List<AddressVM> _address = JsonConvert.DeserializeObject<List<AddressVM>>(ad);
            _agreement.Adresses = _address;
            var _partialPaymentDetails = JsonConvert.DeserializeObject<List<PaymentDetailConvenioVM>>(p);
            PDFGenerator pdf = new PDFGenerator(_hostingEnvironment, OtherController ?? this);
            var fileName = await pdf.GeneraConvenio(new { partialPayment = _partialPayment, partialPaymentDetails = _partialPaymentDetails, agreement = _agreement });
            return Ok(fileName);
        }
        #endregion

        #region CANCELAR CONVENIO
        [HttpPost("Convenios/CancelarConvenio/{agreementId}")]
        public async Task<IActionResult> CancelarConvenio([FromRoute] int agreementId)
        {
            try
            {
                var result = await RequestsApi.SendURIAsync("/api/PartialPayment/cancelPartialPayment/" + agreementId, HttpMethod.Post, Auth.Login.Token);
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
        #endregion

        [HttpGet("Convenios/GetPartialPaymentDetails/{idPartialPayment}")]
        public async Task<IActionResult> GetPartialPaymentDetails([FromRoute] int idPartialPayment = 0)
        {
            try
            {
                var result = await RequestsApi.SendURIAsync("/api/PartialPayment/FindPartialPaymentAmount/" + idPartialPayment, HttpMethod.Get, Auth.Login.Token);
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

        [HttpPost("Convenios/BillingPartialPayment/{agreementId}")]
        public async Task<IActionResult> BillingPartialPayment([FromRoute] int agreementId)
        {
            try
            {
                var result = await RequestsApi.SendURIAsync("/api/PartialPayment/billingPartialPayment/" + agreementId, HttpMethod.Post, Auth.Login.Token);
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
