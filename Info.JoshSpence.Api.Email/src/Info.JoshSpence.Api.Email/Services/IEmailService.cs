using Info.JoshSpence.Api.Email.Models;

namespace Info.JoshSpence.Api.Email.Services;

public interface IEmailService
{
    public Task<bool> SendEmailAsync(EmailRequest emailRequest);
}
