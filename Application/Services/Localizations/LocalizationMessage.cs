using Application.Services.Contracts.Services.Base;
using FluentValidation.Results;

namespace Application.Services.Localizations
{
    public class LocalizationMessage : ILocalizationMessage
    {
        public Dictionary<string, object> GetMessageError(Dictionary<string, object> errors, List<ValidationFailure> errorList)
        {
            foreach (var error in errorList)
            {
                errors.Add(error.PropertyName, error.ErrorMessage);
            }
            return errors;
        }
        public Dictionary<string, object> GetMessageData(Dictionary<string, object> data, List<ValidationFailure> valueList)
        {
            foreach (var value in valueList)
            {
                data[value.PropertyName] = value.AttemptedValue;
            }
            return data;
        }
    }
}
