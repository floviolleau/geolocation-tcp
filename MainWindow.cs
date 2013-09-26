using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeolocationTCP
{
    public partial class MainWindow : Form
    {
        private NotifyIcon trayIcon;
        private ContextMenu trayMenu;

        public MainWindow()
        {
            this.Closing += MainWindow_Closing;
            InitializeComponent();

            trayMenu = new ContextMenu();
            trayMenu.MenuItems.Add("Open", OnOpen);
            trayMenu.MenuItems.Add("Exit", OnExit);
            trayIcon = new NotifyIcon();
            trayIcon.Text = "Geolocation TCP";
            trayIcon.Icon = new Icon(SystemIcons.Information, 40, 40);
            trayIcon.Click += new EventHandler(OnTrayIconClick);
            trayIcon.Visible = true;
            trayIcon.ContextMenu = trayMenu;
        }

        protected override void OnLoad(EventArgs e)
        {
            Visible = true; // Hide form window.
            ShowInTaskbar = true; // Remove from taskbar.

            base.OnLoad(e);
        }


        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
               trayIcon.Dispose();
            }
            base.Dispose(disposing);
            
        }

        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OnTrayIconClick(Object sender, EventArgs args)
        {
            if (this.Visible)
            {
                this.Hide();
                ShowInTaskbar = false;
            }
            else
            {
                this.Show();
                ShowInTaskbar = true;
            }
        }

        private void OnOpen(Object sender, EventArgs args)
        {
            this.Show();
        }

        void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Prevent window from closing
            e.Cancel = true;
            this.Hide();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        internal void SetLocationLog(String text)
        {
            if (this.locationLog.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetLocationLog);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.locationLog.AppendText(text);
            }
        }

        delegate void SetTextCallback(string text);

        internal void SetLatLon(double lat, double lon)
        {
            String deg = "°";

            double lata = Math.Abs(lat);
            double latd = Math.Truncate(lata);
            double latm = (lata - latd) * 60;
            string lath = lat > 0 ? "N" : "S";
            double lnga = Math.Abs(lon);
            double lngd = Math.Truncate(lnga);
            double lngm = (lnga - lngd) * 60;
            string lngh = lon > 0 ? "E" : "W";

            string latitude = latd.ToString("00") + deg + latm.ToString(" 00.00") + "'" + lath;
            string longitude = lngd.ToString("000") + deg + lngm.ToString(" 00.00") + "' " + lath;

            SetLat(latitude);
            SetLon(longitude);

        }
        internal void SetLat(String text)
        {
            
            if (this.labelLat.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetLat);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.labelLat.Text = text;
            }
        }

        internal void SetLon(string text)
        {
            if (this.labelLon.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetLon);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.labelLon.Text = text;
            }
        }

        internal void SetSpeed(string text)
        {
            if (this.labelSpeed.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetSpeed);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.labelSpeed.Text = text;
            }
        }

        internal void SetDatetime(string text)
        {
            if (this.labelDate.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetDatetime);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.labelDate.Text = text;
            }
        }
    }
}
