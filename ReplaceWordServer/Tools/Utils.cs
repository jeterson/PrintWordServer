using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ReplaceWordServer.Tools
{
  public class Utils
    {
      public static string GetLocalIP()
      {
          var host = Dns.GetHostEntry(Dns.GetHostName());
          foreach (var ip in host.AddressList)
          {
              if (ip.AddressFamily == AddressFamily.InterNetwork)
              {
                  return ip.ToString();
              }
          }
          throw new Exception("No network adapters with an IPv4 address in the system!");
      }

      public static bool IsRunningAsAdministrator()
      {
          var identity = WindowsIdentity.GetCurrent();
          var principal = new WindowsPrincipal(identity);
          return principal.IsInRole(WindowsBuiltInRole.Administrator);
      }
    }
}
