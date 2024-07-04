using FluentValidation.Results;

namespace Application.Services.Contracts.Services.Base
{
    public interface ILocalizationMessageError
    {
        Dictionary<string, object> GetMessageError(Dictionary<string, object> errors, List<ValidationFailure> errorList);
    }
}
