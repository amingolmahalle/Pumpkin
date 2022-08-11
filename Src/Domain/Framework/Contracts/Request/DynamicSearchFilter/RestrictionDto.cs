namespace Pumpkin.Domain.Framework.Contracts.Request.DynamicSearchFilter;

public class RestrictionDto
{
    public string Type { get; set; }

    public string Field { get; set; }

    public List<string> Values { get; set; }

    public string MinValue { get; set; }

    public string MaxValue { get; set; }

    public string Value { get; set; }

    public string Operation { get; set; }
}