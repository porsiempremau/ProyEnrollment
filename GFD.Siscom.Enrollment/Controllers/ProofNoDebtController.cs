using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GFD.Siscom.Enrollment.Middleware;
using GFD.Siscom.Enrollment.Models;
using GFD.Siscom.Enrollment.Utilities.GeneraPDF;
using GFD.Siscom.Enrollment.Utilities.Parameters;
using GFD.Siscom.Enrollment.Utilities.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GFD.Siscom.Enrollment.Controllers
{
    [Auth()]
    public class ProofNoDebtController : Controller
    {
        private readonly IOptions<BaseModel> appSettings;
        private RequestApi RequestsApi { get; set; }
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        public Controller OtherController = null;

        public ProofNoDebtController(IOptions<BaseModel> app, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironmen)
        {
            appSettings = app;
            RequestsApi = new RequestApi(appSettings.Value.WebApiBaseUrl);
            _hostingEnvironment = hostingEnvironmen;
        }

        [HttpPost("Agreement/GeneraProofNoDebt")]
        public async Task<IActionResult> GeneraProofNoDebt([FromBody] object data)
        {
            try
            {
                PDFGenerator pdf = new PDFGenerator(_hostingEnvironment, OtherController??this);
                var filename = await pdf.GerneraProofNoDebt(data);
                return Ok(filename);
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }
    }
}
