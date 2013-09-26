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
            string disclaimer = "Geolocation TCP is distributed in the hope that it will be useful, "+
            "but WITHOUT ANY WARRANTY; without even the implied " +
            "warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE." + Environment.NewLine +
            "See the GNU General Public License for more details." + Environment.NewLine +
            "Geolocation TCP must only be used in conjunction with approved paper charts "+
            "and traditional methods of navigation." + Environment.NewLine + Environment.NewLine +
            "DO NOT rely upon Geolocation TCP for safety of life or property.";

            DialogResult res = MessageBox.Show(disclaimer, "Warning", MessageBoxButtons.OKCancel);
            if (res == DialogResult.Cancel)
            {
                return;
            }

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
