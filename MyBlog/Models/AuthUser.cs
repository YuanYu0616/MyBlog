using Microsoft.EntityFrameworkCore;

namespace MyBlog.Models
{
    [Keyless]
    public class AuthUser
    {
        /// <summary>
        /// 用戶名稱
        /// </summary>
        public string Account { get; set; } = string.Empty;
        /// <summary>
        /// 用戶密碼
        /// </summary>
        public string Pwd { get; set; } = string.Empty;

        public string? HttpMethod { get; set; }

        public string? FunctionPath { set; get; }
        public bool Enabled { get; set; }
    }
}
