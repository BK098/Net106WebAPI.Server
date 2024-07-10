using System.ComponentModel;

namespace Application.Enums
{
    public enum ErrorCode
    {
        [Description("Có vấn đề khi tạo {0}")]
        ValidationError,
        [Description("Đã xảy ra lỗi khi tạo {0}")]
        CreateError,
        [Description("Đã xảy ra lỗi khi cập nhật {0}")]
        UpdateError,
        [Description("Đã xảy ra lỗi khi xóa {0}")]
        DeleteError,
        [Description("{0} đã tồn tại")]
        Existed,
        [Description("Không tìm thấy {0}")]
        NotFound,
    }
}