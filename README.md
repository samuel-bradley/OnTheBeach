# OnTheBeach

This is my attempt at the .NET technical assessment for my On the Beach application.
It does not necessarily represent the best solution I could possibly arrive at, as I have tried to stay roughly within the recommended time - hopefully there will be a chance to discuss what improvements could be made.

Note also that this was my first attempt at building a .NET/C# application exposing a web interface in Visual Studio, so there might be some beginner mistakes or idiosyncrasies carried over from working in other ecosystems.

The `HolidayServiceTests` file contains tests which exercise the bulk of the logic, which exists within `HolidayService`.

The application can also be run using the `http` config and exposes a Swagger interface for manual testing. Note that the example body format given by Swagger does not seem to be parsed correctly by `HolidayController` - instead, use the format shown in `OnTheBeach.API.http`.
