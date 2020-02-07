using ReplaceWordServer.Server;
using ReplaceWordServer.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ReplaceWordServer.Resource
{
    public class TestHttpRequestHandler : HttpRequestHandler
    {
        public const String NAME = "/";
        public void Handle(System.Net.HttpListenerContext context)
        {
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;
            Utils.CorsConfig(request, response);

            
            response.StatusCode = (int)HttpStatusCode.OK;

            const String message = "true";
            byte[] messageBytes = Encoding.Default.GetBytes(message);
            response.OutputStream.Write(messageBytes, 0, messageBytes.Length);
            // Send the HTTP response to the client
            response.Close();
        }

        public string GetName()
        {
            return NAME;
        }
    }
}
