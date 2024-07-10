using System.ComponentModel;

namespace Application.Enums
{
    public enum SuccessCode
    {
        [Description("Tạo {0} thành công")]
        CreateSuccess,
        [Description("Cập nhật {0} thành công")]
        UpdateSuccess,
        [Description("Xóa {0} thành công")]
        DeleteSuccess
    }
}