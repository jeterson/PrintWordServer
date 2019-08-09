using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ReplaceWordServer.Server
{
   public interface HttpRequestHandler
    {
        void Handle(HttpListenerContext context);

        string GetName();
    }
}
