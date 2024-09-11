namespace Application.Utilities;

public class JwtSettings
{
    public string ValidIssuer { get; set; }
    public string ValidAudience { get; set; }
    public double Expire { get; set; }
    public string SecreteKey { get; set; }
}
