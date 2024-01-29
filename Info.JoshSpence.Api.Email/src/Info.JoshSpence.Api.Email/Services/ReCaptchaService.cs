using Info.JoshSpence.Api.Email.Models;
using Newtonsoft.Json;

namespace Info.JoshSpence.Api.Email.Services;

public class ReCaptchaService : IReCaptchaService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ReCaptchaService> _logger;

    private const string EndPoint = "siteverify";

    public ReCaptchaService(HttpClient httpClient, ILogger<ReCaptchaService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<bool> DidUserPassReCaptcha(ReCaptchaRequest request)
    {
        var content = new StringContent(JsonConvert.SerializeObject(request));
        _logger.LogInformation($"Sending POST to ${_httpClient.BaseAddress}{EndPoint} with payload {content}");
        try
        {
            var response = await _httpClient.PostAsync(EndPoint, content);
            _logger.LogInformation($"ReCaptcha response completed with status code {response.StatusCode}");
            var result = JsonConvert.DeserializeObject<ReCaptchaResponse>(await response.Content.ReadAsStringAsync());
            return result?.Success == true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Request to ReCaptcha Failed: {ex.Message}");
            throw;
        }
    }
}
