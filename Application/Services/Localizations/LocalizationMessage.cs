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
                if (!errors.ContainsKey(error.PropertyName))
                {
                    errors[error.PropertyName] = new List<object>();
                }
                errors[error.PropertyName].Add(error.ErrorMessage);
            }
            return errors;
        }

        public Dictionary<string, List<object>> GetMessageData(Dictionary<string, List<object>> data, List<ValidationFailure> valueList)
        {
            foreach (var value in valueList)
            {
                if (!data.ContainsKey(value.PropertyName))
                {
                    data[value.PropertyName] = new List<object>();
                }
                data[value.PropertyName].Add(value.AttemptedValue);
            }
            return data;
        }

        public Dictionary<string, object> GetMessageToken(Dictionary<string, object> data)
        {
            return data;
        }
    }
}
