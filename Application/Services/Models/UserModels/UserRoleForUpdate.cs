namespace Application.Services.Models.UserModels
{
    public class UserRoleForUpdate
    {
        public string Role { get; set; }
        public bool IsDeleted { get; set; } // Thêm trường này

    }
}