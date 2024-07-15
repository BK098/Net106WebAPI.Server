using Application.Services.Models.UserModels.Base;

namespace Application.Services.Models.UserModels
{
    public class UserForView
    {
       public IList<UserForViewItems> Users { get; set; } = new List<UserForViewItems>();
    }
    public class UserForViewItems: UserBaseDto
    {
        public string Id { get; set; }
    }
}