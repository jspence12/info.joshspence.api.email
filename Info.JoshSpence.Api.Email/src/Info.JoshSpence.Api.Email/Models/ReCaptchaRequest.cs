namespace Info.JoshSpence.Api.Email.Models
{
    public class ReCaptchaRequest
    {
        public string Secret { get; set; }
        public string Response { get; set; }
        public string RemoteIp { get; set; }
    }
}
