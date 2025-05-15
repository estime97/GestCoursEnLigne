namespace BlazorHero.CleanArchitecture.Application.Requests
{
    public class PaginateRequest : PagedRequest
    {
        public string SearchString { get; set; }
    }
}
