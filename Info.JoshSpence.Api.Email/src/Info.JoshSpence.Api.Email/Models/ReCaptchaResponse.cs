using System.Text.Json.Serialization;

namespace Info.JoshSpence.Api.Email.Models
{
    /// <summary>
    /// Response object from Google's reCaptcha server.
    /// See: https://developers.google.com/recaptcha/docs/verify
    /// </summary>
    public class ReCaptchaResponse
    {
        public bool Success { get; set; }

        [JsonPropertyName("challenge_ts")]
        public DateTime ChallengeTimeStamp { get; set; }

        public string HostName { get; set; } = string.Empty;

        [JsonPropertyName("error-codes")]
        public List<string> ErrorCodes { get; set; } = new List<string>();
    }
}
