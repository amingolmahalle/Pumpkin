using Framework.Exceptions;
using Pumpkin.Domain.Framework.Exceptions;
using Pumpkin.Domain.Framework.Extensions;

namespace Pumpkin.Domain.Framework.Contracts.Request.DynamicSearchFilter.Validation;

public abstract class BaseFilterValidation
{
    public abstract void CustomValidation(string field, string value);

    private void _rangeTypeValidation(string field, string minValue, string maxValue)
    {
        var isValid = minValue.HasValue() && maxValue.HasValue();
        if (isValid == false)
            throw new Dexception(Situation.Make(SitKeys.BadRequest),
                new List<KeyValuePair<string, string>> {new(":پیام:", "محدوده عددی ارسال شده نامعتبر است.")});
    }

    private void _collectionTypeValidation(string field, List<string> values)
    {
        var isValid = values.HasItem();
        if (!isValid)
            throw new Dexception(Situation.Make(SitKeys.BadRequest),
                new List<KeyValuePair<string, string>> {new(":پیام:", "گزینه‌های قابل انتخاب لیست ارسال نشده اند.")});
    }

    private void _simpleTypeValidation(string field, string value, string operation)
    {
        var isValid = value.HasValue() && operation.HasValue();
        if (isValid == false)
            throw new Dexception(Situation.Make(SitKeys.BadRequest),
                new List<KeyValuePair<string, string>> {new(":پیام:", "مقدار و روش فیلتر مشخص نشده است.")});
    }

    public void CheckFiltersValidation(List<RestrictionDto> restrictions)
    {
        foreach (var restriction in restrictions)
        {
            switch (restriction.Type)
            {
                case "range":
                    _rangeTypeValidation(restriction.Field, restriction.MinValue, restriction.MaxValue);
                    CustomValidation(restriction.Field, restriction.MinValue);
                    CustomValidation(restriction.Field, restriction.MaxValue);
                    break;
                case "collection":
                    _collectionTypeValidation(restriction.Field, restriction.Values);

                    foreach (var value in restriction.Values)
                        CustomValidation(restriction.Field, value);

                    break;
                case "simple":
                    _simpleTypeValidation(restriction.Field, restriction.Value, restriction.Operation);
                    CustomValidation(restriction.Field, restriction.Value);
                    break;
                default:
                    throw new Dexception(Situation.Make(SitKeys.BadRequest),
                        new List<KeyValuePair<string, string>>
                        {
                            new(":پیام:", "نوع فیلد «:نوع:» در فیلترهای ارسال شده نامعتبر است."
                                .Replace(":نوع:", restriction.Type))
                        });
            }
        }
    }
}