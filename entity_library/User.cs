using System.ComponentModel.DataAnnotations;

namespace entity_library;

public class User : Person
{
    private string email = "";
    private string password = "";

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Email must have a valid format.")]
    public string Email
    {
        get { return email; }
        set { email = value; }
    }

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, MinimumLength = 4, ErrorMessage = "Password must be between 4 and 100 characters.")]
    public string Password
    {
        get { return password; }
        set { password = value; }
    }
}
