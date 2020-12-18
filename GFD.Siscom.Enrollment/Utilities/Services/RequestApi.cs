using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Utilities.Services
{
    public class RequestApi
    {
        private string UrlBase;
        public RequestApi(string pUrlBase)
        {
            UrlBase = pUrlBase;
        }

        public async Task<string> SendURIAsync(string endPoint, HttpMethod method, HttpContent httpContent = null)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage httpResponse = null;
                try
                {
                    var time = client.Timeout;
                    if (method.Method == "GET")
                    {
                        HttpRequestMessage httpRequest = new HttpRequestMessage
                        {
                            Method = method,
                            RequestUri = new Uri(UrlBase + endPoint),
                        };
                        httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        httpResponse = await client.SendAsync(httpRequest);
                    }
                    else
                    {
                        HttpRequestMessage httpRequest = new HttpRequestMessage
                        {
                            Method = method,
                            RequestUri = new Uri(UrlBase + endPoint),
                            Content = httpContent,
                        };
                        httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        httpResponse = await client.SendAsync(httpRequest);
                    }

                    switch (httpResponse.StatusCode)
                    {
                        case System.Net.HttpStatusCode.OK:
                            return await httpResponse.Content.ReadAsStringAsync();
                        case System.Net.HttpStatusCode.InternalServerError:
                            return "{\"error\": \"Servicio temporalmente no disponible contacte al Administrador, disculpe las molestias: x000500\"}";
                        case System.Net.HttpStatusCode.ServiceUnavailable:
                            return "{\"error\": \"Servicio temporalmente no disponible contacte al Administrador, disculpe las molestias: x000503\"}";
                        case System.Net.HttpStatusCode.Forbidden:
                            return "{\"error\": \"Servicio temporalmente no disponible contacte al Administrador, disculpe las molestias: x000503\"}";
                        case System.Net.HttpStatusCode.GatewayTimeout:
                            return "{\"error\": \"Servicio temporalmente no disponible contacte al Administrador, disculpe las molestias: x000504\"}";
                        case System.Net.HttpStatusCode.Unauthorized:
                            return "{\"error\": \"Sesión expírada o no cuenta con la autorización: x000401 \"}";
                        default:
                            return await httpResponse.Content.ReadAsStringAsync();
                    }

                }
                catch (Exception e)
                {
                    return "{\"error\": \"Servicio no disponible contacte al Administrador\"}";
                }
                finally
                {
                    if (httpResponse != null)
                    {
                        httpResponse.Dispose();
                    }
                }
            }
        }

        public async Task<string> SendURIAsync(string endPoint, HttpMethod method, string Token, HttpContent httpContent = null)
        {

            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(10);
                HttpResponseMessage httpResponse = null;
                try
                {


                    if (method.Method == "GET")
                    {
                        HttpRequestMessage httpRequest = new HttpRequestMessage
                        {
                            Method = method,
                            RequestUri = new Uri(UrlBase + endPoint),
                        };
                        httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);


                        httpResponse = await client.SendAsync(httpRequest);
                    }
                    else
                    {
                        HttpRequestMessage httpRequest = new HttpRequestMessage
                        {
                            Method = method,
                            RequestUri = new Uri(UrlBase + endPoint),
                            Content = httpContent,
                        };
                        httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        httpResponse = await client.SendAsync(httpRequest);
                    }

                    switch (httpResponse.StatusCode)
                    {
                        case System.Net.HttpStatusCode.OK:
                            var result = await httpResponse.Content.ReadAsStringAsync();
                            return result;
                        case System.Net.HttpStatusCode.InternalServerError:
                            return "{\"error\": \"Servicio temporalmente no disponible contacte al Administrador, disculpe las molestias: x000500\"}";
                        case System.Net.HttpStatusCode.ServiceUnavailable:
                            return "{\"error\": \"Servicio temporalmente no disponible contacte al Administrador, disculpe las molestias: x000503\"}";
                        case System.Net.HttpStatusCode.Forbidden:
                            return "{\"error\": \"Servicio temporalmente no disponible contacte al Administrador, disculpe las molestias: x000503\"}";
                        case System.Net.HttpStatusCode.GatewayTimeout:
                            return "{\"error\": \"Servicio temporalmente no disponible contacte al Administrador, disculpe las molestias: x000504\"}";
                        case System.Net.HttpStatusCode.Unauthorized:
                            return "{\"error\": \"Sesión expírada o no cuenta con la autorización: x000401 \"}";
                        default:
                            return await httpResponse.Content.ReadAsStringAsync();
                    }
                }
                catch (Exception)
                {
                    return "{\"error\": \"Servicio no disponible contacte al Administrador\"}";
                }
                finally
                {
                    if (httpResponse != null)
                    {
                        httpResponse.Dispose();
                    }
                }
            }
        }

        public async Task<string> UploadImageToServer(string endPoint, string Token, string filePath, StringContent data = null)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage httpResponse = null;
                try
                {
                    var method = new MultipartFormDataContent();
                    if (string.IsNullOrEmpty(filePath))
                    {
                        return "{\"error\": \"Debe seleccionar un archivo para poder resolver su petición\"}";
                    }

                    FileStream fs = File.OpenRead(filePath);
                    var streamContent = new StreamContent(fs);
                    var imageContent = new ByteArrayContent(streamContent.ReadAsByteArrayAsync().Result);
                    imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                    if (data != null)
                    {
                        method.Add(data, "\"Data\"");
                    }
                    method.Add(imageContent, "AttachedFile", Path.GetFileName(filePath));
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                    httpResponse = await client.PostAsync(new Uri(UrlBase + endPoint), method);
                    var input = await httpResponse.Content.ReadAsStringAsync();

                    switch (httpResponse.StatusCode)
                    {
                        case System.Net.HttpStatusCode.OK:
                            return await httpResponse.Content.ReadAsStringAsync();
                        case System.Net.HttpStatusCode.InternalServerError:
                            return "{\"error\": \"Servicio temporalmente no disponible contacte al Administrador, disculpe las molestias\"}";
                        case System.Net.HttpStatusCode.ServiceUnavailable:
                            return "{\"error\": \"Servicio temporalmente no disponible contacte al Administrador, disculpe las molestias\"}";
                        case System.Net.HttpStatusCode.Unauthorized:
                            return "{\"error\": \"Sesión expírada o no cuenta con la autorización \"}";
                        default:
                            return await httpResponse.Content.ReadAsStringAsync();
                    }
                }
                catch (Exception e)
                {
                    return "{\"error\": \"Servicio no disponible contacte al Administrador\"}";
                }
                finally
                {
                    if (httpResponse != null)
                    {
                        httpResponse.Dispose();
                    }
                }
            }
        }

        public async Task<string> UploadImageToServer(string endPoint, string Token, string FileName, MemoryStream file, StringContent data = null)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage httpResponse = null;
                try
                {
                    var method = new MultipartFormDataContent();
                    if (file == null)
                    {
                        return "{\"error\": \"Debe seleccionar un archivo para poder resolver su petición\"}";
                    }

                    //FileStream fs = File.OpenRead(FileName);
                    var streamContent = new StreamContent(file);
                    var imageContent = new ByteArrayContent(streamContent.ReadAsByteArrayAsync().Result);
                    imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                    if (data != null)
                    {
                        method.Add(data, "\"Data\"");
                    }
                    method.Add(imageContent, "AttachedFile", FileName);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                    httpResponse = await client.PostAsync(new Uri(UrlBase + endPoint), method);
                    var input = await httpResponse.Content.ReadAsStringAsync();

                    switch (httpResponse.StatusCode)
                    {
                        case System.Net.HttpStatusCode.OK:
                            return await httpResponse.Content.ReadAsStringAsync();
                        case System.Net.HttpStatusCode.InternalServerError:
                            return "{\"error\": \"Servicio temporalmente no disponible contacte al Administrador, disculpe las molestias\"}";
                        case System.Net.HttpStatusCode.ServiceUnavailable:
                            return "{\"error\": \"Servicio temporalmente no disponible contacte al Administrador, disculpe las molestias\"}";
                        case System.Net.HttpStatusCode.Unauthorized:
                            return "{\"error\": \"Sesión expírada o no cuenta con la autorización \"}";
                        default:
                            return await httpResponse.Content.ReadAsStringAsync();
                    }
                }
                catch (Exception e)
                {
                    return "{\"error\": \"Servicio no disponible contacte al Administrador\"}";
                }
                finally
                {
                    if (httpResponse != null)
                    {
                        httpResponse.Dispose();
                    }
                }
            }
        }

        //--
        public async Task<HttpResponseMessage> RequestAPIAsync(string endPoint, string strMethod , string Token, HttpContent httpContent = null)
        {
            HttpMethod method;
            switch (strMethod.ToLower())
            {
                case "get":
                    method = HttpMethod.Get;
                    break;
                case "post":
                    method = HttpMethod.Post;
                    break;
                case "put":
                    method = HttpMethod.Put;
                    break;
                case "delete":
                    method = HttpMethod.Delete;
                    break;
                default:
                    method = HttpMethod.Get;
                    break;
            }
            
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(10);
                HttpResponseMessage httpResponse = null;
                try
                {
                    //if (method.Method == "GET")
                    //{
                    //    HttpRequestMessage httpRequest = new HttpRequestMessage
                    //    {
                    //        Method = method,
                    //        RequestUri = new Uri(UrlBase + endPoint),
                    //    };
                    //    httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //    httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);


                    //    httpResponse = await client.SendAsync(httpRequest);
                    //}
                    //else
                    //{
                        HttpRequestMessage httpRequest = new HttpRequestMessage
                        {
                            Method = method,
                            RequestUri = new Uri(UrlBase + endPoint),
                            Content = httpContent,
                        };
                        httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        httpResponse = await client.SendAsync(httpRequest);
                    //}
                    return httpResponse;
                    //switch (httpResponse.StatusCode)
                    //{
                    //    case System.Net.HttpStatusCode.OK:
                    //        var result = await httpResponse.Content.ReadAsStringAsync();
                    //        return result;
                    //    case System.Net.HttpStatusCode.InternalServerError:
                    //        return "{\"error\": \"Servicio temporalmente no disponible contacte al Administrador, disculpe las molestias: x000500\"}";
                    //    case System.Net.HttpStatusCode.ServiceUnavailable:
                    //        return "{\"error\": \"Servicio temporalmente no disponible contacte al Administrador, disculpe las molestias: x000503\"}";
                    //    case System.Net.HttpStatusCode.Forbidden:
                    //        return "{\"error\": \"Servicio temporalmente no disponible contacte al Administrador, disculpe las molestias: x000503\"}";
                    //    case System.Net.HttpStatusCode.GatewayTimeout:
                    //        return "{\"error\": \"Servicio temporalmente no disponible contacte al Administrador, disculpe las molestias: x000504\"}";
                    //    case System.Net.HttpStatusCode.Unauthorized:
                    //        return "{\"error\": \"Sesión expírada o no cuenta con la autorización: x000401 \"}";
                    //    default:
                    //        return await httpResponse.Content.ReadAsStringAsync();
                    //}
                }
                catch (Exception)
                {                   
                    return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                }
                finally
                {
                    if (httpResponse != null)
                    {
                        httpResponse.Dispose();
                    }
                }
            }
        }
    }
}
