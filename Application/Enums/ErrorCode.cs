using System.ComponentModel;

namespace Application.Enums
{
    public enum ErrorCode
    {
        /// <summary>
        /// Có vấn đề khi thực hiện { tên entity }
        /// </summary>
        [Description("Có vấn đề khi thực hiện {0}")]
        ValidationError,
        /// <summary>
        /// Đã xảy ra lỗi khi tạo { tên entity }
        /// </summary>
        [Description("Đã xảy ra lỗi khi tạo {0}")]
        CreateError,
        /// <summary>
        /// Đã xảy ra lỗi khi cập nhật { tên entity}
        /// </summary>
        [Description("Đã xảy ra lỗi khi cập nhật {0}")]
        UpdateError,
        /// <summary>
        /// "Đã xảy ra lỗi khi xóa { tên entity }
        /// </summary>
        [Description("Đã xảy ra lỗi khi xóa {0}")]
        DeleteError,
        /// <summary>
        /// { Tên entity } đã tồn tại
        /// </summary>
        [Description("{0} đã tồn tại")]
        Existed,
        /// <summary>
        /// Không tìm thấy { với tên entity }
        /// </summary>
        [Description("Không tìm thấy {0}")]
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