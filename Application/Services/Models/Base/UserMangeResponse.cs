namespace Application.Services.Models.Base
{
    public class UserMangeResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public Dictionary<string, List<object>>? Data { get; set; } = new Dictionary<string, List<object>>();
        public Dictionary<string, List<object>>? Errors { get; set; } = new Dictionary<string, List<object>>();
    }
}
