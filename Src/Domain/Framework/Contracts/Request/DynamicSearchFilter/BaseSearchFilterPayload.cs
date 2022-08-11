using System.ComponentModel.DataAnnotations;
using Pumpkin.Domain.Framework.Contracts.Request.DynamicSearchFilter.Validation;

namespace Pumpkin.Domain.Framework.Contracts.Request.DynamicSearchFilter;

public class BaseSearchFilterPayload : BaseFilterValidation, IValidatableObject
{
    public List<SortDto> Orders { get; set; } = new();

    public override void CustomValidation(string field, string value)
    {
    }

    public List<RestrictionDto> Restrictions { get; set; } = new();

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        CheckFiltersValidation(Restrictions);
        yield break;
    }
}