using FluentValidation.Results;

namespace Application.Services.Contracts.Services.Base
{
    public interface ILocalizationMessage
    {
        Dictionary<string, object> GetMessageError(Dictionary<string, object> errors, List<ValidationFailure> errorList);
        Dictionary<string, object> GetMessageData(Dictionary<string, object> errors, List<ValidationFailure> errorList);
    }
}