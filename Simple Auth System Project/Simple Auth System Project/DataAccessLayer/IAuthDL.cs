using Simple_Auth_System_project.Model;
using SimpleAuthSystem.Model;

namespace SimpleAuthSystem.DataAccessLayer
{
    public interface IAuthDL
    {
        public Task<SignUpResponse> SignUp(SignUpRequest request);

        public Task<SignInResponse> SignIn(SignInRequest request);
      
    }
}