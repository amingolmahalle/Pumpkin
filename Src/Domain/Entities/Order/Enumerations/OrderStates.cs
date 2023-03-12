using System.ComponentModel;

namespace Pumpkin.Domain.Entities.Order.Enumerations;

public enum OrderStates
{
    [Description("در حال انتظار")] Pending = 100,
    [Description("پرداخت شده")] Paid = 110,
    [Description("تایید شده")] Confirmed = 200,
    [Description("ابطال - انصراف داده شد")] Cancelled = 300,
}