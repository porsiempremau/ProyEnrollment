using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GFD.Siscom.Enrollment.Extensions;
using GFD.Siscom.Enrollment.Models;
using GFD.Siscom.Enrollment.Utilities.Parameters;
using GFD.Siscom.Enrollment.Utilities.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace GFD.Siscom.Enrollment.Controllers
{
    [ViewLayout("_LoginLayout")]
    public class LoginController : Controller
    {
        private readonly IOptions<BaseModel> appSettings;
        private RequestApi RequestsApi { get; set; }

        public LoginController(IOptions<BaseModel> app)
        {
            appSettings = app;
            RequestsApi = new RequestApi(appSettings.Value.WebApiBaseUrl);
        }

        //[Anonymous()]
        [HttpGet("{msg?}")]
        [HttpGet("Login/{msg?}")]
        [HttpGet("Login/Index/{msg?}")]
        public IActionResult Index([FromRoute] string msg = "")
        {
            return View("~/Views/Login/Index.cshtml", msg);
        }

        [HttpPost("Login/login")]
        public async Task<IActionResult> Index(IFormCollection collection)
        {
            string User = collection["UserEmail"].ToString();
            string Password = collection["UserPass"].ToString();
            var login = await RequestsApi.SendURIAsync("/api/Auth/login", HttpMethod.Post, new StringContent("{\"UserName\": \"" + User + "\", \"Password\": \"" + Password + "\"}", Encoding.UTF8, "application/json"));
            if (login.Contains("error"))
            {
                return Conflict(login);
            }
            else
            {
                LoginVM model = JsonConvert.DeserializeObject<LoginVM>(login);
                HttpContext.Session.SetObject<LoginVM>("Login", model);
                

                //var authFirebase = await firebaseService.LogginUser(model.Email, Password);
                //if (authFirebase != null)
                //{
                //    HttpContext.Session.SetObject<FirebaseAuthLink>("FirebaseLogin", authFirebase);
                //    var resAddItem = await firebaseService.AddItemUser(model, authFirebase, User, Password);
                //}
                //else
                //{
                //    return Conflict(login);
                //}

            }
            return Ok();
        }
    }
}
