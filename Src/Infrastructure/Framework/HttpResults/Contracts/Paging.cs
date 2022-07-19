namespace Infrastructure.Framework.HttpResults.Contracts;

public class Paging
{
    public int Size { get; set; }
    public int Page { get; set; }

    public int Count { get; set; }
    public int Pages { get; set; }
}