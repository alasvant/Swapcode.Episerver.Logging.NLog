# Swapcode.Episerver.Logging.NLog
Implementation to change Episerver default logging framework [log4net](https://logging.apache.org/log4net/) with [NLog](http://nlog-project.org/).

This implementation uses the Episerver supported way of [changing the logging framework](https://world.episerver.com/documentation/developer-guides/CMS/logging/) by using the LoggerFactory assembly attribute.

# Before installing this package
- remove the EPiServer.Logging.Log4net NuGet package
- remove log4net NuGet package
- you can also remove the EPiServerLog.config now or later as it is not used anymore

# Install with NuGet Package Manager
Currently the NuGet package is available from the [releases](https://github.com/alasvant/Swapcode.Episerver.Logging.NLog/releases). NuGet feed address coming..

The package includes NLog and NLog.Web NuGet packages that are required for logging and to format the logs.

Install doesn't include NLog configuration file which is described [here](https://github.com/nlog/nlog/wiki/Configuration-file).

# After installing package
- (*optional*) install NLog.Schema NuGet package from NuGet.org to get intellisense for the NLog.config configuration file inside Visual Studio
  - `Install-Package NLog.Schema`

# TODO
- [ ] Better instructions
- [ ] NLog configuration sample
