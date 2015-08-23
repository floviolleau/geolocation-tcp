# Overview
GeolocationTCP creates a simple TCP server which interfaces the Windows 8 Location API and serves NMEA sentences. Intended for use with [OpenCPN](http://opencpn.org).

Feedback is most welcome.

# How to use
1. Download [GeolocationTCP-r6.exe](https://bitbucket.org/petrsimon/geolocationtcp/downloads/GeolocationTCP-r6.exe). You might also need [GeolocationTCP.exe.config](https://bitbucket.org/petrsimon/geolocationtcp/downloads/GeolocationTCP.exe.config). Place it in the same directory as `GeolocationTCP-r6.exe`. Start the program.
2. Open OpenCPN, go to Options > Connections and create new Network connection with Adress 127.0.0.1 and DataPort 15555.

## Note
- Windows will try to locate your position using either of these sources: Cellular, Wifi, Satellite. Thus if you want to see what you get offshore, turn off your cellular and wifi connections.
- The frequency of location updates might be quite irregular. 
- If your position in OpenCPN is off, try `Settings > User Interface` and set `Show Lat/Long as` to `Degrees, Minutes, Seconds`.
- Windows 10 users: GeolocationTCP does not recognize the correct GNSS status when started or when switching the airline mode. Swipe to open the Action center and tap the Location tile to disable location services. Then tap it again. GeolocationTCP should be able to pickup the GNSS status now.


# Building
See [Using Windows 8* WinRT API from desktop applications](http://software.intel.com/en-us/articles/using-winrt-apis-from-desktop-applications) on how to setup VS Express.

# Credits
1. [edrazy](http://www.codeproject.com/Articles/13232/A-very-basic-TCP-server-written-in-C)
2. [kalmangabriel](http://forum.gpsgate.com/topic.asp?TOPIC_ID=13491)