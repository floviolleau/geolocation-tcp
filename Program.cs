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

        //static void Main(string[] args)
        //{
        //    provider = new GeolocationProvider();
        //    server = new TcpServer(provider, 15555);
        //    server.Start();
        //    Console.WriteLine("Press any key to exit...");
        //    Console.ReadKey();
        //}

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainWindow w = new MainWindow();

            provider = new GeolocationProvider();
            server = new TcpServer(provider, 15555);
            server.SetWindow(w);
            server.Start();

            Application.Run(w);
        }



    }
}
