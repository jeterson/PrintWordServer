using ReplaceWordServer.Resource;
using ReplaceWordServer.Server;
using ReplaceWordServer.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplaceWordServer
{
    public class Configuration : IDisposable
    {
        private static Configuration instance;
        private HttpServer server = null;
        public int Port { get; set; }

        public void Dispose()
        {
            if (server != null)
            {
                server.Dispose();
                server = null;
            }
        }
        public static Configuration Singleton()
        {
            if (instance == null)
                instance = new Configuration();


            return instance;
        }

        public void ConfigureAndStart()
        {
            int port = Port;
            String networkIpAddress = Utils.GetLocalIP();
            String address1 = string.Format("http://localhost:{0}/", port);
            String address2 = string.Format("http://127.0.0.1:{0}/", port);
            String address3 = string.Format("http://{0}:{1}/", networkIpAddress, port);

          
            if (Utils.IsRunningAsAdministrator())
            {
                server = new HttpServer(address1, address2, address3);
            }
            else
            {
                server = new HttpServer(address1, address2);
            }


            server.AddHttpRequestHandler(new ReplacePrinterWordHttpRequestHandler());
            server.AddHttpRequestHandler(new ListPrintersHttpRequestHandler());
            server.AddHttpRequestHandler(new TestHttpRequestHandler());
            server.Start();

        }

        public bool IsServerListening()
        {
            return server.IsListening();
        }

        public void Stop()
        {
            server.Stop();
              
        }

        private Configuration() {
            Port = 5170;
        }


    }
}
