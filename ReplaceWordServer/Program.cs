using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using ReplaceWordServer.Server;
using ReplaceWordServer.Services;
using ReplaceWordServer.Model;
using ReplaceWordServer.Resource;
using ReplaceWordServer.Tools;

namespace ReplaceWordServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Configuration.Singleton().ConfigureAndStart();
        }

    }
}
