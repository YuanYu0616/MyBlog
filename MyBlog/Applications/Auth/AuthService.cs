using MyBlog.ViewModels.Auth;
using MyBlog.DbAccess;
using Microsoft.EntityFrameworkCore;

namespace MyBlog.Applications.Auth
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _db;

        public AuthService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> LoginUserCheckPwd(LoginRequest model)
        {
            return await _db.AuthUsers.Where(o => o.Pwd == model.Pwd).AnyAsync();
        }
    }
}
