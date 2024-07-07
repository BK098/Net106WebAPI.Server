using Application.Services.Contracts.Services.Base;
using FluentValidation.Results;

namespace Application.Services.Localizations
{
    public class LocalizationMessage : ILocalizationMessage
    {
        public Dictionary<string, List<object>> GetMessageError(Dictionary<string, List<object>> errors, List<ValidationFailure> errorList)
        {
            foreach (var error in errorList)
            {
                if (errors.ContainsKey(error.PropertyName))
                {
                    errors[error.PropertyName].Add(error.ErrorMessage);
                }
                else
                {
                    errors[error.PropertyName] = new List<object> { error.ErrorMessage };
                }
            }
            return errors;
        }
        public Dictionary<string, List<object>> GetMessageData(Dictionary<string, List<object>> data, List<ValidationFailure> errorList)
        {
            foreach (var error in errorList)
            {
                if (data.ContainsKey(error.PropertyName))
                {
                    data[error.PropertyName].Add(error.AttemptedValue);
                }
                else
                {
                    data[error.PropertyName] = new List<object> { error.AttemptedValue };
                }
            }
            return data;
        }
    }
}
