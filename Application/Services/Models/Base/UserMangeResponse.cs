namespace Application.Services.Models.Base
{
    public class UserMangeResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public Dictionary<string, object>? Data { get; set; } = new Dictionary<string, object>();
        public Dictionary<string, object>? Errors { get; set; } = new Dictionary<string, object>();
    }
}
