namespace AuthenticationService.Authentication.Resources.POST;

public class SavePatientResource
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int Age { get; set; }
}