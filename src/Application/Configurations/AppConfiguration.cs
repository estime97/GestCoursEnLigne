namespace BlazorHero.CleanArchitecture.Application.Configurations;

public class AppConfiguration
{
    public string Secret { get; set; }

    public bool BehindSSLProxy { get; set; }

    public string ProxyIP { get; set; }

    public string ApplicationUrl { get; set; }
}

public class PasswordPolicy
{
    public bool PasswordCanExpire { get; set; }
    public int PasswordLifeTimeinDays { get; set; }
}
