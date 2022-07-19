using System.Reflection;
using NLog;
using NLog.Common;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using NLog.Targets.ElasticSearch;

namespace Infrastructure.Framework.Logging.NLog;

public static class NLogConfigurationManager
{
    public static void Configure()
    {
        LogManager.ThrowExceptions = true;
        InternalLogger.LogToConsole = true;

        var loggingConfiguration = new LoggingConfiguration();

#if DEBUG
        AddColoredConsole(loggingConfiguration);
#endif
        AddKibanaConfiguration(loggingConfiguration);

        LogManager.Configuration = loggingConfiguration;
    }

    public static void AddKibanaConfiguration(LoggingConfiguration loggingConfiguration)
    {
        ConfigurationItemFactory.Default.RegisterItemsFromAssembly(Assembly.Load("NLog.Targets.ElasticSearch"));

        var target = new ElasticSearchTarget
        {
            Name = "ElasticTarget",
            Index = "platform-log-${date:format=yyyy.MM.dd}",
            Layout = "${message}",
            Uri = ConfigManager.GetConnectionString("SearchEngine")
        };

        target.Fields.Add(new Field
        {
            Name = "Application",
            Layout = new SimpleLayout("${appdomain:format=long}")
        });
        target.Fields.Add(new Field
        {
            Name = "CallSite",
            Layout = new SimpleLayout("${callsite}")
        });
        target.Fields.Add(new Field
        {
            Name = "StackTrace", Layout = new SimpleLayout("${stacktrace}")
        });
        target.Fields.Add(new Field
        {
            Name = "Url", Layout = new SimpleLayout("${aspnet-request-url}")
        });
        target.Fields.Add(new Field
        {
            Name = "SequenceId", Layout = new SimpleLayout("${sequenceid}")
        });
        target.Fields.Add(new Field
        {
            Name = "Referrer", Layout = new SimpleLayout("${aspnet-request-referrer}")
        });
        target.Fields.Add(new Field
        {
            Name = "TraceIdentifier", Layout = new SimpleLayout("${aspnet-TraceIdentifier}")
        });
        // target.Fields.Add(new Field
        // {
        //     Name = "UserName", Layout = new SimpleLayout("${username}")
        // });
        target.Fields.Add(new Field
        {
            Name = "Controller", Layout = new SimpleLayout("${aspnet-MVC-Controller}")
        });
        target.Fields.Add(new Field
        {
            Name = "Action", Layout = new SimpleLayout("${aspnet-MVC-Action}")
        });
        target.Fields.Add(new Field
        {
            Name = "Level", Layout = new SimpleLayout("${level}")
        });
        target.Fields.Add(new Field
        {
            Name = "Message", Layout = new SimpleLayout("${message}")
        });

        loggingConfiguration.AddTarget(target);
        var rules = new LoggingRule("*", target);
            
        rules.EnableLoggingForLevel(LogLevel.Info);
        rules.EnableLoggingForLevel(LogLevel.Error);
        rules.EnableLoggingForLevel(LogLevel.Warn);
        rules.EnableLoggingForLevel(LogLevel.Debug);
        rules.EnableLoggingForLevel(LogLevel.Fatal);
            
        loggingConfiguration.LoggingRules.Add(rules);
    }

    public static void AddColoredConsole(LoggingConfiguration loggingConfiguration)
    {
        var target = new ColoredConsoleTarget("ColoredConsoleTarget")
        {
            Layout =
                "${logger} ${newline}${level:uppercase=true}:${longdate} ${newline}Action:${aspnet-mvc-action} ${newline}${message}${newline}${newline}",
            UseDefaultRowHighlightingRules = true
        };

        var blackHole = new ConsoleTarget("blackHole");

        target.WordHighlightingRules.Add(new ConsoleWordHighlightingRule
        {
            Regex = "^INFO:(.*)", ForegroundColor = ConsoleOutputColor.Green
        });
        target.WordHighlightingRules.Add(new ConsoleWordHighlightingRule
        {
            Regex = "^Action:(.*)", ForegroundColor = ConsoleOutputColor.Green
        });
        target.WordHighlightingRules.Add(new ConsoleWordHighlightingRule
        {
            Regex = "^ERROR:(.*)", ForegroundColor = ConsoleOutputColor.Red
        });

        loggingConfiguration.AddTarget(target);

        loggingConfiguration.AddRule(
            LogLevel.Warn,
            LogLevel.Fatal,
            target);

        loggingConfiguration.AddRule(
            LogLevel.Warn,
            LogLevel.Fatal,
            target,
            "Microsoft.EntityFrameworkCore.*");

        loggingConfiguration.AddRule(
            LogLevel.Trace,
            LogLevel.Fatal,
            blackHole,
            "Microsoft.AspNetCore.*");
    }
}