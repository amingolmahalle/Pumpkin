using System.ComponentModel;

namespace Pumpkin.Domain.Entities.Order.Enumerations;

public enum PolicyStates
{
    [Description("در حال انتظار")] Pending = 100,
    [Description("فعال")] Activated = 200,
    [Description("ابطال - انصراف داده شد")] Cancelled = 300,
}