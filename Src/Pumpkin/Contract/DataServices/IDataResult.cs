namespace Pumpkin.Contract.DataServices;

public interface IDataResult
{
    int Page { get; }

    long TotalCount { get; }

    object Data { get; }
}