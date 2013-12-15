using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;

namespace GeolocationTCP
{

    public class Locator
    {
        private Geolocator loc = null;
        private TypedEventHandler<Geolocator, PositionChangedEventArgs> positionChangedHandler;
        private TypedEventHandler<Geolocator, StatusChangedEventArgs> statusChangedHandler;
        private MainWindow window;
        private GeolocationProvider provider;

        public Locator(MainWindow w)
        {
            window = w;
            loc = new Geolocator();
            loc.DesiredAccuracy = PositionAccuracy.Default;
            loc.ReportInterval = 500;
            StartTracking();
        }

        public void SetProvider(TcpLib.TcpServiceProvider p)
        {
            provider = (GeolocationProvider)p;
        }

        /// <SUMMARY>
        /// Calculates NMEA sentence checksum.
        /// Expects NMEA sentence including $.
        /// </SUMMARY>
        private string getChecksum(string sentence)
        {
            int checksum = Convert.ToByte(sentence[sentence.IndexOf('$') + 1]);
            for (int i = sentence.IndexOf('$') + 2; i < sentence.Length; i++)
            {
                checksum ^= Convert.ToByte(sentence[i]);
            }
            // format as hexadecimal
            return checksum.ToString("X2");
        }

        public String decimalToNMEA(double lat, double lon)
        {
            string nmea = "";
            double lata = Math.Abs(lat);
            double latd = Math.Truncate(lata);
            double latm = (lata - latd) * 60;
            string lath = lat > 0 ? "N" : "S";
            double lnga = Math.Abs(lon);
            double lngd = Math.Truncate(lnga);
            double lngm = (lnga - lngd) * 60;
            string lngh = lon > 0.0 ? "E" : "W";

            nmea += latd.ToString("00") + latm.ToString("00.00") + "," + lath + ",";
            nmea += lngd.ToString("000") + lngm.ToString("00.00") + "," + lngh;

            return nmea;
        }

        public void Report(string nmea)
        {
            try
            {
                provider.Report(nmea);
            } catch(Exception e){
                //window.SetLocationLog(e.ToString());
            }
        }

        public void UpdateUI(DateTimeOffset datetime, string nmea, double lat, double lon, 
            string speed, string accuracy)
        {
            //try
            //{
            window.SetLocationLog(datetime.ToString() + " sending NMEA:");
            window.SetLocationLog(nmea);
           
            window.SetLatLon(lat, lon);
            if (speed == "")
            {
                speed = "n/a";
            }
            else
            {
                speed += "kn";
            }
            window.SetSpeed(speed);
            window.SetDatetime(datetime.ToString());
            window.SetAccuracy(accuracy+"m");
            //}
            //catch (Exception ex) {
            //    throw ex;
            //}
        }

        public void StartTracking()
        {

            if (positionChangedHandler == null)
            {
                positionChangedHandler = (geo, e) =>
                {

                    Geoposition pos = e.Position;
                    DateTimeOffset datetime = pos.Coordinate.Timestamp;
                    String time = pos.Coordinate.Timestamp.UtcDateTime.ToString("hhmmss");
                    String date = pos.Coordinate.Timestamp.UtcDateTime.ToString("dMMyy");
                    double lat = (double)pos.Coordinate.Latitude;
                    double lon = (double)pos.Coordinate.Longitude;
                    string accuracy = pos.Coordinate.Accuracy.ToString();

                    String heading = "";
                    if (pos.Coordinate.Heading != null)
                    {
                        heading = pos.Coordinate.Heading.Value.ToString("00.00");
                    }

                    String speed = "";
                    if (pos.Coordinate.Speed != null)
                    {
                        double kn = (double) pos.Coordinate.Speed / 0.514444;
                        speed = kn.ToString("00.00");
                    }

                    String coords = decimalToNMEA(lat, lon);

                    String sentence = String.Format("$GPRMC,{0},A,{1},{2},{3},{4},,",
                        time, coords, speed, heading, date);
                    String nmea = sentence + "*" + getChecksum(sentence);
                    //Console.WriteLine("Sent NMEA sentence {0}", nmea);

                    Report(nmea);
                    UpdateUI(datetime, nmea, lat, lon, speed, accuracy);
                    
                };
            }


            if (statusChangedHandler == null)
            {
                statusChangedHandler = (geo, e) =>
                {
                    PositionStatus status = e.Status;
                    try
                    {
                        window.SetStatus(status.ToString());
                    }
                    catch (Exception ex)
                    {
                    }
                };
            }
            // this implicitly starts the tracking operation
            loc.PositionChanged += positionChangedHandler;
            loc.StatusChanged += statusChangedHandler;
        }

    }
}
