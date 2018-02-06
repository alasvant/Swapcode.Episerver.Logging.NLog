# Swapcode.Episerver.Logging.NLog
Implementation to change Episerver default logging framework [log4net](https://logging.apache.org/log4net/){:target="_ blank"} with [NLog](http://nlog-project.org/){:target="_ blank"}.

This implementation uses the Episerver supported way of [changing the logging framework](https://world.episerver.com/documentation/developer-guides/CMS/logging/){:target="_ blank"} by using the LoggerFactory assembly attribute.

Available for Episerver 11.x, EPiServer.Framework (>= 11.1.0 && < 12.0.0). Most likely could also allow install for 10.x but I think you should first upgrade your Episerver solution to the latest version because of all the imrovements done to Episerver and then change the logging framework :wink:

# Before installing this package
- remove the EPiServer.Logging.Log4net NuGet package
- remove log4net NuGet package
- you can also remove the EPiServerLog.config now or later as it is not used anymore

# Install with NuGet Package Manager
Currently the NuGet package is available from the [releases](https://github.com/alasvant/Swapcode.Episerver.Logging.NLog/releases){:target="_ blank"}. NuGet feed address coming..

The package includes NLog and NLog.Web NuGet packages that are required for logging and to format the logs.

Install doesn't include NLog configuration file which is described [here](https://github.com/nlog/nlog/wiki/Configuration-file){:target="_ blank"}.

# After installing package
- (*optional*) install NLog.Schema NuGet package from NuGet.org to get intellisense for the NLog.config configuration file inside Visual Studio
  - in package manager console `Install-Package NLog.Schema` or use the NuGet UI in Visual Studio
- add configuration file for NLog, see [NLog configuration file locations](https://github.com/nlog/nlog/wiki/Configuration-file#configuration-file-locations){:target="_ blank"}
  - I would say that the most common name is `NLog.config` and placed to the web application root (same place where you have your `web.config`)

# Sample NLog.config

# TODO
- [x] Better instructions
- [ ] NLog configuration sample
