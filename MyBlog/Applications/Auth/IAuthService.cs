using MyBlog.ViewModels.Auth;

namespace MyBlog.Applications.Auth
{
    public interface IAuthService
    {
        public Task<bool> LoginUserCheckPwd(LoginRequest model);
    }
}
