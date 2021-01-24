using GFD.Siscom.Enrollment.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Syncfusion.Pdf;
using Syncfusion.HtmlConverter;
using System.IO;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace GFD.Siscom.Enrollment.Utilities.GeneraPDF
{
    public class PDFGenerator
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        private Controller controller;
        //private object data { get; set; }

        public PDFGenerator(Microsoft.AspNetCore.Hosting.IHostingEnvironment __hostingEnvironment, Controller _controller)
        {
            this._hostingEnvironment = __hostingEnvironment;
            this.controller = _controller;
            //this.data = _data;
        }

        public async Task<string> GerneraEstadoDeCuenta(object data)
        {
            HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter();
            WebKitConverterSettings settings = new WebKitConverterSettings();
            try
            {
                dynamic d = JsonConvert.DeserializeObject<object>(JsonConvert.SerializeObject(data));
                Agreement ag = JsonConvert.DeserializeObject<Agreement>(JsonConvert.SerializeObject(d["agreement"]));
                string template = await ConvertViewsToHTML.RenderViewAsync(controller, "EstadoDeCuentaPDF", data, true);
                settings.WebKitPath = Path.Combine(_hostingEnvironment.ContentRootPath, "QtBinariesWindows");
                htmlConverter.ConverterSettings = settings;
                PdfDocument document = htmlConverter.Convert(template, "");
                MemoryStream stream = new MemoryStream();
                document.Save(stream);
                stream.Position = 0;
                document.Close(true);
                string Filename = "Estado_De_Cuenta_" + ag.account + ".pdf";
                System.IO.File.WriteAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, Filename), stream.ToArray());
                return Filename;
            }
            catch (Exception e)
            {
                return e.Message;
            }
            
        }
    }
}
