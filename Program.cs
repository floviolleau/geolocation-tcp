using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using TcpLib;

namespace GeolocationTCP
{
    class Program
    {
        private static TcpServer server;
        private static GeolocationProvider provider;

        static void Main(string[] args)
        {
            provider = new GeolocationProvider();
            server = new TcpServer(provider, 15555);
            server.Start();
            Console.ReadKey();
        }

    }
}
