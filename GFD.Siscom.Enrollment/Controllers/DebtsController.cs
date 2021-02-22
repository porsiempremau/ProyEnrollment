using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GFD.Siscom.Enrollment.Middleware;
using GFD.Siscom.Enrollment.Models;
using GFD.Siscom.Enrollment.Utilities;
using GFD.Siscom.Enrollment.Utilities.Auth;
using GFD.Siscom.Enrollment.Utilities.GeneraPDF;
using GFD.Siscom.Enrollment.Utilities.Parameters;
using GFD.Siscom.Enrollment.Utilities.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace GFD.Siscom.Enrollment.Controllers
{
    [Auth]
    public class DebtsController : Controller
    {
        private readonly IOptions<BaseModel> appSettings;
        private RequestApi RequestsApi { get; set; }
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        public Controller OtherController = null;
        public DebtsController(IOptions<BaseModel> app, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironmen)
        {
            appSettings = app;
            RequestsApi = new RequestApi(appSettings.Value.WebApiBaseUrl);
            this._hostingEnvironment = hostingEnvironmen;
        }
        // GET: DebtsController
        public ActionResult Index()
        {
            return View();
        }

        [Role("Admin|Supervisor|Super|Isabi")]
        [HttpGet("Debt/DebtsAgreement/{idAgreement}")]
        public async Task<IActionResult> SearchByNameClient([FromRoute] int idAgreement)
        {
            try
            {
                var result = await RequestsApi.SendURIAsync("/api/Debts/" + idAgreement, HttpMethod.Get, Auth.Login.Token);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }
                var response = JsonConvert.DeserializeObject<List<DebtVM>>(result);
                return Ok(response);

            }
            catch (Exception e)
            {
                return null;
            }
        }

        [Role("Admin|Supervisor|Super|Isabi")]
        [HttpPost("Debt/AddDebtToAgreement")]
        public async Task<IActionResult> AddDebtToAgreement([FromQuery] int AgreementId = 0, [FromQuery] int month = 0, [FromQuery] int year = 0)
        {
            try
            {
                var status = "ED001";
                var url = "";
                var body = new StringContent(JsonConvert.SerializeObject(""), Encoding.UTF8, "application/json");
                if (Plataform.IsAyuntamiento)
                {
                    url = "/api/Agreements/addDebtAyuntamiento/" + AgreementId;
                }
                else
                {
                    url = "/api/Agreements/addDebtSosapac/" + AgreementId + "/" + month + "/" + year + "/" + status;
                }
                var result = await RequestsApi.SendURIAsync(url, HttpMethod.Post, Auth.Login.Token, body);
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

        [Role("Admin|Supervisor|Super|Isabi")]
        [HttpGet("Debt/GeneraPDF/{agreementId}")]
        public async Task<IActionResult> GeneraPDF([FromRoute] int agreementId)
        {
            try
            {
                var resultDebt = await RequestsApi.SendURIAsync("/api/Debts/" + agreementId, HttpMethod.Get, Auth.Login.Token);
                var resultAgreement = await RequestsApi.SendURIAsync("/api/Agreements/" + agreementId, HttpMethod.Get, Auth.Login.Token);
                
                var debts = JsonConvert.DeserializeObject<List<DebtVM>>(resultDebt);
                var agreements = JsonConvert.DeserializeObject<Agreement>(resultAgreement);
                PDFGenerator pdf = new PDFGenerator(_hostingEnvironment, OtherController??this);
                var filename = await pdf.GerneraEstadoDeCuenta(new { agreement = agreements, debt = debts});
                return Ok(filename);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpGet("Debt/DownloadPDF/{fileName}")]
        public async Task<IActionResult> PrintPDF([FromRoute] string fileName)
        {
            try
            {
                var memory = new MemoryStream();
                using (var stream = new FileStream(Path.Combine(_hostingEnvironment.WebRootPath, fileName), FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                System.IO.File.Delete(Path.Combine(_hostingEnvironment.WebRootPath, fileName));
                return File(memory, "application/pdf", fileName);
            }
            catch (Exception ex)
            {
                return Conflict("Error al descargar el archivo. " + ex.Message);
            }
        }
    }
}
