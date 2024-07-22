using Application.Services.Models.UserModels.Base;

namespace Application.Services.Models.UserModels
{
    public class UserForView: UserBaseDto
    {
        public string Id { get; set; }
        public string? Role { get; set; }
    }
}