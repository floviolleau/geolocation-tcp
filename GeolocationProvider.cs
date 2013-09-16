using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using TcpLib;
using Windows.Devices.Geolocation;
using Windows.Foundation;


namespace GeolocationTCP
{
    public class GeolocationProvider : TcpServiceProvider
    {
        private Geolocator loc = null;
        private TypedEventHandler<Geolocator, PositionChangedEventArgs> positionChangedHandler;

        public GeolocationProvider()
        {
            loc = new Geolocator();
            loc.DesiredAccuracy = PositionAccuracy.High;
            loc.ReportInterval = (uint)TimeSpan.FromSeconds(1).TotalMilliseconds;
        }

        public override object Clone()
        {
            return new GeolocationProvider();
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
            string lngh = lon > 0 ? "E" : "W";

            nmea += latd.ToString("00") + latm.ToString("00.00") + "," + lath + ",";
            nmea += lngd.ToString("000") + lngm.ToString("00.00") + "," + lngh;

            return nmea;
        }

        public void StartTracking(ConnectionState state)
        {
            if (positionChangedHandler == null)
            {
                positionChangedHandler = (geo, e) =>
                {
                    Geoposition pos = e.Position;
                    String time = pos.Coordinate.Timestamp.UtcDateTime.ToString("hhmmss");
                    String date = pos.Coordinate.Timestamp.UtcDateTime.ToString("dMMyy");
                    double lat = (double) pos.Coordinate.Latitude;
                    double lon = (double) pos.Coordinate.Longitude;

                    String heading = "";
                    if (pos.Coordinate.Heading != null)
                    {
                        heading = pos.Coordinate.Heading.Value.ToString("00.00");
                    }
                    
                    String speed = "";
                    if (pos.Coordinate.Speed != null)
                    {
                        double kn = pos.Coordinate.Speed.Value * 0.514444;
                        speed = kn.ToString("00.00");
                    }

                    String coords = decimalToNMEA(lat, lon);
                    
                    String sentence = String.Format("$GPRMC,{0},A,{1},{2},{3},{4},,", 
                        time, coords, speed, heading, date);
                    String nmea = sentence + "*" + getChecksum(sentence);
                    //Console.WriteLine("Connected, sending NMEA string {0}", nmea);
                    
                    //FIXME from time to time throws ObjectDisposed
                    Boolean written = state.Write(Encoding.UTF8.GetBytes(nmea), 0, nmea.Length);
                    //if (!written)
                    //{
                    //    try
                    //    {
                    //        state.EndConnection(); //if write fails... then close connection
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        Console.WriteLine(ex.ToString());
                    //    }
                    //}
                };
            }

            // this implicitly starts the tracking operation
            loc.PositionChanged += positionChangedHandler;
        }
        public override void OnAcceptConnection(ConnectionState state)
        {
            Console.WriteLine("connection accepted");
            StartTracking(state);
        }

        public override void OnReceiveData(ConnectionState state)
        {

        }
        public override void OnDropConnection(ConnectionState state)
        {
            Console.WriteLine("Connection dropped.");
        }

    }
}
