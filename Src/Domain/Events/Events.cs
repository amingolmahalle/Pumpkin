namespace Pumpkin.Domain.Events;

public static class Events
{
    public static class Routes
    {
        public const string Panel = "ICX_EEB_PANEL";
        public const string Sales = "ICX_EEB_SALES";
    }

    public const string NotificationsBus = "NOTIFICATIONS";
    public const string StateChangesBus = "STATE_CHANGES";
}