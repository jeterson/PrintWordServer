using ReplaceWordServer.Model;
using ReplaceWordServer.Parsers;
using ReplaceWordServer.Server;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ReplaceWordServer.Resource
{
    public class ListPrintersHttpRequestHandler: HttpRequestHandler
    {
        public const String NAME = "/ListPrinters";

        public void Handle(System.Net.HttpListenerContext context)
        {
            HttpListenerResponse response = context.Response;
            response.StatusCode = (int) HttpStatusCode.OK;

            List<PrinterModel> printers = new List<PrinterModel>();
            PrinterSettings ps = new PrinterSettings();
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                
                
                    printers.Add(new PrinterModel()
                    {
                        Name = printer,
                        Default = ps.PrinterName == printer
                    });
                
                
            }

            String serialized = Serialization.SerializeToJson<List<PrinterModel>>(printers);
            byte[] messageBytes = Encoding.Default.GetBytes(serialized);
            response.OutputStream.Write(messageBytes, 0, messageBytes.Length);
            // Send the HTTP response to the client
            response.ContentType = "application/json";
            response.Close();
        }

        public string GetName()
        {
            return NAME;
        }
    }
}
