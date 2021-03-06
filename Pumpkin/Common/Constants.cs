
namespace Pumpkin.Common
{
    public static class Constants
    {
        public const int FluentValidationHttpStatusCode = 710;
        public const string MobileNumberPattern = "(^(09)[0-9]{9}$)";
        public const string EmailPattern = @"([^@|\s]+@[^@]+\.[^@|\s]+$)";
        public const string NationalCodePattern = @"(^\d{10}$)";
        public const string HostTitle = "Versioned Api";
        public const string HostApiRouteDiscriminator = "api/";
    }
}