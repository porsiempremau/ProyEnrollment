using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GFD.Siscom.Enrollment.Middleware;
using GFD.Siscom.Enrollment.Models;
using GFD.Siscom.Enrollment.Utilities;
using GFD.Siscom.Enrollment.Utilities.Auth;
using GFD.Siscom.Enrollment.Utilities.Parameters;
using GFD.Siscom.Enrollment.Utilities.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace GFD.Siscom.Enrollment.Controllers
{
    [Auth]
    public class DebtsController : Controller
    {
        private readonly IOptions<BaseModel> appSettings;
        private RequestApi RequestsApi { get; set; }
        public DebtsController(IOptions<BaseModel> app)
        {
            appSettings = app;
            RequestsApi = new RequestApi(appSettings.Value.WebApiBaseUrl);
        }
        // GET: DebtsController
        public ActionResult Index()
        {
            return View();
        }

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

        [HttpGet("Debt/EstadoDeCuenta/{agreementId}")]
        public async Task<IActionResult> GetDebtsEstadoDeCuenta([FromRoute] int agreementId)
        {
            try
            {
                var resultDebt = await RequestsApi.SendURIAsync("/api/Debts/" + agreementId, HttpMethod.Get, Auth.Login.Token);
                var resultAgreement = await RequestsApi.SendURIAsync("/api/Agreements/" + agreementId, HttpMethod.Get, Auth.Login.Token);
                var resultADetails = await RequestsApi.SendURIAsync("/api/AgreementDetails/" + agreementId, HttpMethod.Get, Auth.Login.Token);
                //if (resultDebt.Contains("error") || resultADetails.Contains("error") || resultAgreement.Contains("error"))
                //{
                //    return Conflict(resultDebt);
                //}
                var debts = JsonConvert.DeserializeObject<List<DebtVM>>(resultDebt);
                var agreements = JsonConvert.DeserializeObject<Agreement>(resultAgreement);
                var agreementDetails = JsonConvert.DeserializeObject<AgreementDetailVM>(resultADetails);
                return View("~/Views/Agreements/EstadoDeCuenta.cshtml", new { debt = debts, agreement = agreements, agreementDetail = agreementDetails });

            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
