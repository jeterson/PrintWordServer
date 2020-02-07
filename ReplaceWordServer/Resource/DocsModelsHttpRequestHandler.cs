using ReplaceWordServer.Model;
using ReplaceWordServer.Parsers;
using ReplaceWordServer.Server;
using ReplaceWordServer.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ReplaceWordServer.Resource
{
    public class DocsModelsHttpRequestHandler : HttpRequestHandler
    {
        public const String NAME = "/DocsModels";

        public void Handle(System.Net.HttpListenerContext context)
        {
            HttpListenerResponse response = context.Response;
            HttpListenerRequest request = context.Request;
            Utils.CorsConfig(request, response);
            response.StatusCode = (int)HttpStatusCode.OK;
            String dir = request.QueryString.Get("dir");

            if (request.HttpMethod == "OPTIONS")
            {
                response.Close();
            }
            else
            {

                try
                {
                    List<DocFile> docfiles = GetFiles(dir);
                    String serialized = Serialization.SerializeToJson<List<DocFile>>(docfiles);
                    byte[] messageBytes = Encoding.Default.GetBytes(serialized);
                    response.OutputStream.Write(messageBytes, 0, messageBytes.Length);
                    // Send the HTTP response to the client
                    response.ContentType = "application/json";
                    response.Close();
                }
                catch (Exception ex)
                {
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    byte[] messageBytes = Encoding.Default.GetBytes(ex.Message);
                    response.OutputStream.Write(messageBytes, 0, messageBytes.Length);
                    response.ContentType = "application/json";
                    response.Close();
                }
            }


        }

        public string GetName()
        {
            return NAME;
        }

        private List<DocFile> GetFiles(String dir)
        {
            DirectoryInfo d = new DirectoryInfo(dir);
            FileInfo[] files = d.GetFiles("*.doc*");
            List<DocFile> docFiles = new List<DocFile>();
            foreach (FileInfo file in files)
            {
                docFiles.Add(new DocFile()
                {
                    FileName = file.Name,
                    Path = file.FullName,
                    BasePath = file.DirectoryName
                })
;
            }

            return docFiles;
        }
    }
}
