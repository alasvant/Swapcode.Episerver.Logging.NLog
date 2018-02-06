# Swapcode.Episerver.Logging.NLog
Implementation to change Episerver default logging framework [log4net](https://logging.apache.org/log4net/) with [NLog](http://nlog-project.org/).

This implementation uses the Episerver supported way of [changing the logging framework](https://world.episerver.com/documentation/developer-guides/CMS/logging/) by using the LoggerFactory assembly attribute.

Available for Episerver 11.x, EPiServer.Framework (>= 11.1.0 && < 12.0.0). Most likely could also allow install for 10.x but I think you should first upgrade your Episerver solution to the latest version because of all the imrovements done to Episerver and then change the logging framework :wink:

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
  - in package manager console `Install-Package NLog.Schema` or use the NuGet UI in Visual Studio
- add configuration file for NLog, see [NLog configuration file locations](https://github.com/nlog/nlog/wiki/Configuration-file#configuration-file-locations)
  - I would say that the most common name is `NLog.config` and placed to the web application root (same place where you have your `web.config`)

# Sample NLog.config
```
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
      autoReload="true"
      internalLogFile="[PUT-YOUR-ABSOLUTE-PATH-HERE]\nlog-internal-log.txt"
      internalLogLevel="Info"
      internalLogToTrace="false">
    <!-- NOTE: internalLogFile needs to be absolute path  -->
    <!-- NLog internal logging on just for demo purposes -->
    
    <!-- set the log and archive directory with variables, NOTE: file target by default creates the directory if it doesn't exist -->
    <variable name="logDir" value="App_Data\Logs" />
    <variable name="logArchiveDir" value="App_Data\LogsArchive" />
    <!-- NOTE: DO NOT USE App_Data folder in production or other similiar environments! Only in development. You were warned. -->

    <targets>
        <!-- Just a silly sample for archiving, archiveEveryMinute used just for demo purposes -->
        <target name="logfile" xsi:type="File" fileName="${logDir}\EpiAppLog.log"
                archiveFileName="${logArchiveDir}\EpiAppLog.{##}.log"
                archiveEvery="Minute"
                archiveNumbering="Sequence"
                maxArchiveFiles="15"/>
        
        <!-- JSON layout sample -->
        <target xsi:type="File" name="jsonfile" fileName="${logDir}\EpiAppJsonLog.json"
                archiveFileName="${logArchiveDir}\EpiAppJsonLog.{##}.json"
                archiveEvery="Minute"
                archiveNumbering="Sequence"
                maxArchiveFiles="15">
            <layout xsi:type="JsonLayout">
                <attribute name="time" layout="${longdate}" />
                <attribute name="level" layout="${level:upperCase=true}"/>
                <attribute name="logger" layout="${logger}"/>
                <attribute name="controller" layout="${aspnet-mvc-controller}" />
                <attribute name="action" layout="${aspnet-mvc-action}" />
                <attribute name="request-method" layout="${aspnet-request-method}" />
                <attribute name="request-url" layout="${aspnet-request-url:IncludeQueryString=true}" />
                <attribute name="message" layout="${message}" />
                <attribute name="exception" layout="${exception}" />
            </layout>
        </target>
    </targets>

    <rules>
        <!-- write all warnings and above -->
        <logger name="*" minlevel="Warn" writeTo="jsonfile" />
        <logger name="*" minlevel="Warn" writeTo="logfile" final="true"/>
        
        <!-- write everything from the app -->
        <logger name="EpiElevenWebsite*" minlevel="Trace" writeTo="jsonfile" />
        <logger name="EpiElevenWebsite*" minlevel="Trace" writeTo="logfile" final="true" />
    </rules>
</nlog>
```

# TODO
- [x] Better instructions
- [ ] NLog configuration sample
