using System.Runtime.Serialization;

namespace Framework.Contracts.Response;

[DataContract]
public class PageableData<TData> : Pageable
{
    /// <summary>
    /// Current page items
    /// </summary>
    [DataMember(Order = 1)]
    public IEnumerable<TData> Results { get; set; }
}