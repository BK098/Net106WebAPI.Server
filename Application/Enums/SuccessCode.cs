using System.ComponentModel;

namespace Application.Enums
{
    public enum SuccessCode
    {
        /// <summary>
        /// Tạo { entity } thành công
        /// </summary>
        [Description("Tạo {0} thành công")]
        CreateSuccess,
        /// <summary>
        /// Cập nhật { entity } thành công
        /// </summary>
        [Description("Cập nhật {0} thành công")]
        UpdateSuccess,
        /// <summary>
        /// Xóa { entity } thành công
        /// </summary>
        [Description("Xóa {0} thành công")]
        DeleteSuccess,
        /// <summary>
        /// Đăng nhập thành công
        /// </summary>
        [Description("Đăng nhập thành công")]
        LoginSuccess
    }
}