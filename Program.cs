using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using TcpLib;

namespace GeolocationTCP
{
    class Program
    {
        private static TcpServer server;
        private static GeolocationProvider provider;

        [STAThread]
        static void Main()
        {
            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainWindow w = new MainWindow();
            Locator locator = new Locator(w);
            provider = new GeolocationProvider();
            server = new TcpServer(provider, 15555);
            server.SetLocator(locator);
            server.Start();

            Application.Run(w);
        }
    }
}
