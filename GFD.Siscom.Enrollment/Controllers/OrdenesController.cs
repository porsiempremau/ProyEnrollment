using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GFD.Siscom.Enrollment.Middleware;
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
    [Auth()]
    public class OrdenesController : Controller
    {
        private readonly IOptions<BaseModel> appSettings;
        private RequestApi RequestsApi { get; set; }
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        public Controller OtherController = null;

        public OrdenesController(IOptions<BaseModel> app, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironmen)
        {
            appSettings = app;
            RequestsApi = new RequestApi(appSettings.Value.WebApiBaseUrl);
            _hostingEnvironment = hostingEnvironmen;
        }
        // GET: OrdenesController
        [HttpGet("Ordenes/Index")]
        public ActionResult Index()
        {
            ViewData["Title"] = "Ordenes de Pago";
            return View("~/Views/Ordenes/OrdersIndex.cshtml");
        }

        [HttpGet("Ordenes/GetPathers/{idDivition}")]
        public async Task<IActionResult> GetPathers([FromRoute] int idDivition)
        {
            try
            {
                var result = await RequestsApi.SendURIAsync("/api/Products/Division/" + idDivition, HttpMethod.Get, Auth.Login.Token);
                //if (result.Contains("error"))
                //{
                //    return Conflict(result);
                //}
                var data = JsonConvert.DeserializeObject<List<ProductVM>>(result);
                return Ok(data);
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }

        [HttpGet("Ordenes/GetTarriff/{idProduct}")]
        public async Task<IActionResult> GetTarriff([FromRoute] int idProduct)
        {
            try
            {
                var result = await RequestsApi.SendURIAsync("/api/Products/Tariff/" + idProduct, HttpMethod.Get, Auth.Login.Token);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }
                var data = JsonConvert.DeserializeObject<TariffProduct>(result);
                return Ok(data);
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }

        [HttpGet("Ordenes/GetFactors")]
        public async Task<IActionResult> GetFactors([FromQuery] string value)
        {
            try
            {
                var result = await RequestsApi.SendURIAsync("/api/ValueParameters?value=" + value, HttpMethod.Get, Auth.Login.Token);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }
                var data = JsonConvert.DeserializeObject<SystemsParametersVM>(result);
                return Ok(data);
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }

        [HttpPost("Ordenes/CreateOrders")]
        public async Task<IActionResult> CreateOrdersTaxUser([FromBody] object data, [FromQuery] bool isWithAccount = false, [FromQuery] int idAgreement = 0)
        {
            try
            {
                var url = "/api/OrderSales";
                if (isWithAccount)
                {
                    url = "/api/Products/Agreement/" + idAgreement;
                }
                var body = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
                var result = await RequestsApi.SendURIAsync(url, HttpMethod.Post, Auth.Login.Token, body);
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

        [HttpPost("Ordenes/GeneraPDF/TaxUser")]
        public async Task<IActionResult> GeneraPDFTaxUser([FromBody] OrderSaleVM data)
        {
            try
            {
                PDFGenerator pdf = new PDFGenerator(_hostingEnvironment, OtherController??this);
                var filename = await pdf.GerneraOrderPayment(data);
                return Ok(filename);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpPost("Ordenes/GeneraPDF/WithAccount")]
        public async Task<IActionResult> GeneraPDFWithAccount([FromBody] DebtVM debt)
        {
            try
            {
                var clte = await RequestsApi.SendURIAsync("/api/Agreement/" + debt.AgreementId + "/Clients", HttpMethod.Get, Auth.Login.Token);
                var addrs = await RequestsApi.SendURIAsync("/api/Agreement/" + debt.AgreementId + "/Adresses", HttpMethod.Get, Auth.Login.Token);

                List<ClientVM> client = JsonConvert.DeserializeObject<List<ClientVM>>(clte);
                List<AddressVM> address = JsonConvert.DeserializeObject<List<AddressVM>>(addrs);
                debt.Agreement.Clients = client;
                debt.Agreement.Adresses = address;
                PDFGenerator pdf = new PDFGenerator(_hostingEnvironment, OtherController??this);
                var filename = await pdf.GerneraOrderPaymentWithAccount(debt);
                return Ok(filename);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpGet("Ordenes/SearchTaxUserByRFC/{rfc}")]
        public async Task<IActionResult> SearchTaxUserByRFC([FromRoute] string rfc)
        {
            try
            {
                var result = await RequestsApi.SendURIAsync("/api/OrderSales/RFC/" + rfc, HttpMethod.Get, Auth.Login.Token);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }
                var data = JsonConvert.DeserializeObject<TaxUserVM>(result);
                return Ok(data);
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }

        #region BUSQUEDA DE ORDENES POR FECHA
        [HttpGet("Ordenes/SearchOrderByDate/Index")]
        public IActionResult SearchOrderByDateIndex()
        {
            ViewData["Title"] = "Busqueda de Ordenes";
            return View("~/Views/Ordenes/SearchOrdersByDate/SearchOrdersByDate.cshtml");
        }

        [HttpGet("Ordenes/SearchOrderByDate/{date}")]
        public async Task<IActionResult> SearchOrderByDate([FromRoute] string date)
        {
            try
            {
                var result = await RequestsApi.SendURIAsync("/api/OrderSales/FindAllOrdersByDate/" + date, HttpMethod.Get, Auth.Login.Token);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }
                var data = JsonConvert.DeserializeObject<List<OrderSaleVM>>(result);
                return Ok(data);
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }

        [HttpGet("Ordenes/SearchOrderByFolio/{folio}")]
        public async Task<IActionResult> SearchOrderByFolio([FromRoute] string folio)
        {
            try
            {
                var result = await RequestsApi.SendURIAsync("/api/OrderSales/Folio/" + folio, HttpMethod.Get, Auth.Login.Token);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }
                var data = JsonConvert.DeserializeObject<OrderSaleVM>(result);
                return Ok(data);
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }

        [HttpGet("Ordenes/GetOrderSaleById/{id}")]
        public async Task<IActionResult> GetOrderSaleById([FromRoute] int id)
        {
            try
            {
                var result = await RequestsApi.SendURIAsync("/api/OrderSales/GetOrderSaleById?=" + id, HttpMethod.Get, Auth.Login.Token);
                if (result.Contains("error"))
                {
                    return Conflict(result);
                }
                var data = JsonConvert.DeserializeObject<OrderSaleVM>(result);
                return Ok(data);
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }        
        #endregion

    }
}
