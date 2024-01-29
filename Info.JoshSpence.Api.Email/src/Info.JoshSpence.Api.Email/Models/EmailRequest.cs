using System.ComponentModel.DataAnnotations;

namespace Info.JoshSpence.Api.Email.Models;

public class EmailRequest
{
    [Required]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MaxLength(2000)]
    public string Message { get; set; }

    public EmailRequest(string name, string email, string message)
    {
        Name = name;
        Email = email;
        Message = message;
    }
}

