namespace Notes.Identity.Models;

public class AppUser : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string SecondName { get; set; } = null!;
}