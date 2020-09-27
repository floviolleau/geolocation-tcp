using System;
using System.Text;
using TcpLib;


namespace GeolocationTCP
{
    public class GeolocationProvider : TcpServiceProvider
    {
        //private Geolocator loc = null;
        //private TypedEventHandler<Geolocator, PositionChangedEventArgs> positionChangedHandler;
        //private TypedEventHandler<Geolocator, StatusChangedEventArgs> statusChangedHandler;

        //private MainWindow window = null;
        private ConnectionState connection = null;

        public GeolocationProvider()
        {
            
        }

        public override object Clone()
        {
            return new GeolocationProvider();
        }

        public void Report(string nmea)
        {

            //FIXME from time to time throws ObjectDisposed
            Boolean sent = connection.Write(Encoding.UTF8.GetBytes(nmea), 0, nmea.Length);
            //if (!sent)
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
        }

        public override void OnAcceptConnection(ConnectionState state)
        {
            connection = state;
        }

        public override void OnReceiveData(ConnectionState state)
        {

        }
        public override void OnDropConnection(ConnectionState state)
        {
        }

    }
}
