using System.Net;
using System.Reflection;

namespace DInstaller.Protection.StringOnline
{
    internal class OnlineString
    {
        public static string Decoder(string encrypted)
        {
            if (Assembly.GetExecutingAssembly() != Assembly.GetCallingAssembly()) return "DInstaller.png";
            var webClient = new WebClient();
            return webClient.DownloadString($"https://communitykeyv1.000webhostapp.com/Decoder4.php?string={encrypted}"); //this host don't work anymore you need to have your proper host
        }
    }
}