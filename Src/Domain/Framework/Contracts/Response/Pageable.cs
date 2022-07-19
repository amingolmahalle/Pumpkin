using System.Runtime.Serialization;

namespace Framework.Contracts.Response;

[DataContract]
public class Pageable
{
    /// <summary>
    /// Current page number
    /// </summary>
    [DataMember(Order = 1)]
    public int Page { get; set; }

    /// <summary>
    /// Total pages count
    /// </summary>
    [DataMember(Order = 2)]
    public int Pages { get; set; }

    /// <summary>
    /// Items per page
    /// </summary>
    [DataMember(Order = 3)]
    public int Size { get; set; }

    /// <summary>
    /// Total items count
    /// </summary>
    [DataMember(Order = 4)]
    public int Count { get; set; }
}