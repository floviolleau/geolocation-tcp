using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;

namespace GeolocationTCP
{
    class LocationManager
    {
        private Geolocator _geolocator = null;

        public async void getCoordinate()
        {
            _geolocator = new Geolocator();
            _geolocator.DesiredAccuracy = PositionAccuracy.High;

            try
            {
                CancellationTokenSource _cts = new CancellationTokenSource();
                CancellationToken token = _cts.Token;

                Geoposition pos = await _geolocator.GetGeopositionAsync();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

    }
}
