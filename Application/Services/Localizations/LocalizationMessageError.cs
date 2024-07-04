using Application.Services.Contracts.Services.Base;
using FluentValidation.Results;

namespace Application.Services.Localizations
{
    public class LocalizationMessageError : ILocalizationMessageError
    {
        public Dictionary<string, object> GetMessageError(Dictionary<string, object> errors, List<ValidationFailure> errorList)
        {
            foreach (var error in errorList)
            {
                errors.Add(error.PropertyName, error.ErrorMessage);
            }
            return errors;
        }
    }
}
