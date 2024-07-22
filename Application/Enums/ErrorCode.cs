using System.ComponentModel;

namespace Application.Enums
{
    public enum ErrorCode
    {
        /// <summary>
        /// Có vấn đề khi thực hiện {0}
        /// </summary>
        [Description("Có vấn đề khi thực hiện { entity }")]
        ValidationError,
        /// <summary>
        /// Đã xảy ra lỗi khi tạo {0}
        /// </summary>
        [Description("Đã xảy ra lỗi khi tạo { entity }")]
        CreateError,
        /// <summary>
        /// Đã xảy ra lỗi khi cập nhật {0}
        /// </summary>
        [Description("Đã xảy ra lỗi khi cập nhật { entity }")]
        UpdateError,
        /// <summary>
        /// "Đã xảy ra lỗi khi xóa {0}
        /// </summary>
        [Description("Đã xảy ra lỗi khi xóa { entity }")]
        DeleteError,
        /// <summary>
        /// { Tên entity } đã tồn tại
        /// </summary>
        [Description("{ Tên entity } đã tồn tại")]
        Existed,
        /// <summary>
        /// Không tìm thấy { với tên entity }
        /// </summary>
        [Description("Không tìm thấy { entity }")]
        NotFound,
        /// <summary>
        /// Mật khẩu đã sai
        /// </summary>
        [Description("Mật khẩu đã sai")]
        WrongPassword,
        /// <summary>
        /// {0} có đối tượng bị lỗi
        /// </summary>
        [Description("{0} có đối tượng bị lỗi")]
        OperationFailed,
        /// <summary>
        /// Lỗi {0}
        /// </summary>
        [Description("Lỗi {0}")]
        Exception
    }
}