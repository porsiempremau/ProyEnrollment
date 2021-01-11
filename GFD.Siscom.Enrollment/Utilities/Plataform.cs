using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Utilities
{
    public class Plataform
    {
        static IServiceProvider services = null;
        public static IServiceProvider Services
        {
            get { return services; }
            set
            {
                if (services != null)
                {
                    throw new Exception("Can't set once a value has already been set.");
                }
                services = value;
            }
        }


        public static bool IsAyuntamiento
        {
            get
            {
                IConfiguration configuration = services.GetService(typeof(IConfiguration)) as IConfiguration;
                var isAyuntamiento = bool.Parse(configuration.GetConnectionString("isAyuntamiento"));
                return isAyuntamiento;
            }
        }

        //public static string getUrlQr(string id)
        //{
        //    IConfiguration configuration = services.GetService(typeof(IConfiguration)) as IConfiguration;
        //    //= configuration.GetValue<BaseModel>("BaseModel");
        //    var baseModel = configuration.GetSection("BaseModel").Get<BaseModel>();
        //    string keyurl = AESEncryptionString.EncryptString(id.ToString().PadLeft(10, '0'), baseModel.IssuerName);
        //    while (keyurl.Contains("+") || keyurl.Contains("&") || keyurl.Contains("?"))
        //    {
        //        keyurl = AESEncryptionString.EncryptString(id.ToString().PadLeft(10, '0'), baseModel.IssuerName);
        //    }

        //    IHttpContextAccessor httpContextAccessor = services.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;
        //    string baseurl = "https://enlinea.cuautlancingo.gob.mx";
        //    if (!Platform.IsAyuntamiento)
        //    {
        //        baseurl = "https://enlinea.sosapac.gob.mx";
        //    }

        //    var url = $"{baseurl}/ValidAccountStatus/false/?SecuritySafe={keyurl}";


        //    return url;

        //}
    }
}
