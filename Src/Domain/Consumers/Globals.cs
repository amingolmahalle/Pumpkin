namespace Pumpkin.Domain.Consumers;

public static class Globals
{
    public static class Channels
    {
        public const string WeatherChannel = "WEATHER";
    }

    public static class Events
    {
        public static class Routes
        {
            public const string WeatherRoute = "ICX_ALBB_WEATHER";
        }

        public const string NotificationsBus = "NOTIFICATIONS";
        public const string StateChangesBus = "STATE_CHANGES";
    }
}