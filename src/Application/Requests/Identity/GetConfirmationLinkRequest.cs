namespace BlazorHero.CleanArchitecture.Application.Requests.Identity;

public class GetConfirmationLinkRequest
{
    public GetConfirmationLinkRequest(string userId, string origin)
    {
        UserId = userId;
        Origin = origin;
    }
    public string UserId { get; init; }
    public string Origin { get; init; }
}
