using System.ComponentModel.DataAnnotations;

namespace Pumpkin.Domain.Contracts.Inputs.Policy;

public class PolicyRegisterContract : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        throw new NotImplementedException();
    }
}