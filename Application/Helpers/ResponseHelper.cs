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
            string message = string.IsNullOrEmpty(entityName) ? errorCode.GetDescription() : errorCode.GetDescription(entityName);
            return new UserMangeResponse
            {
                Message = message,
                Data = localization.GetMessageData(new Dictionary<string, List<object>>(), validationErrors),
                Errors = localization.GetMessageError(new Dictionary<string, List<object>>(), validationErrors),
                IsSuccess = false
            };
        } 
        public static UserMangeResponse ErrorResponse(
           ErrorCode errorCode,
           string entityName = "")
        {
            string message = string.IsNullOrEmpty(entityName) ? errorCode.GetDescription() : errorCode.GetDescription(entityName);
            return new UserMangeResponse
            {
                Message = message,
                IsSuccess = false
            };
        }
        public static UserMangeResponse SuccessResponse(
            SuccessCode successCode,
            string entityName = "",
            string token = null)
        {
            var response = new UserMangeResponse
            {
                IsSuccess = true
            };
            string message = string.IsNullOrEmpty(entityName) ? successCode.GetDescription() : successCode.GetDescription(entityName);

            if (successCode == SuccessCode.LoginSuccess && token != null)
            {
                response.Message = message;
                response.Data["Token"] = new List<object> { token };
            }
            else
            {
                response.Message = message;
            }

            return response;
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