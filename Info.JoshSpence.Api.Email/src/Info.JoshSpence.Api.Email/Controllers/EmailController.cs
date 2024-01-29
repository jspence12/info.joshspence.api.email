using Info.JoshSpence.Api.Email.Models;
using Info.JoshSpence.Api.Email.Services;
using Microsoft.AspNetCore.Mvc;

namespace Info.JoshSpence.Api.Email.Controllers;


[ApiController]
[Route("[controller]")]
public class EmailController : ControllerBase
{
    private readonly IEmailService _emailProvider;
    private readonly IReCaptchaService _reCaptchaService;
    private readonly ILogger<EmailController> _logger;

    public EmailController(ILogger<EmailController> logger, IEmailService emailProvider, IReCaptchaService recpatchaService)
    {
        _logger = logger;
        _emailProvider = emailProvider;
        _reCaptchaService = recpatchaService;
    }

    [HttpPost()]
    public async Task<ActionResult> Post(EmailRequest request)
    {
        _logger.LogInformation($"Received request for ${request.Name} at ${request.Email}");

        //if (!await _reCaptchaService.DidUserPassReCaptcha(new ReCaptchaRequest()))
        //{
        //    _logger.LogInformation($"Email for ${request.SenderName} at ${request.SenderEmail} failed reCaptcha");
        //    return Unauthorized();
        //}

        await _emailProvider.SendEmailAsync(request);

        _logger.LogInformation($"Completed request for ${request.Name} at ${request.Email}");
        return NoContent        ();
    }
}
