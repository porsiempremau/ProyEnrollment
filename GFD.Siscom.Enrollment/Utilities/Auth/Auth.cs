using GFD.Siscom.Enrollment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GFD.Siscom.Enrollment.Utilities.Auth
{
    public static class Auth
    {
        static IServiceProvider services = null;

        public static IServiceProvider Services
        {
            get { return services; }
            set
            {
                if (services != null)
                {
                    throw new Exception("No se puede establecer una vez que ya se ha establecido un valor.");
                }
                services = value;
            }
        }

        public static LoginVM Login
        {
            get
            {
                IHttpContextAccessor httpContextAccessor = services.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;

                var httpContext = httpContextAccessor.HttpContext;
                if (httpContext.Session == null)
                {
                    return null;
                }
                var login = httpContext.Session.GetString("Login");
                if (login == null)
                {
                    return null;
                }

                return JsonConvert.DeserializeObject<LoginVM>(login.ToString());
            }
        }

        public static List<SystemsParametersVM> ParametrosSistema
        {
            get
            {

                IHttpContextAccessor httpContextAccessor = services.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;

                var httpContext = httpContextAccessor.HttpContext;
                if (httpContext.Session == null)
                {
                    return null;
                }
                var parametrosSistema = httpContext.Session.GetString("parametrosSistema");
                if (parametrosSistema == null)
                {
                    return null;
                }

                return JsonConvert.DeserializeObject<List<SystemsParametersVM>>(parametrosSistema.ToString());

            }
        }

        public static string AppName
        {
            get
            {
                IConfiguration Configuration = services.GetService(typeof(IConfiguration)) as IConfiguration;
                var AppName = Configuration.GetValue<string>("BaseModel.AppName", "GFDSISCOM");
                return AppName;
            }
        }
    }
}
