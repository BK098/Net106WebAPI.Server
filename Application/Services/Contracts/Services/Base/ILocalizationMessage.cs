using FluentValidation.Results;

namespace Application.Services.Contracts.Services.Base
{
    public interface ILocalizationMessage
    {
        Dictionary<string, List<object>> GetMessageError(Dictionary<string, List<object>> errors, List<ValidationFailure> errorList);
        Dictionary<string, List<object>> GetMessageData(Dictionary<string, List<object>> errors, List<ValidationFailure> errorList);
        Dictionary<string, object> GetMessageToken(Dictionary<string, object> data);
    }
}