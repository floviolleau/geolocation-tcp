# Overview
GeolocationTCP creates a simple TCP server which interfaces the Windows 8 Location API and serves NMEA sentences. Intended for use with [OpenCPN](http://opencpn.org).

The software is provided "as-is", no warranties. Use at our own risk!

# How to use
1. Download [GeolocationTCP-r9.exe](https://bitbucket.org/petrsimon/geolocationtcp/downloads/GeolocationTCP-r9.exe) and start the program.
2. Open OpenCPN, go to Options > Connections and create new Network connection with Adress 127.0.0.1 and DataPort 15555.

## Note
- Windows will try to locate your position using either of these sources: Cellular, Wifi, Satellite. Thus if you want to see what you get offshore, turn off your cellular and wifi connections.
- The frequency of location updates might be quite irregular. 
- If your position in OpenCPN is off, try `Settings > User Interface` and set `Show Lat/Long as` to `Degrees, Minutes, Seconds`.

# Building
Open the project in Visual Studio and add references to `Windows.Devices` and `Windows.Foundation`. Then build.

#License
GPL v2.0

# Credits
1. [edrazy](http://www.codeproject.com/Articles/13232/A-very-basic-TCP-server-written-in-C)
2. [kalmangabriel](http://forum.gpsgate.com/topic.asp?TOPIC_ID=13491)
3. [Petr Simon <petr.simon[at]gmail.com>](Petr Simon <petr.simon[at]gmail.com>)
