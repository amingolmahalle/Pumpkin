namespace Pumpkin.Common;

public static class AssemblyScanner
{
    public static IEnumerable<Type> AllTypes()
    {
        return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes());
    }
}