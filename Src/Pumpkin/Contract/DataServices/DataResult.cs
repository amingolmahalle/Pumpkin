namespace Pumpkin.Contract.DataServices;

internal class DataResult : IDataResult
{
    public int Page { get; }

    public long TotalCount { get; }

    public object Data { get; }

    public DataResult(object data, long count)
    {
        TotalCount = count;
        Data = data;
    }

    public DataResult(object data, long count, int page)
    {
        Page = page;
        TotalCount = count;
        Data = data;
    }
}