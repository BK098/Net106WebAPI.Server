using System.ComponentModel.DataAnnotations;

namespace Application.Services.Models.UserModels.Base
{
    public class UserBaseDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? ImagePath { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsDeleted { get; set; } // Thêm trường này

    }
}
