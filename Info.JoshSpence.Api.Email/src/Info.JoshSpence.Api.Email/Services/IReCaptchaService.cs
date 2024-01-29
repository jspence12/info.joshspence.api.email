using Info.JoshSpence.Api.Email.Models;

namespace Info.JoshSpence.Api.Email.Services
{
    public interface IReCaptchaService
    {
        public Task<bool> DidUserPassReCaptcha(ReCaptchaRequest request);
    }
}
