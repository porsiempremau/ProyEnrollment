using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
    public class AgreementController : Controller
    {
        private readonly IOptions<BaseModel> appSettings; 
        private RequestApi RequestsApi { get; set; }
        public AgreementController(IOptions<BaseModel> app)
        {
            appSettings = app;
            RequestsApi = new RequestApi(appSettings.Value.WebApiBaseUrl);
        }
        [HttpGet("Agreement/List")]
        public IActionResult Index([FromQuery] string page = "agreements")
        {
            if (Plataform.IsAyuntamiento)
            {
                ViewData["Title"] = "Predios";
            }
            else
            {
                ViewData["Title"] = "Contratos";
            }
            
            return View("~/Views/Agreements/ListAgreement.cshtml", new { page = page });
        }

        [HttpGet("Agreement/Get/List")]
        public async Task<IActionResult> GetAgreementList([FromQuery] string name = "", [FromQuery] string account = "",
            [FromQuery] string rfc = "", [FromQuery] string address = "")
        {
            try
            {
                var type = 0;
                var StringSearch = "";

                if (account != "")
                {
                    type = 1;
                    StringSearch = account;
                }
                if (name != "")
                {
                    type = 2;
                    StringSearch = name;
                }
                if (address != "")
                {
                    type = 3;
                    StringSearch = address;
                }
                if (rfc != "")
                {
                    type = 4;
                    StringSearch = rfc;
                }

                var url = "/api/Agreements/FindAgreementParam?Type=" + type + "&StringSearch=" + StringSearch;
                var result = await RequestsApi.SendURIAsync(url, HttpMethod.Get, Auth.Login.Token);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }
                var data = JsonConvert.DeserializeObject<List<AgreementListVM>>(result);
                return Ok(data);

            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpGet("Agreement/EditCreateView")]
        public IActionResult EditCreateView([FromQuery] int idAgreement = 0)
        {
            return View("~/Views/Agreements/AgreementCreateEdit.cshtml", new { id = idAgreement });
        }

        [HttpGet("Agreement/GetData")]
        public async Task<IActionResult> GetData()
        {
            try
            {
                var result = await RequestsApi.SendURIAsync("/api/Agreements/GetData", HttpMethod.Get, Auth.Login.Token);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }
                return Ok(JsonConvert.DeserializeObject<AgreementDataVM>(result));
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpPost("Agreement/CreateEdit/{id}")]
        public async Task<IActionResult> CreateEditAgreement([FromBody] AgreementVM agreement, [FromRoute] int id = 0)
        {
            try
            {
                var method = HttpMethod.Post;
                var idAgreement = "";
                var url = "/api/Agreements/NewPost";
                var messagge = "";
                if (id != 0)
                {
                    method = HttpMethod.Put;
                    url = "/api/Agreements/" + idAgreement;
                    idAgreement = id.ToString();
                    messagge = Plataform.IsAyuntamiento ? "Predio actualizado correctamente" : "Contrato actualizado correctamente";
                }
                var body = new StringContent(JsonConvert.SerializeObject(agreement), Encoding.UTF8, "application/json");
                var result = await RequestsApi.SendURIAsync(url, method, Auth.Login.Token, body);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }
                if (id != 0)
                {
                    return Ok(messagge);
                }
                else
                {
                    return Ok(result);
                }
                

            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpGet("FoundAgreement/{agreementId}")]
        public async Task<IActionResult> FoundAgreement([FromRoute] int agreementId)
        {
            var response = await RequestsApi.SendURIAsync("/api/Agreements/FoundAgreement/" + agreementId, HttpMethod.Get, Auth.Login.Token);
            if (response.Contains("error"))
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        [HttpGet("Agreement/SearchTaxUserByName/{name}")]
        public async Task<IActionResult> SearchTaxUserByName([FromRoute] string name)
        {
            try
            {
                var result = await RequestsApi.SendURIAsync("/api/TaxUsers/Search/" + name, HttpMethod.Get, Auth.Login.Token);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }
                return Ok(JsonConvert.DeserializeObject<List<TaxUserVM>>(result));

            }
            catch (Exception e)
            {
                return null;
            }
        }
        
        [HttpGet("Agreement/ComparateAccount/{account}/{idTypeConsume}")]
        public async Task<IActionResult> ComparateAccount([FromRoute] string account, [FromRoute] int idTypeConsume)
        {
            try
            {
                var result = await RequestsApi.SendURIAsync("/api/Agreements/comparateAccount/" + account + "/" + idTypeConsume, HttpMethod.Get, Auth.Login.Token);
                if (result.Contains("Ya existe"))
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

        [HttpGet("Agreement/GetSelected/{id}")]
        public async Task<IActionResult> GetSelected([FromRoute] int id)
        {
            try
            {
                var result = await RequestsApi.SendURIAsync("/api/Agreements/" + id, HttpMethod.Get, Auth.Login.Token);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }
                return Ok(JsonConvert.DeserializeObject<object>(JsonConvert.SerializeObject(result)));

            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpPost("Agreement/CreateEditAgreementDetail/{AgreementId}/{idAgreeDetail}")]
        public async Task<IActionResult> CreateEditAgreementDetail([FromBody] AgreementDetailVM agreementDetail, [FromRoute] int AgreementId, [FromRoute] int idAgreeDetail = 0)
        {
            try
            {
                var method = HttpMethod.Post;
                var url = "";
                var messagge = Plataform.IsAyuntamiento ? "Detalles del predio agregados correctamente" : "Detalles del contrato agregados correctamente";
                if (idAgreeDetail != 0)
                {
                    method = HttpMethod.Put;
                    url = "/api/AgreementDetails/" + idAgreeDetail;
                    messagge = Plataform.IsAyuntamiento ? "Detalles del predio actualizados correctamente" : "Detalles del contrato actualizados correctamente";
                }
                else
                {
                    url = "/api/AgreementDetails?AgreementId=" + AgreementId;
                }
                var body = new StringContent(JsonConvert.SerializeObject(agreementDetail), Encoding.UTF8, "application/json");
                var result = await RequestsApi.SendURIAsync(url, method, Auth.Login.Token, body);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }
                return Ok(messagge);

            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpPut("Agreement/CreateEditClient/{AgreementId}")]
        public async Task<IActionResult> CreateEditAgreementDetail([FromBody] object client, [FromRoute] int AgreementId)
        {
            try
            {
                var body = new StringContent(client.ToString(), Encoding.UTF8, "application/json");
                var result = await RequestsApi.SendURIAsync("/api/Agreement/" + AgreementId + "/Clients", HttpMethod.Put, Auth.Login.Token, body);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }
                return Ok("Datos del cliente actualizados");

            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }

        [HttpPut("Agreement/CreateEditAddress/{AgreementId}")]
        public async Task<IActionResult> CreateEditAddress([FromBody] object addresses, [FromRoute] int AgreementId)
        {
            try
            {
                var body = new StringContent(addresses.ToString(), Encoding.UTF8, "application/json");
                var result = await RequestsApi.SendURIAsync("/api/Agreement/" + AgreementId + "/Adresses", HttpMethod.Put, Auth.Login.Token, body);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }
                return Ok("Datos de la dirección actualizados");

            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }

        [HttpPost("Agreement/SearchByNameClient")]
        public async Task<IActionResult> SearchByNameClient([FromBody] object nombre)
        {
            try
            {
                var body = new StringContent(nombre.ToString(), Encoding.UTF8, "application/json"); 
                var result = await RequestsApi.SendURIAsync("/api/Agreements/SearchByNameClient", HttpMethod.Post, Auth.Login.Token, body);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }
                var response = JsonConvert.DeserializeObject<List<ClientVM>>(result);
                return Ok(response);

            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpGet("Agreement/record")]
        public async Task<IActionResult> getRecord([FromQuery] int idAgreement, [FromQuery] string tipo = "Pagos")
        {
            try
            {
                //string tipo = "Pagos";
                List<DebtVM> responseRecibos = new List<DebtVM>();
                List<NotificationVM> responseNotificacion = new List<NotificationVM>();
                List<PrepaidVM> responseAnticipos = new List<PrepaidVM>();
                List<PaymentVM> responsePagos = new List<PaymentVM>();
                switch (tipo)
                {
                    case "Recibos":
                        var resultRecibos = await RequestsApi.SendURIAsync($"/api/Debts/GetDebtByPeriod/{idAgreement}", HttpMethod.Get, Auth.Login.Token);
                        if (resultRecibos.Contains("error"))
                        {
                            return Conflict(resultRecibos);
                        }
                        responseRecibos = JsonConvert.DeserializeObject<List<DebtVM>>(resultRecibos);
                        return Ok(responseRecibos);
                        break;
                    case "Notificaciones":
                        var resultNotifi = await RequestsApi.SendURIAsync($"/api/Notifications/{idAgreement}", HttpMethod.Get, Auth.Login.Token);
                        if (resultNotifi.Contains("error"))
                        {
                            return Conflict(resultNotifi);
                        }
                        responseNotificacion = JsonConvert.DeserializeObject<List<NotificationVM>>(resultNotifi);
                        return Ok(responseNotificacion);
                        break;
                    case "Anticipos":
                        var resultAnticipos = await RequestsApi.SendURIAsync($"/api/Prepaid/{idAgreement}", HttpMethod.Get, Auth.Login.Token);
                        if (resultAnticipos.Contains("error"))
                        {
                            return Conflict(resultAnticipos);
                        }
                        responseAnticipos = JsonConvert.DeserializeObject<List<PrepaidVM>>(resultAnticipos);
                        return Ok(responseAnticipos);
                        break;
                    default: //pagos
                        var resultPagos = await RequestsApi.SendURIAsync($"/api/PaymentHistory/{idAgreement}", HttpMethod.Get, Auth.Login.Token);
                        if (resultPagos.Contains("error"))
                        {
                            return Conflict(resultPagos);
                        }
                        responsePagos = JsonConvert.DeserializeObject<List<PaymentVM>>(resultPagos);
                        return Ok(responsePagos);
                        break;
                }                
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("Agreement/RunAccountSimulation/{account}")]
        public async Task<IActionResult> RunAccountSimulation([FromRoute] string account)
        {
            try
            {
                //var body = new StringContent(nombre.ToString(), Encoding.UTF8, "application/json");
                var result = await RequestsApi.SendURIAsync("/api/StoreProcedure/runAccountSimulation/" + account, HttpMethod.Post, Auth.Login.Token);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }
                var response = JsonConvert.DeserializeObject<List<SimulationCalPredialVM>>(result);
                return Ok(response);

            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpGet("pdf")]
        public IActionResult PDF()
        {
            ViewData["Title"] = "PDF";
            return View("~/Views/Agreements/EstadoDeCuentaPDF.cshtml");
        }
    }
}
