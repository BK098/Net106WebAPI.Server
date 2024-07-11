using Application.Enums;
using Application.Services.Contracts.Services.Base;
using Application.Services.Models.Base;
using FluentValidation.Results;
using System.ComponentModel;
using System.Reflection;

namespace Application.Helpers
{
    public static class ResponseHelper
    {
        public static UserMangeResponse ErrorResponse(
           ErrorCode errorCode,
           List<ValidationFailure> validationErrors,
           ILocalizationMessage localization,
           string entityName = "")
        {
            string message = entityName == string.Empty ? errorCode.GetDescription() : errorCode.GetDescription(entityName);
            return new UserMangeResponse
            {
                Message = message,
                Data = localization.GetMessageData(new Dictionary<string, List<object>>(), validationErrors),
                Errors = localization.GetMessageError(new Dictionary<string, List<object>>(), validationErrors),
                IsSuccess = false
            };
        }
        public static UserMangeResponse SuccessResponse(
          SuccessCode successCode,
          string entityName = "")
        {
            string message = entityName == string.Empty ? successCode.GetDescription() : successCode.GetDescription(entityName);
            return new UserMangeResponse
            {
                Message = message,
                IsSuccess = true
            };
        }
        private static string GetDescription(this Enum value, params object[] args)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute = field.GetCustomAttribute<DescriptionAttribute>();
            string description = attribute == null ? value.ToString() : attribute.Description;

            return string.Format(description, args);
        }
    }
}