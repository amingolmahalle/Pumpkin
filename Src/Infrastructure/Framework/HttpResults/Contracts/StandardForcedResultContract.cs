namespace Pumpkin.Infrastructure.Framework.HttpResults.Contracts;

public class StandardForcedResultContract
{
    public string Level { get; set; }
    public string Title { get; set; }
    public int Status { get; set; }
    public string Message { get; set; }
    public string Description { get; set; }
    public string TraceId { get; set; }
    public Dictionary<string, object> Errors { get; set; }
}