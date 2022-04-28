namespace Pumpkin.Common;

public static class Constants
{
    public const int FluentValidationHttpStatusCode = 710;
    public const string MobileNumberPattern = @"(^09\d{9}$)|(^\+989\d{9}$)|(^9\d{9}$)|(^989\d{9}$)";
    public const string EmailPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
    public const string NationalCodePattern = @"(^\d{10}$)";
    public const string HostTitle = "Versioned Api";
    public const string HostApiRouteDiscriminator = "api/";
}