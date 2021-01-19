using System;
using System.Collections.Generic;
using System.IO;
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
using Newtonsoft.Json;

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

        [HttpPost("Discounts/AddDiscountToAgreement/{AgreementId}")]
        public async Task<IActionResult> AddDiscount(IFormCollection data, [FromRoute] int AgreementId)
        {
            try
            {
                IFormFile File = null;
                File = data.Files["file"];
                var uploadFile = await UploadFileVulnerableGroup(File, AgreementId);
                //if (uploadFile.Contains("exito"))
                //{
                    var objectGV = new DiscountGroupVulnerable()
                    {
                        agreementId = AgreementId,
                        discountId = Convert.ToInt32(data["nameDiscount"].ToString()),
                        userId = Auth.Login.User,
                        isActive = true,
                        observation = "DESCUENTO"
                    };
                    var body = new StringContent(JsonConvert.SerializeObject(objectGV), Encoding.UTF8, "application/json");
                    var result = await RequestsApi.SendURIAsync("/api/Agreements/AddDiscount", HttpMethod.Post, Auth.Login.Token, body);
                    if (result.Contains("error"))
                    {
                        return Conflict(result);
                    }

                    return Ok(result);
                //}

                //return Conflict(uploadFile);
                
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }

        public async Task<string> UploadFileVulnerableGroup(IFormFile file, int AgreementId)
        {
            try
            {                
                MultipartFormDataContent multiContent = new MultipartFormDataContent();
                if (file != null && file.Length > 0)
                {
                    byte[] data;
                    using (var br = new BinaryReader(file.OpenReadStream()))
                        data = br.ReadBytes((int)file.OpenReadStream().Length);

                    ByteArrayContent bytes = new ByteArrayContent(data);                    
                    multiContent.Add(bytes, "file", file.FileName);                    
                }

                var body = new StringContent(JsonConvert.SerializeObject(file), Encoding.UTF8, "application/json");
                var result = await RequestsApi.UploadImageToServer("/api/FileUpload/" + AgreementId + "/FAG01/Descuento", Auth.Login.Token, multiContent, body);
                if (result.Contains("error"))
                {
                    return "Problema para subir el archivo";
                }
                return "exito";
            }
            catch (Exception e)
            {
                return "Error: " + e.Message;
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

    public class DiscountGroupVulnerable
    {
        public int agreementId { get; set; }
        public int discountId { get; set; }
        public string userId { get; set; }
        public bool isActive { get; set; }
        public string observation { get; set; }
    }
}
