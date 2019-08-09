using ReplaceWordServer.Model;
using ReplaceWordServer.Parsers;
using ReplaceWordServer.Server;
using ReplaceWordServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ReplaceWordServer.Resource
{
    public class ReplacePrinterWordHttpRequestHandler : HttpRequestHandler
    {
        public const String NAME = "/Print";

        private ReplaceWordService replaceService = new ReplaceWordService();
        private PrintWordService printService = new PrintWordService();

        public void Handle(System.Net.HttpListenerContext context)
        {
            HttpListenerResponse response = context.Response;
            response.StatusCode = (int)HttpStatusCode.OK;

            System.IO.StreamReader reader = null;
            System.IO.Stream body = null;
            string message;
            try
            {
                body = context.Request.InputStream;
                System.Text.Encoding encoding = context.Request.ContentEncoding;
                reader = new System.IO.StreamReader(body, encoding);

                String json = reader.ReadToEnd();

                PrintModel printModel = Serialization.DesserializeFromJson<PrintModel>(json);

                String outFile = replaceService.Replace(printModel);
                printService.PrintDoc(outFile);


            }
            catch (Exception e)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                message = e.Message;
                byte[] messageBytes = Encoding.Default.GetBytes(message);
                response.OutputStream.Write(messageBytes, 0, messageBytes.Length);

            }
            finally
            {
                response.Close();

                if (body != null)
                {
                    body.Close();
                }

                if (reader != null)
                {
                    reader.Close();
                }

            }
        }

        public string GetName()
        {
            return NAME;
        }
    }
}
