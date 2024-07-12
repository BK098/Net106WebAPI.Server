using FluentValidation;

namespace Application.Services.Models.AuthenticationModels
{
    public class RegisterModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? ImagePath { get; set; }
    }
    public class RegisterModelValidator: AbstractValidator<RegisterModel>
    {
        public RegisterModelValidator()
        {
            
        }
    }
}