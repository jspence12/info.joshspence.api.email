using Amazon.SimpleEmailV2;
using Amazon.SimpleEmailV2.Model;
using Info.JoshSpence.Api.Email.Models;

namespace Info.JoshSpence.Api.Email.Services;

public class EmailService : IEmailService
{
    private readonly IServiceProvider _provider;
    private readonly ILogger<EmailService> _logger;
    private const string FromAddress = "REPLACE ME";
    private const string ToEmailAddress = "REPLACE ME";
    public EmailService(IServiceProvider provider, ILogger<EmailService> logger)
    {
        _provider = provider;
        _logger = logger;
    }

    public async Task<bool> SendEmailAsync(EmailRequest email)
    {
        var request = BuildRequest(email);
        _logger.LogInformation($"sending email for {email.Name} at {email.Email}");

        try
        {

            using var client = _provider.GetRequiredService<IAmazonSimpleEmailServiceV2>();
            await client.SendEmailAsync(request);
            _logger.LogInformation($"email for {email.Name} at {email.Email} sent");
        }
        catch (Exception ex)
        {
            _logger.LogError($"failed to send email for {email.Name} at {email.Email}: ${ex.Message}");
            throw;
        }

        return true;
    }

    private SendEmailRequest BuildRequest(EmailRequest email)
    {
        return new SendEmailRequest
        {
            FromEmailAddress = FromAddress,
            Destination = new Destination
            {
                ToAddresses = new List<string> { ToEmailAddress }
            },
            Content = new EmailContent
            {
                Simple = new Message
                {
                    Subject = new Content
                    {
                        Data = $"Website message from {email.Email}",
                        Charset = "UTF-8"
                    },
                    Body = new Body
                    {
                        Text = new Content
                        {
                            Data = $"From: {email.Name}\n"
                                + $"Email: {email.Email}\n\nMessage:\n{email.Message}",
                            Charset = "UTF-8"
                        }
                    }
                }
            }
        };
    }
}
