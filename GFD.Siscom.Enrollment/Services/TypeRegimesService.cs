using GFD.Siscom.Enrollment.Utilities.Parameters;
using GFD.Siscom.Enrollment.Utilities.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Services
{
    public class TypeRegimesService
    {
        private RequestApi RequestsApi { get; set; }

        public TypeRegimesService(IOptions<BaseModel> appSettings)
        {
            RequestsApi = new RequestApi(appSettings.Value.WebApiBaseUrl);
        }

        public async Task<int> get() 
        {
            //var login = await RequestsApi.SendURIAsync("/api/Auth/login", HttpMethod.Post, new StringContent("{\"UserName\": \"" + UserEmail + "\", \"Password\": \"" + UserPass + "\"}", Encoding.UTF8, "application/json"));
            if (login.Contains("error"))
            {
                return Conflict(login);
            }
            else
            {
                              
            }
        }
    }
}
